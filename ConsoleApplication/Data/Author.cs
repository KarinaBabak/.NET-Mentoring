using Data.Interfaces;

namespace Data
{
    public class Author : IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AKAName { get; set; }
    }
}
