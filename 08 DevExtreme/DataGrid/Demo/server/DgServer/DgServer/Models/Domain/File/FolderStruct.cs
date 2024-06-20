namespace DgServer.Models.Domain.File
{
    public class FolderStruct : FileFolderStruct
    {
        public string FolderName { get; set; }

        public List<FileFolderStruct> Children { get; set; }

        public FolderStruct(string id, string name)
        {
            Id = id;
            FolderName = name;
        }

        public FolderStruct(string name)
        {
            FolderName = name;
        }

        public FileFolderStruct AddChild(FileFolderStruct item)
        {
            if (Children == null)
            {
                Children = new List<FileFolderStruct>();
            }
            item.Id = Id + "_" + (Children.Count + 1);
            Children.Add(item);

            return item;
        }
    }
}
