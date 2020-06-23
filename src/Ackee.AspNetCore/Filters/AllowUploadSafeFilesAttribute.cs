using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage(AttributeTargets.Method)]
public sealed class AllowUploadSafeFilesAttribute : ActionFilterAttribute
{
    public int SizeKb { get; set; }
    public int SizeMB
    {
        get { return (SizeKb / 1000); }
        set { SizeKb = (1024 * value); }
    }


    static readonly IList<string> ExtToFilter = new List<string> {
            ".aspx", ".asax", ".asp", ".ashx", ".asmx", ".axd", ".master", ".svc", ".php" ,
            ".php3" , ".php4", ".ph3", ".ph4", ".php4", ".ph5", ".sphp", ".cfm", ".ps", ".stm",
            ".htaccess", ".htpasswd", ".php5", ".phtml", ".cgi", ".pl", ".plx", ".py", ".rb", ".sh", ".jsp",
            ".cshtml", ".vbhtml", ".swf" , ".xap", ".asptxt"
        };

    static readonly IList<string> NameToFilter = new List<string> {
           "web.config" , "htaccess" , "htpasswd", "web~1.con"
        };

    static bool CanUpload(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return false;


        fileName = fileName.ToLowerInvariant();
        var name = Path.GetFileName(fileName);
        var ext = Path.GetExtension(fileName);

        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidOperationException("Uploaded file should have a name.");

        return !ExtToFilter.Contains(ext) &&
               !NameToFilter.Contains(name) &&
               !NameToFilter.Contains(ext) &&
               //for "file.asp;.jpg" files
               ExtToFilter.All(item => !name.Contains(item));
    }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        foreach (var postedFile in GetFilesName(filterContext))
        {
            if (IsEmptyOrNull(postedFile)) continue;

            CheckSizeLimit(postedFile);
            
            CheckExtensionOfFile(postedFile);
        }

        base.OnActionExecuting(filterContext);
    }

    private static bool IsEmptyOrNull(IFormFile postedFile)
    {
        return postedFile == null || postedFile.Length == 0;
    }

    private static IFormFileCollection GetFilesName(ActionExecutingContext filterContext)
    {
        return filterContext.HttpContext.Request.Form.Files;
    }

    private void CheckSizeLimit(IFormFile postedFile)
    {
        if (DidSizeLimitSet())
            if (postedFile.Length > (SizeKb * 1000))
                throw new InvalidOperationException($"Too size {SizeKb.ToString()}");
    }
    private bool DidSizeLimitSet()
    {
        return SizeKb > 0;
    }
    private static void CheckExtensionOfFile(IFormFile postedFile)
    {
        if (!CanUpload(postedFile.FileName))
            throw new InvalidOperationException(string.Format("You are not allowed to upload {0} file.",
                Path.GetFileName(postedFile.FileName)));
    }
}