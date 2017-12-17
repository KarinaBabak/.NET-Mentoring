using System;
using System.Collections.Generic;
using LinqToDB;
using LinqToDB.Mapping;

namespace Task1_ORM_Models.Entities
{
    [Table(Name = "Region")]
    public class Region
    {
        [Column(Name = "RegionID"), PrimaryKey]
        public int Id { get; set; }

        [Column(Name = "RegionDescription", Length = 50), NotNull]
        public string Description { get; set; }
    }
}
