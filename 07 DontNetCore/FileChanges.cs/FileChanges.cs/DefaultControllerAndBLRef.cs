using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileChanges.cs
{
    internal class DefaultControllerAndBLRef
    {

        private string _folderPath;
        private bool _isRecursive;


        public DefaultControllerAndBLRef(string folderPath, bool isRecursive)
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
                int indexController = fileInfo.Name.IndexOf("Controller");
                string resourceName = fileInfo.Name.Split(".")[0]
                    .Substring(2, indexController - 2);

                string template = @$"

/// <summary>
/// Instance of BL{resourceName}
/// </summary>
private readonly BL{resourceName} _objBL{resourceName};

/// <summary>
/// Default constructor for CL{resourceName}Controller
/// </summary>
public CL{resourceName}Controller(){{
	_objBL{resourceName} = new BL{resourceName}();
}}

";


                FileStream fs1 = fileInfo.Open(FileMode.Open);
                string fileContent = string.Empty;
                using (StreamReader sr = new StreamReader(fs1))
                {
                    fileContent = sr.ReadToEnd();
                }

                int ApiControllerIndex = fileContent.IndexOf("ApiController");
                int nextOpenCurlyBraceIndex = fileContent.IndexOf("{", ApiControllerIndex);
                int toInsertIndex = nextOpenCurlyBraceIndex + 5;

                fileContent = fileContent.Insert(toInsertIndex, template);

                FileStream fs2 = fileInfo.Open(FileMode.Create, FileAccess.Write);
                using (StreamWriter sw = new StreamWriter(fs2))
                {
                    sw.Write(fileContent);
                }
            }
        }
    }
}
