// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;
using System.Text.RegularExpressions;

// Version Mad01

namespace SampleQueries
{
    [Title("LINQ Module")]
    [Prefix("Linq")]
    public class LinqSamples : SampleHarness
    {

        private DataSource dataSource = new DataSource();

        [Category("Restriction Operators")]
        [Title("Where - Task 1")]
        [Description("This sample uses the where clause to find all elements of an array with a value less than 5.")]
        public void Linq_1()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var lowNums =
                from num in numbers
                where num < 5
                select num;

            Console.WriteLine("Numbers < 5:");
            foreach (var x in lowNums)
            {
                Console.WriteLine(x);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 2")]
        [Description("This sample return return all presented in market products")]

        public void Linq_2()
        {
            var products =
                from p in dataSource.Products
                where p.UnitsInStock > 0
                select p;

            foreach (var p in products)
            {
                ObjectDumper.Write(p);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 4")]
        [Description("This sample return customers id of customers who pay for all orders in total more than some value")]

        public void Linq1()
        {
            decimal x = 3000;
            var customers = dataSource.Customers
                .Where(customer => customer.Orders.Sum(order => order.Total) > x)
                .Select(customer => customer.CustomerID);

            foreach (var customer in customers)
            {
                ObjectDumper.Write(customer);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 4")]
        [Description("This sample return customers id of customers who pay for all orders in total more than some value")]

        public void Linq1_Expression()
        {
            decimal x = 3000;
            var customers = from customer in dataSource.Customers
                            where customer.Orders.Sum(order => order.Total) > x
                            select customer.CustomerID;

            foreach (var customer in customers)
            {
                ObjectDumper.Write(customer);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 5")]
        [Description("This sample return customers id of customers who pay for all orders in total more than some value")]

        public void Linq2_GroupJoin()
        {
            var result = dataSource.Customers
                .GroupJoin(
                    dataSource.Suppliers,
                    customer => new { customer.Country, customer.City },
                    supplier => new { supplier.Country, supplier.City },
                    (customer, suppliers) => new
                    {
                        Customer = customer.CompanyName,
                        Country = customer.Country,
                        City = customer.City,
                        Suppliers = suppliers
                    }
                );

            foreach (var c in result)
            {
                ObjectDumper.Write(c.Customer);

                foreach (var s in c.Suppliers)
                {
                    ObjectDumper.Write(s.SupplierName);
                }

            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 5")]
        [Description("This sample return customers ")]

        public void Linq2_GroupJoin_Expression()
        {
            var result = from customer in dataSource.Customers
                         join supplier in dataSource.Suppliers
                         on customer.City equals supplier.City into suppliers
                         select new
                         {
                             Customer = customer.CompanyName,
                             City = customer.City,
                             Suppliers = suppliers
                         };

            foreach (var c in result)
            {
                ObjectDumper.Write(c.Customer);

                foreach (var s in c.Suppliers)
                {
                    ObjectDumper.Write(s.SupplierName);
                }

            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 6")]
        [Description("This sample return customers id of customers who pay for all orders in total more than some value")]

        public void Linq2_WithOutGroupJoin()
        {
            foreach (var customer in dataSource.Customers)
            {
                ObjectDumper.Write(customer.CompanyName);
                ObjectDumper.Write(
                    dataSource.Suppliers
                        .Where(s => s.Country == customer.Country && s.City == customer.City)
                        .Select(s => s.SupplierName)
                );
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 7")]
        [Description("This sample return clients who has order with price more than X")]

        public void Linq3()
        {
            decimal maxPrice = 9000;
            ObjectDumper.Write(dataSource.Customers
                .Where(c => c.Orders.Any(o => o.Total > maxPrice))
                .Select(c => c.CompanyName));
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 7")]
        [Description("This sample return clients who has order with price more than X")]

        public void Linq3_Expression()
        {
            decimal maxPrice = 9000;
            var result = from customer in dataSource.Customers
                         where customer.Orders.Any(o => o.Total > maxPrice)
                         select customer.CompanyName;
            ObjectDumper.Write(result);
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 8")]
        [Description("This sample return clients with date of their first ordering")]

        public void Linq4()
        {
            ObjectDumper.Write(
                dataSource.Customers
                    .Select(c => new {
                        customer = c.CompanyName,
                        firstOrder = c.Orders.Any() ? c.Orders.Min(o => o.OrderDate) : (DateTime?)null
                    })
            );
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 9")]
        [Description("This sample return clients with date of their first ordering")]

        public void Linq5()
        {
            ObjectDumper.Write(
                dataSource.Customers
                    .Select(c => new {
                        customerName = c.CompanyName,
                        totalSumOfOrders = c.Orders.Any() ? c.Orders.Sum(o => o.Total) : 0,
                        firstOrder = c.Orders.Any() ? c.Orders.Min(o => o.OrderDate) : (DateTime?)null
                    })
                    .OrderBy(o => o.firstOrder.HasValue ? o.firstOrder.Value.Year : 0)
                    .ThenBy(o => o.firstOrder.HasValue ? o.firstOrder.Value.Month : 0)
                    .ThenByDescending(o => o.totalSumOfOrders)
                    .ThenBy(o => o.customerName)
            );
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 9")]
        [Description("This sample return clients with date of their first ordering")]

        public void Linq5_Expression()
        {
            var newCustomerItems = from cusomer in dataSource.Customers
                         select new
                         {
                             customerName = cusomer.CompanyName,
                             totalSumOfOrders = cusomer.Orders.Any() ? cusomer.Orders.Sum(o => o.Total) : 0,
                             firstOrder = cusomer.Orders.Any() ? cusomer.Orders.Min(o => o.OrderDate) : (DateTime?)null
                         };

            var result = from item in newCustomerItems
                         orderby item.firstOrder.Value.Year
                         orderby item.firstOrder.Value.Month
                         orderby item.totalSumOfOrders descending
                         orderby item.customerName
                         select item;

            ObjectDumper.Write(result);
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 10")]
        [Description("This sample return clients without region, phone extension and with non diginal postal code")]

        public void Linq6()
        {
            var degitalRegex = "^[0-9]+$";
            var customers = dataSource.Customers.Where(
                c => (!String.IsNullOrEmpty(c.PostalCode) && !Regex.IsMatch(c.PostalCode, degitalRegex))
                || String.IsNullOrWhiteSpace(c.Region)
                || c.Phone[0] != '(');
            foreach (var customer in customers)
            {
                ObjectDumper.Write($"{customer.CompanyName} {customer.PostalCode} {customer.Region} {customer.Phone}");
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 11")]
        [Description("This sample return groupped products")]

        public void Linq7()
        {
            var result = dataSource.Products
                .GroupBy(p => p.Category)
                .Select(category =>
                    {
                        var grouppedByCount =
                            category.GroupBy(c => c.UnitsInStock).OrderBy(u => u.Count());
                        var grouppedByPrice = grouppedByCount.Select(i => i.OrderBy(p => p.UnitPrice));
                        return new
                        {
                            category = category.Key,
                            grouppedProducts = grouppedByPrice.ToList()
                        };
                    }
                );

            foreach (var item in result)
            {
                ObjectDumper.Write(item.category.ToUpper());
                foreach (var product in item.grouppedProducts)
                {
                    ObjectDumper.Write(product);
                }
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 12")]
        [Description("This sample return groupped products by price")]

        public void Linq8()
        {
            var result = dataSource.Products
                .GroupBy(p =>
                {
                    if (p.UnitPrice < 10)
                    {
                        return "cheap";
                    }
                    else if (p.UnitPrice < 20)
                    {
                        return "not expensive";
                    }

                    return "expensive";
                });

            foreach (var groups in result)
            {
                ObjectDumper.Write(groups.Key.ToUpper());
                foreach (var product in groups)
                {
                    ObjectDumper.Write(product);
                }
            }
        }


        [Category("Restriction Operators")]
        [Title("Where - Task 13")]
        [Description("This sample return cite revenue and sell intensity")]

        public void Linq9()
        {
            var result = dataSource.Customers
                .GroupBy(c => c.City)
                .Select(group => new
                {
                    city = group.Key,
                    sum = group.Where(c => c.Orders.Any()).Average(c => c.Orders.Sum(o => o.Total)),
                    intensity = group.Sum(i => i.Orders.Count()) / group.Count()
                });
            ObjectDumper.Write(result);
        }

        //[Category("Restriction Operators")]
        //[Title("Where - Task 13")]
        //[Description("This sample return cite revenue and sell intensity")]

        //public void Linq9_Expression()
        //{
        //    var result = from customer in dataSource.Customers
        //                 group customer by customer.City into custGroup
        //                 select new
        //                 {
        //                     city = custGroup.Key,
        //                     intensity = custGroup.Sum(i => i.Orders.Count()) / custGroup.Count(),
        //                     sum = (from c in custGroup
        //                            where (c.Orders.Any()).Average(c => c.Orders.Sum(o => o.Total)))
        //                 };

        //    ObjectDumper.Write(result);
        //}

        [Category("Restriction Operators")]
        [Title("Where - Task 14")]
        [Description("This sample return statistic")]

        public void Linq10()
        {
            var orders = dataSource.Customers.SelectMany(c => c.Orders);

            foreach (var item in orders.GroupBy(o => o.OrderDate.Month))
            {
                ObjectDumper.Write(item);
            }

            foreach (var item in orders.GroupBy(o => o.OrderDate.Year))
            {
                ObjectDumper.Write(item);
            }

            foreach (var item in orders.GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month }))
            {
                ObjectDumper.Write(item);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 15")]
        [Description("Вывести клиентов, которые заказывали товары только у одного поставщика, сортировать по количеству потраченных денег.")]

        public void Linq11()
        {

            var result = dataSource.Customers.Select(c => new
            {
                customer = c.CustomerID,
                totalSumOfOrders = c.Orders.Any() ? c.Orders.Sum(o => o.Total) : 0,
                orders = c.Orders
            })
            .OrderBy(o => o.totalSumOfOrders);

            foreach (var item in result)
            {
                var firstItem = item.orders.FirstOrDefault();
                string supplier = firstItem != null ? firstItem.SupplierName : null;

                if (supplier != null && item.orders.All(i => i.SupplierName == supplier))
                {
                    ObjectDumper.Write(item.customer);
                }
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 16")]
        [Description("найти первые K клиентов, у которых было сделано больше N заказов на сумму не меньше S у одного поставщика, вывести их, а также города, где они делали заказы")]

        public void Linq12()
        {
            var k = 2;
            var n = 3; // amount of orders
            var s = 100;
            var result = dataSource.Customers.Select(c => new
            {
                customer = c.CustomerID,
                city = c.City,
                ordersCount = c.Orders.Any() ? c.Orders.Count() : 0,
                orders = c.Orders.GroupBy(o => o.SupplierName)
            })
            .Where(item => item.orders.Any(a => a.Count() > n && a.Sum(o => o.Total) > s))
            .Take(k);
            if(true)
            {
                ObjectDumper.Write(result);
            }
            //foreach (var item in result)
            //{
            //    var firstItem = item.orders.FirstOrDefault();
            //    string supplier = firstItem != null ? firstItem.SupplierName : null;

            //    if (supplier != null && item.orders.All(i => i.SupplierName == supplier))
            //    {
            //        ObjectDumper.Write(item.customer);
            //    }
            //}
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 16")]
        [Description("Вывести первые K продуктов, которые пользовались наибольшей популярностью за заданный промежуток времени . Наибольшую популярность считать двумя способами(т.е. будет два запроса): ")]

        public void Linq13()
        {
        }
    }

}
