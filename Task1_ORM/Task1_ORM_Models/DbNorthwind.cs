using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB;
using Task1_ORM_Models.Entities;
using LinqToDB.Data;

namespace Task1_ORM_Models
{
    public class DbNorthwind : DataConnection
    {
        public DbNorthwind() : base("Northwind") { }

        public ITable<Product> Product { get { return GetTable<Product>(); } }
        public ITable<Category> Category { get { return GetTable<Category>(); } }
        public ITable<Customer> Customer { get { return GetTable<Customer>(); } }
        public ITable<Employee> Employee { get { return GetTable<Employee>(); } }
        public ITable<EmployeeTerritory> EmployeeTerritory { get { return GetTable<EmployeeTerritory>(); } }
        public ITable<Order> Order { get { return GetTable<Order>(); } }
        public ITable<OrderDetail> OrderDetail { get { return GetTable<OrderDetail>(); } }
        public ITable<Region> Region { get { return GetTable<Region>(); } }
        public ITable<Shipper> Shipper { get { return GetTable<Shipper>(); } }
        public ITable<Supplier> Supplier { get { return GetTable<Supplier>(); } }
        public ITable<Territory> Territory { get { return GetTable<Territory>(); } }

    }
}
