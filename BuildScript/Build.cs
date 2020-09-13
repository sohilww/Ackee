using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static Nuke.Common.IO.FileSystemTasks;
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
    public static int Main() => Execute<Build>(x => x.PushNuget);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;

    [Parameter] readonly long BuildNumber = 0;
    [Parameter] public readonly string ArtifactsPath = RootDirectory + @"\artifacts\";
    [Parameter] public readonly string ApiKey = "oy2hxoha2p5nxmhgbd6qtaoeqhbujspepmnizuk6em3fhm";
    [Parameter] readonly string NugetSourceURL = "https://www.nuget.org";

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
                                  + "." + BuildNumber + "-alpha</Version>";

            content = Regex.Replace(content, pattern, version);
            File.WriteAllText(filePath, content, Encoding.UTF8);
        });

    Target Compile => _ => _
        .DependsOn(UpdateVersion)
        .Produces(ArtifactsPath)
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
        .Requires(() => !string.IsNullOrEmpty(ArtifactsPath))
        .DependsOn(RunTests)
        .Produces(ArtifactsPath)
        .Executes(() =>
        {
            DotNetPack(a =>
                a.SetProject(Solution)
                    .SetConfiguration(Configuration)
                    .SetOutputDirectory(ArtifactsPath)
                    .SetAuthors("Soheil Karami")
                    .SetNoRestore(true)
                    .SetRepositoryUrl("https://soheilkarami92.visualstudio.com/Ackee")
                    .SetNoBuild(true));
        });

    Target PushNuget => _ => _
        .Requires(() => !string.IsNullOrEmpty(ApiKey))
        .Requires(() => !string.IsNullOrEmpty(NugetSourceURL))
        .DependsOn(PackNuget)
        .Executes(() =>
        {
            var packages = Directory.EnumerateFiles(ArtifactsPath, "*.nupkg");

            foreach (var package in packages)
            {
                if (package.ToLower().Contains("testutility"))
                    continue;
                Logger.Log(LogLevel.Warning, package);
                DotNetNuGetPush(a => a
                    .SetTargetPath(package)
                    .SetApiKey(ApiKey)
                    .SetSource(NugetSourceURL));
            }
        });
}