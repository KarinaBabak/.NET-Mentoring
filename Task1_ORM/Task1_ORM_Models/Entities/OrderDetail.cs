using System;
using System.Collections.Generic;
using LinqToDB;
using LinqToDB.Mapping;

namespace Task1_ORM_Models.Entities
{
    [Table(Name = "Order Details")]
    public class OrderDetail
    {
        [Column(Name = "OrderID"), PrimaryKey]
        public int OrderID { get; set; }

        [Association(ThisKey = "OrderID", OtherKey = "Id", CanBeNull = false)]
        public Order Order { get; set; }

        [Column(Name = "ProductID"), PrimaryKey]
        public int ProductId { get; set; }

        [Association(ThisKey = "ProductId", OtherKey = "Id", CanBeNull = false)]
        public Product Product { get; set; }

        public decimal UnitPrice { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }
    }
}
