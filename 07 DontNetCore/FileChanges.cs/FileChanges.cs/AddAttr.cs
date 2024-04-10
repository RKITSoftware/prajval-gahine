using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileChanges.cs
{
    internal class AddAttr
    {

        private string _folderPath;
        private bool _isRecursive;


        public AddAttr(string folderPath, bool isRecursive)
        {
            _folderPath = folderPath;
            _isRecursive = isRecursive;
        }

        public void AddJsonPropertyNameAttr()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(_folderPath);


            SearchOption so = _isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            FileInfo[] lstFileInfo = dirInfo.GetFiles("*.cs", so);

            foreach (FileInfo fileInfo in lstFileInfo)
            {

                bool firstPublicFound = false;
                List<string> fileLines = File.ReadAllLines(fileInfo.FullName).ToList();
                for(int i = 0; i < fileLines.Count; i++)
                {
                    string line = fileLines[i];
                    int publicIndex = line.IndexOf("public");
                    if(publicIndex != -1)
                    {
                        if (!firstPublicFound)
                        {
                            firstPublicFound = true;
                        }
                        else
                        {
                            string subLine = line.Substring(publicIndex);
                            int propBeforeIndex = subLine.IndexOfNth(" ", 1);
                            int propIndex = propBeforeIndex + 1;
                            string propName = subLine.Substring(propIndex, 6);
                            string propJsonName = propName.Remove(3, 1).Insert(3, "f");
                            
                            fileLines.Insert(i, @$"        [JsonPropertyName(""{propJsonName}"")]");
                            i++;
                        }
                    }
                }
                File.WriteAllLines(fileInfo.FullName, fileLines);

            }
        }
    }
}
