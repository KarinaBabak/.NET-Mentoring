using System;
using System.Collections.Generic;
using LinqToDB;
using LinqToDB.Mapping;

namespace Task1_ORM_Models.Entities
{
    [Table(Name = "Territories")]
    public class Territory
    {
        [Column(Name = "TerritoryID", Length = 20), PrimaryKey, NotNull]
        public string Id { get; set; }

        [Column(Name = "TerritoryDescription", Length = 50), NotNull]
        public string Description { get; set; }

        [Column(Name = "RegionID", DataType = DataType.Int32)]
        public int RegionId { get; set; }

        [Association(ThisKey = "RegionID", OtherKey = "Id", CanBeNull = false)]
        public Region Region { get; set; }

        [Association(ThisKey = "Id", OtherKey = "TerritoryId")]
        public IList<EmployeeTerritory> EmployeeTerritories { get; set; }
    }
}
