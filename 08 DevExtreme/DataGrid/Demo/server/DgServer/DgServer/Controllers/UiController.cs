using DgServer.Data;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using static DgServer.Data.DataStore;

namespace DgServer.Controllers
{
    [ApiController]
    [Route("api/ui")]
    public class UiController : Controller
    {
        private readonly DataStore _dataStore;

        public UiController(DataStore dataStore)
        {
            _dataStore = dataStore;
        }

        [HttpGet("menu")]
        public IActionResult GetMenus()
        {
            return Ok(_dataStore.LstMenu);
        }

        [HttpGet("filehierarchy")]
        public IActionResult GetFileHierarchy()
        {
            string rootDirectoryPath = "F:\\prajval-gahine\\root";

            var directoryInfo = new DirectoryInfo(rootDirectoryPath);
            var rootNode = CreateDirectoryNode(directoryInfo);

            string json = JsonSerializer.Serialize(rootNode, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(json);

            return Ok(json);
        }

        [HttpGet("folderitems")]
        public IActionResult GetFolderItems(string parentId)
        {

            List<Item> items = new List<Item>();

            // Get all directories and files within the specified path
            var directories = Directory.GetDirectories(parentId);
            var files = Directory.GetFiles(parentId);

            foreach (var directory in directories)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                items.Add(new Item
                {
                    Id = directoryInfo.FullName,
                    ParentId = parentId,
                    Text = directoryInfo.Name,
                    HasItems = true
                });
            }

            foreach (var file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                items.Add(new Item
                {
                    Id = fileInfo.FullName,
                    ParentId = parentId,
                    Text = fileInfo.Name,
                    HasItems = false
                });
            }

            return Ok(items);
        }   
    }

    class Item
    {
        public bool HasItems { get; set; }
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Text { get; set; }
    }
}
