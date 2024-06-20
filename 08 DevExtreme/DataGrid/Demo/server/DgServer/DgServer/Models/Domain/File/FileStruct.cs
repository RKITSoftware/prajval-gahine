namespace DgServer.Models.Domain.File
{
    public class FileStruct : FileFolderStruct
    {
        public string FileName { get; set; }

        public FileStruct(string name)
        {
            FileName = name;
        }
    }
}
