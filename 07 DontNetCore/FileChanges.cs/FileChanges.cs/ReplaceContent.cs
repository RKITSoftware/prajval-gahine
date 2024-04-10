using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileChanges.cs
{
    internal class ReplaceContent
    {
        private string _folderPath;
        private string _fromContent;
        private string _toContent;
        private bool _isRecursive;

        public ReplaceContent(string FolderPath, string FromContent, string ToContent, bool IsRecursive)
        {
            _folderPath = FolderPath;
            _fromContent = FromContent;
            _toContent = ToContent;
            _isRecursive = IsRecursive;
        }

        public void Replace()
        {
            DirectoryInfo folderDirInfo = new DirectoryInfo(_folderPath);

            SearchOption so = _isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            FileInfo[] lstFileInfo = folderDirInfo.GetFiles("*.cs", so);

            foreach (FileInfo fileInfo in lstFileInfo)
            {
                string fileContent = string.Empty;
                FileStream fs = fileInfo.Open(FileMode.Open);

                using (StreamReader sr = new StreamReader(fs))
                {
                    fileContent = sr.ReadToEnd();
                    fileContent = fileContent.Replace(_fromContent, _toContent);
                }

                FileStream fs2 = fileInfo.Open(FileMode.Create, FileAccess.Write);

                using (StreamWriter sw = new StreamWriter(fs2))
                {
                    sw.Write(fileContent);
                }
            }
        }
    }
}
