using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Task.DB;

namespace Task.Surragates
{
    public class OrderDataContractSerializationSurrogate : IDataContractSurrogate
    {
        public Type GetDataContractType(Type type)
        {
            return type;
        }

        public object GetObjectToSerialize(object obj, Type targetType)
        {
            if (obj is Order)
            {
                Order order = obj as Order;
                return CreateOrderToSerialize(order);
            }

            if (obj is Customer)
            {
                Customer customer = obj as Customer;
                return CreateCustomerToSerialize(customer);
            }

            if (obj is Customer)
            {
                Customer customer = obj as Customer;
                return CreateCustomerToSerialize(customer);
            }

            if (obj is Employee)
            {
                Employee employee = obj as Employee;
                return CreateEmployeeToSerialize(employee);
            }

            if (obj is Order_Detail)
            {
                Order_Detail orederDetail = obj as Order_Detail;
                return CreateOrderDetail(orederDetail);
            }

            if (obj is Shipper)
            {
                Shipper shipper = obj as Shipper;
                return CreateShipperToSerialize(shipper);
            }

            return obj;
        }

        public object GetDeserializedObject(object obj, Type targetType)
        {
            return obj;
        }

        #region init entities

        private Order CreateOrderToSerialize(Order order)
        {
            return new Order
            {
                Customer = order.Customer,
                CustomerID = order.CustomerID,
                Employee = order.Employee,
                EmployeeID = order.EmployeeID,
                Freight = order.Freight,
                OrderDate = order.OrderDate,
                OrderID = order.OrderID,
                Order_Details = order.Order_Details,
                RequiredDate = order.RequiredDate,
                ShipAddress = order.ShipAddress,
                ShipCity = order.ShipCity,
                ShipCountry = order.ShipCountry,
                ShipName = order.ShipName,
                ShippedDate = order.ShippedDate,
                Shipper = order.Shipper,
                ShipPostalCode = order.ShipPostalCode,
                ShipRegion = order.ShipRegion,
                ShipVia = order.ShipVia
            };
        }

        private Customer CreateCustomerToSerialize(Customer customer)
        {
            return new Customer
            {
                Address = customer.Address,
                City = customer.City,
                CompanyName = customer.CompanyName,
                ContactName = customer.ContactName,
                ContactTitle = customer.ContactTitle,
                Country = customer.Country,
                CustomerDemographics = customer.CustomerDemographics,
                CustomerID = customer.CustomerID,
                Fax = customer.Fax,
                Orders = null,
                Phone = customer.Phone,
                PostalCode = customer.PostalCode,
                Region = customer.Region
            };
        }

        private Employee CreateEmployeeToSerialize(Employee employee)
        {
            return new Employee
            {
                Address = employee.Address,
                BirthDate = employee.BirthDate,
                City = employee.City,
                Country = employee.Country,
                Employee1 = null,
                EmployeeID = employee.EmployeeID,
                Employees1 = null,
                Extension = employee.Extension,
                FirstName = employee.FirstName,
                HireDate = employee.HireDate,
                HomePhone = employee.HomePhone,
                LastName = employee.LastName,
                Notes = employee.Notes,
                Orders = null,
                Photo = employee.Photo,
                PhotoPath = employee.PhotoPath,
                PostalCode = employee.PostalCode,
                Region = employee.Region,
                ReportsTo = employee.ReportsTo,
                Territories = null,
                Title = employee.Title,
                TitleOfCourtesy = employee.TitleOfCourtesy
            };
        }

        private Order_Detail CreateOrderDetail(Order_Detail orederDetail)
        {
            return new Order_Detail
            {
                Discount = orederDetail.Discount,
                Order = null,
                OrderID = orederDetail.OrderID,
                Product = null,
                ProductID = orederDetail.ProductID,
                Quantity = orederDetail.Quantity,
                UnitPrice = orederDetail.UnitPrice
            };
        }

        private Shipper CreateShipperToSerialize(Shipper shipper)
        {
            return new Shipper
            {
                Phone = shipper.Phone,
                CompanyName = shipper.CompanyName,
                Orders = null,
                ShipperID = shipper.ShipperID
            };
        }

        #endregion

        #region Not implemented Methods
        public object GetCustomDataToExport(Type clrType, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
        {
            throw new NotImplementedException();
        }


        public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
        {
            throw new NotImplementedException();
        }

        public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
