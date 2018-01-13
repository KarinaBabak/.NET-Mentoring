using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.DB;
using Task.TestHelpers;
using System.Runtime.Serialization;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using Task.Surragates;

namespace Task
{
	[TestClass]
	public class SerializationSolutions
	{
		Northwind dbContext;

		[TestInitialize]
		public void Initialize()
		{
			dbContext = new Northwind();
		}

		[TestMethod]
		public void SerializationCallbacks()
		{
			dbContext.Configuration.ProxyCreationEnabled = false;

			var tester = new XmlDataContractSerializerTester<IEnumerable<Category>>(new NetDataContractSerializer(), true);
			var categories = dbContext.Categories.ToList();

			var category = categories.First();

			var actualResult = tester.SerializeAndDeserialize(categories).FirstOrDefault();

            Assert.AreEqual(category.GetType(), actualResult.GetType());
            Assert.AreEqual(category.Products.Count(), actualResult.Products.Count());
        }

		[TestMethod]
		public void ISerializable()
		{
			dbContext.Configuration.ProxyCreationEnabled = false;

            var objContext = (dbContext as IObjectContextAdapter).ObjectContext;
            var netDataContractSerializer = new NetDataContractSerializer(new StreamingContext(StreamingContextStates.All, objContext));
            var tester = new XmlDataContractSerializerTester<IEnumerable<Product>>(netDataContractSerializer);

            var products = dbContext.Products.ToList();
            var currentType = products.FirstOrDefault().GetType();

            var results = tester.SerializeAndDeserialize(products);
            var actualType = tester.SerializeAndDeserialize(products).FirstOrDefault().GetType();

            Assert.AreEqual(currentType, actualType);
        }


		[TestMethod]
		public void ISerializationSurrogate()
		{
			dbContext.Configuration.ProxyCreationEnabled = false;

            // Create a SurrogateSelector.
            SurrogateSelector surrogateSelector = new SurrogateSelector();

            // Tell the SurrogateSelector that Order_Detail objects are serialized and deserialized 
            // using the OrderDetailSerializationSurrogate object.
            surrogateSelector.AddSurrogate(typeof(Order_Detail),
                new StreamingContext(StreamingContextStates.All),
                new OrderDetailSerializationSurrogate());

            var objectContext = (dbContext as IObjectContextAdapter).ObjectContext;

            var netDataContractSerializer = new NetDataContractSerializer()
            {
                SurrogateSelector = surrogateSelector,
                Context = new StreamingContext(StreamingContextStates.All, objectContext)
            };

            var tester = new XmlDataContractSerializerTester<IEnumerable<Order_Detail>>(netDataContractSerializer);
			var orderDetails = dbContext.Order_Details.ToList();

			var actualResult = tester.SerializeAndDeserialize(orderDetails);

            Assert.AreEqual(orderDetails.GetType(), actualResult.GetType());
            Assert.AreEqual(orderDetails.FirstOrDefault().OrderID, actualResult.FirstOrDefault().OrderID);
        }

		[TestMethod]
		public void IDataContractSurrogate()
		{
			dbContext.Configuration.ProxyCreationEnabled = true;
			dbContext.Configuration.LazyLoadingEnabled = true;

            var dataContractSerializer = new DataContractSerializer(typeof(IEnumerable<Order>),
                new DataContractSerializerSettings
                {
                    DataContractSurrogate = new OrderDataContractSerializationSurrogate()
                });

            var tester = new XmlDataContractSerializerTester<IEnumerable<Order>>(dataContractSerializer);
			var orders = dbContext.Orders.ToList();

			var ordersList = tester.SerializeAndDeserialize(orders);
            var orderItem = ordersList.FirstOrDefault();

            Assert.AreNotEqual(orderItem.Customer, null);
            Assert.AreNotEqual(orderItem.Shipper, null);
            Assert.AreNotEqual(orderItem.Employee.FirstName, null);
            Assert.AreNotEqual(orderItem.Order_Details, null);
        }
	}
}
