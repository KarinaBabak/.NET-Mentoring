using System;
using Library.Interfaces;

namespace Library.Entities
{
    public class Newspaper : IEntity
    {
        public string Name { get; set; }
        public Publisher Publisher { get; set; }
        public int PublishYear { get; set; }
        public int PagesCount { get; set; }
        public string Note { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public string IssnNumber { get; set; }

        public override string ToString()
        {
            return $"Name - {Name}, IssnNumber - {IssnNumber}, Date - {Date.ToShortDateString()} Note - {Note}, PagesCount - {PagesCount}, Publisher - {Publisher.Name}";
        }
    }
}
