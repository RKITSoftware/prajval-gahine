using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileChanges.cs
{
    internal class ModelInjectCreationLastModified
    {
        private string _folderPath;
        private bool _isRecursive;
        private static Dictionary<string, string> map = new Dictionary<string, string>(
            new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("ATD01", "Attendance"),
                new KeyValuePair<string, string>("DPT01", "Department"),
                new KeyValuePair<string, string>("EMP01", "Employee"),
                new KeyValuePair<string, string>("LVE02", "Leave"),
                new KeyValuePair<string, string>("PCH01", "Punch"),
                new KeyValuePair<string, string>("PSN01", "Position"),
                new KeyValuePair<string, string>("SLY01", "Salary"),
                new KeyValuePair<string, string>("USR01", "User")
            }
        );


        public ModelInjectCreationLastModified(string folderPath, bool isRecursive)
        {
            _folderPath = folderPath;
            _isRecursive = isRecursive;
        }

        public void Update()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(_folderPath);


            SearchOption so = _isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            FileInfo[] lstFileInfo = dirInfo.GetFiles("*.cs", so);

            foreach (FileInfo fileInfo in lstFileInfo)
            {
                string fileName = fileInfo.Name;
                string modelName = fileName.Split(".")[0];
                string modelSuffix = modelName.Substring(2, 3).ToLower();
                if(fileName.Length == 8)
                {
                    string fileContent = File.ReadAllText(fileInfo.FullName);

                    string pattern = $" {modelSuffix}f[0-9]{{2}} ";
                    Regex rg = new Regex(pattern);
                    MatchCollection matches = rg.Matches(fileContent);

                    int lastMatchedIndex = matches[matches.Count - 1].Index;

                    
                    string lastPropName = fileContent.Substring(lastMatchedIndex + 1, 6);
                    int lastPropNo = int.Parse(lastPropName.Substring(4, 2));  

                    int nextLineIndex = fileContent.IndexOf("\n", lastMatchedIndex + 1);

                    string blName = map.GetValueOrDefault(modelName);

                    string template = @$"
        /// <summary>
        /// {blName} creation datetime
        /// </summary>
        [ValidateNotNull]
        public DateTime {modelSuffix}f{(lastPropNo + 1).ToString("00")} {{ get; set; }}

        /// <summary>
        /// {blName} last modified datetime
        /// </summary>
        [ValidateNotNull]
        public DateTime {modelSuffix}f{(lastPropNo + 2).ToString("00")} {{ get; set; }}";

                    fileContent = fileContent.Insert(nextLineIndex, template);

                    File.WriteAllText(fileInfo.FullName, fileContent);
                }
            }
        }
    }
}
