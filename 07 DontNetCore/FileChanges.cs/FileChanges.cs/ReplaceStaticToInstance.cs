using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileChanges.cs
{
    internal class ReplaceStaticToInstance
    {
        private string _folderPath;
        private bool _isRecursive;

        public ReplaceStaticToInstance(string FolderPath, bool IsRecursive)
        {
            _folderPath = FolderPath;
            _isRecursive = IsRecursive;
        }

        public void Replace()
        {
            DirectoryInfo folderDirInfo = new DirectoryInfo(_folderPath);

            SearchOption so = _isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            FileInfo[] lstFileInfo = folderDirInfo.GetFiles("*.cs", so);

            foreach (FileInfo fileInfo in lstFileInfo)
            {
                int indexController = fileInfo.Name.IndexOf("Controller");
                string resourceName = fileInfo.Name.Split(".")[0]
                    .Substring(2, indexController - 2);

                string fromContent = "BL" + resourceName + ".";
                string toContent = "_obj" + fromContent;

                string fileContent = string.Empty;
                FileStream fs = fileInfo.Open(FileMode.Open);

                using (StreamReader sr = new StreamReader(fs))
                {
                    fileContent = sr.ReadToEnd();
                    int indexOfBLResource = fileContent.IndexOf("BLResource<");
                    if(indexOfBLResource > -1)
                    {
                        string fromContent2 = fileContent.Substring(indexOfBLResource, 17);
                        fileContent = fileContent.Replace(fromContent2, toContent);
                    }
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
