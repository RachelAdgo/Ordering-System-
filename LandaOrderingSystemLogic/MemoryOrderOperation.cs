using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LandaOrderingSystemLogic
{
    public class MemoryOrderOperation : IOrderManagementSystem
    {
        readonly List<Order> listOrders = new List<Order>();

        public Order CreateOrder(Order order)
        {
            listOrders.Add(order);
            return order;
        }

        public void DeleteOrder(Guid orderId)
        {
            listOrders.Remove(listOrders.Find(obj => obj.OrderId == orderId));
        }

        public List<Order> GetAllCustomerOrders(Guid customerId)
        {
            return listOrders.FindAll(Order => Order.CustomerId == customerId);
        }

        public List<Order> GetAllOrders()
        {
            return listOrders;
        }

        public Order GetOrder(Guid orderId)
        {
            return listOrders.Find(obj => obj.OrderId == orderId);
        }

        public Order UpdateOrder(Order order)
        {
            Order updateOrder = listOrders.FirstOrDefault(obj => obj.OrderId == order.OrderId);
            if (updateOrder != null) { updateOrder.CustomerName = order.CustomerName; updateOrder.Details = order.Details; updateOrder.TotalSum = order.TotalSum; };
            return updateOrder;
        }
    }
}
