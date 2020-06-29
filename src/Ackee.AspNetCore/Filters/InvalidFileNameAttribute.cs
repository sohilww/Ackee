using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace Ackee.AspNetCore.Filters
{
    public class InvalidFileNameAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return canUpload(value.ToString());
        }

        static readonly IList<string> InvalidCharacters = new List<string> {
            "..", "//","@","~","^","&","*","cd ","<",">",":","\\","|","?",
            "con","prn","aux","nul","cm1","com2","com3","com4","com5","com6","com7",
            "com8","com9","lpt1","lpt2","lpt3","lpt4","lpt5","lpt6","lpt7","lpt8","lpt9"
        };

        static readonly IList<string> ExtToFilter = new List<string> {
            ".aspx", ".asax", ".asp", ".ashx", ".asmx", ".axd", ".master", ".svc", ".php" ,
            ".php3" , ".php4", ".ph3", ".ph4", ".php4", ".ph5", ".sphp", ".cfm", ".ps", ".stm",
            ".htaccess", ".htpasswd", ".php5", ".phtml", ".cgi", ".pl", ".plx", ".py", ".rb", ".sh", ".jsp",
            ".cshtml", ".vbhtml", ".swf" , ".xap", ".asptxt"
        };

        static readonly IList<string> NameToFilter = new List<string> {
           "web.config" , "htaccess" , "htpasswd", "web~1.con"
        };

        static bool canUpload(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;


            fileName = fileName.ToLowerInvariant();
            var name = GetFileName(fileName);
            var ext = Path.GetExtension(fileName);

            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidOperationException("Uploaded file should have a name.");

            return !ExtToFilter.Contains(ext) &&
                   !NameToFilter.Contains(name) &&
                   !NameToFilter.Contains(ext) &&
                   !InvalidCharacters.Contains(name) &&
                   //for "file.asp;.header" files
                   ExtToFilter.All(item => !name.Contains(item));
        }

        private static string GetFileName(string fileName)
        {
            var name = fileName.Split('.')[0];
            return name;
        }
    }



}

