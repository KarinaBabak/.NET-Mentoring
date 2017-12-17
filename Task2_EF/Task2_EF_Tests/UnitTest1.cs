using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Task2_EF;
using Task2_EF.Migrations;
using System.Linq;

namespace Task2_EF_Tests
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        public void GetOrdersByCategory_Test()
        {
            int categoryId = 4;
            using (var dbConnetion = new NorthwindContext())
            {
                var entries = dbConnetion.Orders
                    .Include(o => o.Order_Details.Select(od => od.Product))
                    .Include(o => o.Customer)
                    .Where(o => o.Order_Details.All(od => od.Product.CategoryID == categoryId))
                    .Select(r => new
                    {
                        CompanyName = r.Customer.CompanyName,
                        OrderDetails = r.Order_Details.Select(od => new
                        {
                            od.ProductID,
                            od.Product.ProductName,
                            od.OrderID,
                            od.Discount,
                            od.Quantity,
                            od.UnitPrice
                        })
                    });

                foreach (var entry in entries.ToList())
                {
                    Console.WriteLine($"Customer: { entry.CompanyName } Products: { entry.OrderDetails.Select(od => od.ProductName) }");
                }


            }
        }
    }
}
