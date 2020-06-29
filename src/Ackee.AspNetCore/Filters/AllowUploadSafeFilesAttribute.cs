using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ackee.AspNetCore.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class AllowUploadSafeFilesAttribute : ActionFilterAttribute
    {
        public int LitimSizeKB { get; set; }

        public int LimitSizeMB
        {
            get => (LitimSizeKB / 1024);
            set => LitimSizeKB = (1024 * value);
        }


        static readonly IList<string> ExtToFilter = new List<string>
        {
            ".aspx", ".asax", ".asp", ".ashx", ".asmx", ".axd", ".master", ".svc", ".php",
            ".php3", ".php4", ".ph3", ".ph4", ".php4", ".ph5", ".sphp", ".cfm", ".ps", ".stm",
            ".htaccess", ".htpasswd", ".php5", ".phtml", ".cgi", ".pl", ".plx", ".py", ".rb", ".sh", ".jsp",
            ".cshtml", ".vbhtml", ".swf", ".xap", ".asptxt"
        };

        static readonly IList<string> NameToFilter = new List<string>
        {
            "web.config", "htaccess", "htpasswd", "web~1.con"
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
                   ExtToFilter.All(item => !name.Contains((string) item));
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            foreach (var postedFile in GetFiles(filterContext))
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

        private static IFormFileCollection GetFiles(ActionExecutingContext filterContext)
        {
            return filterContext.HttpContext.Request.Form.Files;
        }

        private void CheckSizeLimit(IFormFile postedFile)
        {
            if (DidSizeLimitSet())
                if (postedFile.Length > (LitimSizeKB * 1024))
                    throw new InvalidFileException(GetFileName(postedFile));
        }

        private bool DidSizeLimitSet()
        {
            return LitimSizeKB > 0;
        }

        private static void CheckExtensionOfFile(IFormFile postedFile)
        {
            if (!CanUpload(postedFile.FileName))
                throw new InvalidFileException(GetFileName(postedFile));
        }

        private static string GetFileName(IFormFile postedFile)
        {
            return Path.GetFileName(postedFile.FileName);
        }

    }
}