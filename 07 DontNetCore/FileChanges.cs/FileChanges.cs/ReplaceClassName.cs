using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileChanges.cs
{
    internal class ReplaceClassName
    {
        private string _folderPath;
        private bool _isRecursive;
        private string _prefix;

        public ReplaceClassName(string folderPath, string prefix, bool isRecursive)
        {
            _folderPath = folderPath;
            _isRecursive = isRecursive;
            _prefix = prefix;
        }

        public void Replace()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(_folderPath);


            SearchOption so = _isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            FileInfo[] lstFileInfo = dirInfo.GetFiles("*.cs", so);

            foreach (FileInfo fileInfo in lstFileInfo)
            {
                string fileName = fileInfo.Name;
                string fileNameWoExt = fileName.Split(".")[0];

                FileStream fs1 = fileInfo.Open(FileMode.Open);
                string fileContent = string.Empty;
                using (StreamReader sr = new StreamReader(fs1))
                {
                    fileContent = sr.ReadToEnd();
                }
                fileContent = fileContent.Replace(fileNameWoExt, _prefix + fileNameWoExt);

                FileStream fs2 = fileInfo.Open(FileMode.Create, FileAccess.Write);
                using (StreamWriter sw = new StreamWriter(fs2))
                {
                    sw.Write(fileContent);
                }

                File.Move(_folderPath + fileName, _folderPath + _prefix + fileName);

            }



            // access .csproj file
            string csProjFile = @"F:\prajval-gahine\prajval-gahine\06 Advance CSharp\FirmAdvanceDemo\FirmAdvanceDemo\FirmAdvanceDemo.csproj";
            string csProjFileContent = string.Empty;
            using (StreamReader sr = new StreamReader(csProjFile))
            {
                csProjFileContent = sr.ReadToEnd();
            }

            foreach(FileInfo fileInfo in lstFileInfo)
            {
                csProjFileContent = csProjFileContent.Replace("DTO\\" + fileInfo.Name.Substring(3), "DTO\\" + fileInfo.Name);
            }
            using (StreamWriter sw = new StreamWriter(csProjFile))
            {
                sw.Write(csProjFileContent);
            }
        }
    }
}
