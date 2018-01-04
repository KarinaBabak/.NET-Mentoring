using Library.Interfaces;

namespace Library.Entities
{
    public class Inventor : IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
