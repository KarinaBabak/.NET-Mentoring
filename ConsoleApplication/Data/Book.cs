using Data.Interfaces;
using System.Collections.Generic;

namespace Data
{
    public class Book : IEntity
    {
        public string Name { get; set; }
        public List<Author> Authors { get; set; }
        public Publisher Publisher { get; set; }
        public int PublishYear { get; set; }
        public int PagesCount { get; set; }
        public string Note { get; set; }
        public string IsbnNumber { get; set; }
    }
}
