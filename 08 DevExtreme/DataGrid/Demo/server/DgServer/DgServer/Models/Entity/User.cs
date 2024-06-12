using DgServer.Models.Domain;

namespace DgServer.Models.Entity
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }   

        public Address PermanentAddress { get; set; }
    }
}
