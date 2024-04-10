using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileChanges.cs
{
    internal class DeleteWord
    {
        private string _folderPath;
        private string _word;
        private bool _isRecursive;

        public DeleteWord(string folderPath, string word, bool isRecursive)
        {
            _folderPath = folderPath;
            _word = word;
            _isRecursive = isRecursive;
        }

        public void Delete()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(_folderPath);


            SearchOption so = _isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            FileInfo[] lstFileInfo = dirInfo.GetFiles("*.cs", so);

            foreach (FileInfo fileInfo in lstFileInfo)
            {
                FileStream fs1 = fileInfo.Open(FileMode.Open);
                string fileContent = string.Empty;
                using (StreamReader sr = new StreamReader(fs1))
                {
                    fileContent = sr.ReadToEnd();
                }
                fileContent = fileContent.Replace(_word, "");

                FileStream fs2 = fileInfo.Open(FileMode.Create, FileAccess.Write);
                using (StreamWriter sw = new StreamWriter(fs2))
                {
                    sw.Write(fileContent);
                }
            }
        }
    }
}
