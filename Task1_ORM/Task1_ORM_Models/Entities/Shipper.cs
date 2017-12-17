using System;
using System.Collections.Generic;
using LinqToDB;
using LinqToDB.Mapping;

namespace Task1_ORM_Models.Entities
{
    [Table(Name = "Shippers")]
    public class Shipper
    {
        [Column(Name = "ShipperID"), PrimaryKey, Identity]
        public int Id { get; set; }

        [Column(Length = 40), NotNull]
        public string CompanyName { get; set; }

        [Column(Length = 24, DataType = DataType.NVarChar)]
        public string Phone { get; set; }
    }
}
