using System;
using System.Collections.Generic;
using LinqToDB;
using LinqToDB.Mapping;

namespace Task1_ORM_Models.Entities
{
    [Table(Name = "Customers")]
    public class Customer
    {
        [Column(Name = "CustomerID", Length = 5), PrimaryKey, Identity]
        public int Id { get; set; }

        [Column(Length = 40, DataType = DataType.NChar, CanBeNull = false)]
        public string CompanyName { get; set; }

        [Column(Length = 30, DataType = DataType.NChar, CanBeNull = true)]
        public string ContactName { get; set; }

        [Column(Length = 30, DataType = DataType.NChar, CanBeNull = true)]
        public string ContactTitle { get; set; }

        [Column(Length = 60, DataType = DataType.NVarChar)]
        public string Address { get; set; }

        [Column(Length = 15, DataType = DataType.NVarChar)]
        public string City { get; set; }

        [Column(Length = 15, DataType = DataType.NVarChar)]
        public string Region { get; set; }

        [Column(Length = 10, DataType = DataType.NVarChar)]
        public string PostalCode { get; set; }

        [Column(Length = 15, DataType = DataType.NVarChar)]
        public string Country { get; set; }

        [Column(Length = 24, DataType = DataType.NVarChar)]
        public string Phone { get; set; }

        [Column(Length = 24, DataType = DataType.NVarChar)]
        public string Fax { get; set; }
    }
}
