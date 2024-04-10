using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileChanges.cs
{
    internal class InjectConsPresaveValidateAddUpdate
    {

        private string _folderPath;
        private bool _isRecursive;
        private static Dictionary<string, string> map = new Dictionary<string, string>(
            new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Attendance", "ATD01"),
                new KeyValuePair<string, string>("Department", "DPT01"),
                new KeyValuePair<string, string>("Employee", "EMP01"),
                new KeyValuePair<string, string>("Leave", "LVE02"),
                new KeyValuePair<string, string>("Punch","PCH01"),
                new KeyValuePair<string, string>("Position","PSN01"),
                new KeyValuePair<string, string>("Salary", "SLY01"),
                new KeyValuePair<string, string>("User","USR01")
            }
        );


        private string namespaces = @"using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utitlity;
";


        public InjectConsPresaveValidateAddUpdate(string folderPath, bool isRecursive)
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
                string fileName = fileInfo.Name.Split(".")[0];
                if (fileName.StartsWith("BL"))
                {
                    
                    string blName = fileName.Substring(2);
                    string pocoModelName = map.GetValueOrDefault(blName);
                    string dtoModelName = $"DTO{ pocoModelName}";



                   if(pocoModelName != null)
                    {
                        string template = @$"
        /// <summary>
        /// Instance of {pocoModelName} model
        /// </summary>
        private {pocoModelName} _obj{pocoModelName};

        /// <summary>
        /// Default constructor for BL{blName}, initializes {pocoModelName} instance
        /// </summary>
        public BL{blName}()
        {{
            _obj{pocoModelName} = new {pocoModelName}();
        }}

        /// <summary>
        /// Method to convert DTO{pocoModelName} instance to {pocoModelName} instance
        /// </summary>
        /// <param name=""objDTO{pocoModelName}"">Instance of DTO{pocoModelName}</param>
        private void Presave(DTO{pocoModelName} objDTO{pocoModelName})
        {{
            _obj{pocoModelName} = objDTO{pocoModelName}.ConvertModel<{pocoModelName}>();
        }}

        /// <summary>
        /// Method to validate the {pocoModelName} instance
        /// </summary>
        /// <returns>True if {pocoModelName} instance is valid else false</returns>
        private bool Validate()
        {{
            return true;
        }}

        /// <summary>
        /// Method to Add (Create) a new record of {pocoModelName.ToLower()} table in DB
        /// </summary>
        private void Add()
        {{

        }}

        /// <summary>
        /// Method to Update (Modify) an existing record {pocoModelName.ToLower()} table in DB
        /// </summary>
        private void Update()
        {{

        }}
";
                        string fileContent = File.ReadAllText(fileInfo.FullName);
                        int BLResourceKWindex = fileContent.IndexOf(": BLResource");

                        if (BLResourceKWindex != -1)
                        {
                            int nextLineIndex = fileContent.IndexOf(@"
    {", BLResourceKWindex) + @"
    {".Length;

                            fileContent = fileContent.Insert(nextLineIndex, template);
                            fileContent = fileContent.Insert(0, namespaces);

                            File.WriteAllText(fileInfo.FullName, fileContent);
                        }
                    }
                }
            }
        }
    }
}   
