using System;
using System.Collections.Generic;
using Data.Interfaces;

namespace Data
{
    public class Patent : IEntity
    {
        public string Name { get; set; }
        public List<Inventor> Inventors { get; set; }
        public string Country { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime FilingDate { get; set; }
        public DateTime PublishDate { get; set; }
        public int PagesCount { get; set; }
        public string Note { get; set; }
    }
}
