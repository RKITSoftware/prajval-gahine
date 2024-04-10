using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FileChanges.cs
{
    internal class RemoveReturner
    {
        private string _folderPath;
        private bool _isRecursive;

        public RemoveReturner(string folderPath, bool isRecursive)
        {
            _folderPath = folderPath;
            _isRecursive = isRecursive;
        }

        public void Remove()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(_folderPath);

            SearchOption so = _isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            FileInfo[] lstFileInfo = dirInfo.GetFiles("*.cs", so);

            foreach (FileInfo fileInfo in lstFileInfo)
            {
                //FileStream fs1 = fileInfo.Open(FileMode.Open);
                //string fileContent = string.Empty;
                //using (StreamReader sr = new StreamReader(fs1))
                //{
                //    fileContent = sr.ReadToEnd();
                //    sr.ReadA
                //}

                List<string> fileLines = File.ReadAllLines(fileInfo.FullName).ToList();
                int fromIndex = -1;
                int toIndex = -1;
                foreach(var model in fileLines.Select((fileLine, i) => new { i, fileLine }))
                {
                    int index = model.fileLine.IndexOf("public IHttpActionResult Returner");
                    if (index != -1)
                    {
                        fromIndex = model.i - 1;
                        toIndex = model.i + 7;
                        break;
                    }
                }

                if(fromIndex != -1)
                {
                    for(int i = toIndex; i >= fromIndex; i--)
                    {
                        fileLines.RemoveAt(i);
                    }
                    File.WriteAllLines(fileInfo.FullName, fileLines);
                }


                //FileStream fs2 = fileInfo.Open(FileMode.Create, FileAccess.Write);
                //using (StreamWriter sw = new StreamWriter(fs2))
                //{
                //    sw.Write(fileContent);
                //}
            }
        }
    }
}
