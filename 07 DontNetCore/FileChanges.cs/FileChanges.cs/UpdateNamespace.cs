using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileChanges.cs
{
    internal class UpdateNamespace
    {
        private string _folderPath;
        private string _namespaceToAppend;

        public UpdateNamespace(string FolderPath, string NamespaceToAppend)
        {
            _folderPath = FolderPath;
            _namespaceToAppend = NamespaceToAppend;
        }

        public void AppendNamespace()
        {
            DirectoryInfo folderDirInfo = new DirectoryInfo(_folderPath);
            FileInfo[] lstFileInfo = folderDirInfo.GetFiles("*.cs");


            foreach(FileInfo fileInfo in lstFileInfo)
            {
                string fileContent = string.Empty;
                FileStream fs = fileInfo.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite);
                using(StreamReader sr = new StreamReader(fs))
                {
                    fileContent = sr.ReadToEnd();
                    int namespaceStartIndex = fileContent.IndexOf("namespace");
                    int newLineAfterNamespace = fileContent.IndexOf("\r\n", namespaceStartIndex);
                    fileContent = fileContent.Insert(newLineAfterNamespace, "." + _namespaceToAppend);
                }
                FileStream fs2 = fileInfo.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite);

                using (StreamWriter sw = new StreamWriter(fs2))
                {
                    sw.Write(fileContent);
                }
            }
        }
    }
}
