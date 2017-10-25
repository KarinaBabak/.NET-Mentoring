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

            foreach (var p in result)
            {
                ObjectDumper.Write(p.Customer);

                foreach (var s in p.Suppliers)
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
            

        }

    }
}
