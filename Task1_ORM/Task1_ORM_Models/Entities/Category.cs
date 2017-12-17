using System;
using System.Collections.Generic;
using LinqToDB;
using LinqToDB.Mapping;

namespace Task1_ORM_Models.Entities
{
    [Table(Name = "Categories")]
    public class Category
    {
        [Column(Name = "CategoryID"), PrimaryKey, Identity]
        public int Id { get; set; }

        [Column(Name = "CategoryName", Length = 15), NotNull]
        public string Name { get; set; }

        [Column(DataType = DataType.NVarChar)]
        public string Description { get; set; }

        [Column(DataType = DataType.Image)]
        public byte[] Picture { get; set; }
    }
}
