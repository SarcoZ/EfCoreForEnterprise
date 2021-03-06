using System;
using System.Collections.ObjectModel;
using Store.Core.EntityLayer.HumanResources;

namespace Store.Core.EntityLayer.Sales
{
    public class Order : IAuditEntity
    {
        public Order()
        {
            OrderDetails = new Collection<OrderDetail>();
        }

        public Order(Int32 orderID)
        {
            OrderID = orderID;
            OrderDetails = new Collection<OrderDetail>();
        }

        public Int32? OrderID { get; set; }

        public Int16? OrderStatusID { get; set; }

        public DateTime? OrderDate { get; set; }

        public Int32? CustomerID { get; set; }

        public Int32? EmployeeID { get; set; }

        public Int32? ShipperID { get; set; }

        public Decimal? Total { get; set; }

        public String Comments { get; set; }

        public String CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public String LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public Byte[] Timestamp { get; set; }

        public virtual OrderStatus OrderStatusFk { get; set; }

        public virtual Customer CustomerFk { get; set; }

        public virtual Employee EmployeeFk { get; set; }

        public virtual Shipper ShipperFk { get; set; }

        public virtual Collection<OrderDetail> OrderDetails { get; set; }
    }
}
