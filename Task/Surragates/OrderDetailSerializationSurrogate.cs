using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Task.DB;

namespace Task.Surragates
{
    public class OrderDetailSerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var orderDetail = (Order_Detail)obj;
            var objectContext = context.Context as ObjectContext;

            if (objectContext != null)
            {
                objectContext.LoadProperty(orderDetail, o => o.Order);
                objectContext.LoadProperty(orderDetail, o => o.Product);
            }

            info.AddValue(nameof(orderDetail.Discount), orderDetail.Discount);
            info.AddValue(nameof(orderDetail.Order), orderDetail.Order);
            info.AddValue(nameof(orderDetail.OrderID), orderDetail.OrderID);
            info.AddValue(nameof(orderDetail.Product), orderDetail.Product);
            info.AddValue(nameof(orderDetail.ProductID), orderDetail.ProductID);
            info.AddValue(nameof(orderDetail.Quantity), orderDetail.Quantity);
            info.AddValue(nameof(orderDetail.UnitPrice), orderDetail.UnitPrice);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var orderDetail = (Order_Detail)obj;
            orderDetail.Discount = info.GetSingle(nameof(orderDetail.Discount));
            orderDetail.Order = (Order)info.GetValue(nameof(orderDetail.Order), typeof(Order));
            orderDetail.OrderID = info.GetInt32(nameof(orderDetail.OrderID));
            orderDetail.Product = (Product)info.GetValue(nameof(orderDetail.Product), typeof(Product));
            orderDetail.ProductID = info.GetInt32(nameof(orderDetail.ProductID));
            orderDetail.Quantity = info.GetInt16(nameof(orderDetail.Quantity));
            orderDetail.UnitPrice = info.GetDecimal(nameof(orderDetail.UnitPrice));

            return orderDetail;
        }
    }
}
