using Library.Interfaces;

namespace Library.Entities
{
    public class Publisher : IEntity
    {
        public string City { get; set; }
        public string Name { get; set; }
    }
}
