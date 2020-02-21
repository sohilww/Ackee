using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.NuGet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[UnsetVisualStudioEnvironmentVariables]
class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<Build>(x => x.PushNuget);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;

    [Parameter] readonly long BuildNumber = 0;
    [Parameter] string ArtifactsPath = RootDirectory + "/artifacts/";
    [Parameter] string ApiKey= "oy2fcudljgbqv6u7ao4q44jugu7wuk5exjhwjgrcfxm63m";
    [Parameter] string NugetSourceURL= "https://www.nuget.org";

    Target Clean => _ => _
        .Executes(() =>
        {
            DotNetClean(a =>
                a.SetProject(Solution)
                    .SetConfiguration(Configuration));
            EnsureCleanDirectory(ArtifactsPath);
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(a =>
                a.SetProjectFile(Solution));
        });

    Target UpdateVersion => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            var ss = Solution.AllProjects;
            var filePath = Path.Combine(Solution.Directory, "Directory.Build.Props");
            const string pattern = @"<Version>(.*)<\/Version>";
            var content = File.ReadAllText(filePath, Encoding.UTF8);
            var group = Regex.Match(content, pattern).Groups;
            var version = group[group.Count - 1].Value;
            version = "<Version>" + version
                          .Substring(0, 
                              version.LastIndexOf(".", StringComparison.Ordinal)) 
                                  + "." + BuildNumber + "</Version>";

            content = Regex.Replace(content, pattern, version);
            File.WriteAllText(filePath,content,Encoding.UTF8);

        });

    Target Compile => _ => _
        .DependsOn(UpdateVersion)
        .Executes(() =>
        {
            DotNetBuild(a =>
                a.SetProjectFile(Solution)
                    .SetNoRestore(true)
                    .SetConfiguration(Configuration));
        });

    Target RunTests => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            var projects = Solution.AllProjects.Where(a => a.Name.Contains("Test"));

            foreach (var project in projects)
            {
                DotNetTest(a => a
                    .SetConfiguration(Configuration)
                    .SetNoRestore(true)
                    .SetProjectFile(project)
                    .SetNoBuild(true));

            }
        });
    

    Target PackNuget => _ => _
        .DependsOn(RunTests)
        .Executes(() =>
        {
            DotNetPack(a =>
                a.SetProject(Solution)
                    .SetOutputDirectory(ArtifactsPath)
                    .SetNoRestore(true)
                    .SetNoBuild(true));
        });

    Target PushNuget => _ => _
        .DependsOn(PackNuget)
        .Executes(() =>
        {
            DotNetNuGetPush(a => a
                .SetTargetPath(ArtifactsPath)
                .SetApiKey(ApiKey)
                .SetSource(NugetSourceURL));
        });

}