using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using LinqToDB;
using Task1_ORM_Models;
using Task1_ORM_Models.Entities;
using System.Collections.Generic;

namespace Task1_ORM_Tests
{
    [TestClass]
    public class DbNorthwindTests
    {
        //•	Список продуктов с категорией и поставщиком
        [TestMethod]
        public void CategoriesAndSupplersList_Test()
        {
            using (var db = new DbNorthwind())
            {
                foreach (var product in db.Product.LoadWith(p => p.Category).LoadWith(p => p.Supplier).ToList())
                {
                    Console.WriteLine($"Product name: {product.ProductName}; Category: {product.Category?.Name}; Supplier: {product.Supplier?.ContactName}");
                }
            }
        }

        //•	Список сотрудников с указанием региона, за который они отвечают
        [TestMethod]
        public void EmployeesRegionsList_Test()
        {
            using (var db = new DbNorthwind())
            {
                //var employees = db.Employee
                //    .LoadWith(e => e.EmployeeTerritories)
                //    .LoadWith(et => et.).Take(10);

                var entries = from e in db.Employee
                                join et in db.EmployeeTerritory on e.Id equals et.EmployeeId
                                join t in db.Territory on et.TerritoryId equals t.Id
                                join r in db.Region on t.RegionId equals r.Id
                                select new
                                {
                                    EmployeeId = e.Id,
                                    LastName = e.LastName,
                                    Region = r.Description
                                };

                foreach (var entry in entries.Distinct())
                {
                    Console.WriteLine($"Id: {entry.EmployeeId}. Name: {entry.LastName} - Region: {entry.Region}");
                }
            }
        }

        //•	Статистики по регионам: количества сотрудников по регионам
        [TestMethod]
        public void EmployeesCountByRegions_Test()
        {
            using (var db = new DbNorthwind())
            {
                var query = from e in db.Employee
                            join et in db.EmployeeTerritory on e.Id equals et.EmployeeId
                            join t in db.Territory on et.TerritoryId equals t.Id
                            join r in db.Region on t.RegionId equals r.Id
                            group e by r.Description;

                
                foreach (var entry in query.ToDictionary(r => r.Key, r => r.Count()))
                {
                    Console.WriteLine($"Region: {entry.Key} - Employee count: {entry.Value}");
                }
            }
        }

        //•	Списка «сотрудник – с какими грузоперевозчиками работал» (на основе заказов)
        [TestMethod]
        public void EmployeesByOrders_Test()
        {
            using (var db = new DbNorthwind())
            {
                var query = from e in db.Employee
                            join o in db.Order on e.Id equals o.EmployeeID
                            join s in db.Shipper on o.ShipVia equals s.Id
                            select new
                            {
                                Employee = o.Employee,
                                Shipper = s.CompanyName
                            };

                foreach (var entry in query.ToList())
                {
                    Console.WriteLine($"Employee: {entry.Employee.FirstName} {entry.Employee.LastName} - Shipper: {entry.Shipper}");
                }
            }
        }

        #region Insert/Uptare queries

        //•	Добавить нового сотрудника, и указать ему список территорий, за которые он несет ответственность
        [TestMethod]
        public void AddEmployeeWithTerritories_Test()
        {
            Employee newEmployee = new Employee { FirstName = "Anton", LastName = "Chehov" };

            using (var dbConnection = new DbNorthwind())
            {
                try
                {
                    dbConnection.BeginTransaction();
                    newEmployee.Id = Convert.ToInt32(dbConnection.InsertWithIdentity(newEmployee));
                    dbConnection.Territory.Where(t => t.RegionId == 2)
                        .Insert(dbConnection.EmployeeTerritory, t => new EmployeeTerritory { EmployeeId = newEmployee.Id, TerritoryId = t.Id });
                    dbConnection.CommitTransaction();
                }
                catch
                {
                    dbConnection.RollbackTransaction();
                }
            }
        }

        //•	Перенести продукты из одной категории в другую
        [TestMethod]
        public void ChangeProductsCategory_Test()
        {
            using (var dbConnection = new DbNorthwind())
            {
                var products = dbConnection.Product.Where(p => p.CategoryId == 2).Take(6)
                    .Update(pr => new Product
                        {
                            CategoryId = 1
                        });                
            }
        }

        //•	•	Добавить список продуктов со своими поставщиками и категориями (массовое занесение), при этом если поставщик или категория с таким названием есть, то использовать их – иначе создать новые. 
        [TestMethod]
        public void AddProsuctsWithShippersAndCategories_Test()
        {
            var products = new List<Product>
            {
                new Product
                {
                    ProductName = "Smarthpnone",
                    Category = new Category {Name = "Phones"},
                    Supplier = new Supplier {CompanyName = "Samsung"}
                },
                new Product
                {
                    ProductName = "Ring",
                    Category = new Category {Name = "Jewerly"},
                    Supplier = new Supplier {CompanyName = "Jenavi"}
                }
            };

            using (var dbConnection = new DbNorthwind())
            {
                try
                {
                    foreach (var product in products)
                    {
                        var category = dbConnection.Category.FirstOrDefault(c => c.Name == product.Category.Name);
                        product.CategoryId = category?.Id ?? Convert.ToInt32(dbConnection.InsertWithIdentity(
                            new Category
                            {
                                Name = product.Category.Name
                            }));
                        var supplier = dbConnection.Supplier.FirstOrDefault(s => s.CompanyName == product.Supplier.CompanyName);
                        product.SupplierId = supplier?.Id ?? Convert.ToInt32(dbConnection.InsertWithIdentity(
                            new Supplier
                            {
                                CompanyName = product.Supplier.CompanyName
                            }));
                    }

                    dbConnection.InsertWithIdentity(products);
                    dbConnection.CommitTransaction();
                }
                catch
                {
                    dbConnection.RollbackTransaction();
                }
            }
        }

        //•	Замена продукта на аналогичный: во всех еще неисполненных заказах(считать таковыми заказы, у которых ShippedDate = NULL) заменить один продукт на другой.
        [TestMethod]
        public void ReplaceProductsWithEqvivalent_Test()
        {
            using (var db = new DbNorthwind())
            {
                var orderDetails = db.OrderDetail
                    .LoadWith(od => od.Order)
                    .Where(od => od.Order.ShippedDate == null)
                    .Update(
                        od => new OrderDetail
                        {
                            ProductId = db.Product.First(p => p.CategoryId == od.Product.CategoryId && p.Id > od.ProductId) != null
                                ? db.Product.First(p => p.CategoryId == od.Product.CategoryId && p.Id > od.ProductId).Id
                                : db.Product.First(p => p.CategoryId == od.Product.CategoryId).Id
                        });

            }
        }

        #endregion
    }
}
