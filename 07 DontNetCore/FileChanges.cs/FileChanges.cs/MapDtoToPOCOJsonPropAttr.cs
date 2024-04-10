using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileChanges.cs
{
    internal class MapDtoToPOCOJsonPropAttr
    {
        private string _dtoFolder;
        private string _pocoFolder;
        private bool _isRecursive;


        public MapDtoToPOCOJsonPropAttr(string dtoFolder, string pocoFolder, bool isRecursive)
        {
            _dtoFolder = dtoFolder;
            _pocoFolder = pocoFolder;
            _isRecursive = isRecursive;
        }

        public void Map()
        {
            DirectoryInfo dtoDirInfo = new DirectoryInfo(_dtoFolder);
            DirectoryInfo pocoDirInfo = new DirectoryInfo(_pocoFolder);


            SearchOption so = _isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            FileInfo[] lstDtoFileInfo = dtoDirInfo.GetFiles("*.cs", so);
            FileInfo[] lstPocoFileInfo = pocoDirInfo.GetFiles("*.cs", so);

            foreach (FileInfo fileInfo in lstDtoFileInfo)
            {
                string fileName = fileInfo.Name;
                string nameWithoutDTO = fileName.Substring(3);
                
                FileInfo dtoFileInfo = lstDtoFileInfo.SingleOrDefault(fileInfo => fileInfo.Name == nameWithoutDTO);

                List<string> dtoFileLines = File.ReadAllLines(fileInfo.FullName).ToList();

                string pocoFilePath = lstPocoFileInfo.SingleOrDefault(fileInfo1 => fileInfo1.Name.StartsWith(nameWithoutDTO)).FullName;
                List<string> pocoFileLines = File.ReadAllLines(pocoFilePath).ToList();

                if (pocoFileLines[0] == "using System.Text.Json.Serialization;")
                {
                    continue;
                }

                bool toSkipPocoFile = false;

                for (int i = 0; i < dtoFileLines.Count; i++)
                {
                    string dtoFileLine = dtoFileLines[i];
                    int attrIndex = dtoFileLine.IndexOf("JsonPropertyName(\"");
                    if(attrIndex != -1)
                    {
                        int pocoPropIndex = attrIndex + 18;
                        string dtoNextLine = dtoFileLines[i + 1];
                        string dtoNextLineTrimmed = dtoNextLine.Trim();
                        string dtoPropName = dtoNextLineTrimmed.Split(" ").SingleOrDefault(kw => kw.Length == 6 && kw.Any(char.IsDigit));
                        string pocoPropName = dtoFileLine.Substring(pocoPropIndex, 6);


                        bool classBodyStarted = false;
                        for (int j = 0; j < pocoFileLines.Count; j++)
                        {
                            string pocoFileLine = pocoFileLines[j];
                            if (!classBodyStarted && pocoFileLine.Contains("public"))
                            {
                                classBodyStarted = true;
                                continue;
                            }
                            if (classBodyStarted && pocoFileLine.Contains(pocoPropName))
                            {
                                pocoFileLines.Insert(j, @$"        [JsonPropertyName(""{dtoPropName}"")]");
                                j++;
                            }
                        }
                    }
                }
                pocoFileLines.Insert(0, "using System.Text.Json.Serialization;");
                File.WriteAllLines(pocoFilePath, pocoFileLines);
            }
        }
    }
}
