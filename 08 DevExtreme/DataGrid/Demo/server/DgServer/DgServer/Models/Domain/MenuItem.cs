using Newtonsoft.Json;

namespace DgServer.Models.Domain
{
    public class MenuItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("items")]
        public List<MenuItem> Child { get; set; }

        public MenuItem(string name)
        {
            Id = string.Empty;
            Name = name;
        }

        public MenuItem(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public MenuItem AddChild(MenuItem item)
        {
            if(Child == null)
            {
                Child = new List<MenuItem>();
            }
            item.Id = Id + "_" + (Child.Count + 1);
            Child.Add(item);

            return item;
        }
    }
}
