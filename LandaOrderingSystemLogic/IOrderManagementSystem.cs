 using System;
using System.Collections.Generic;
using System.Text;

namespace LandaOrderingSystemLogic
{
    public interface IOrderManagementSystem
    {
        List<Order> GetAllOrders();
        Order CreateOrder(Order order);
        Order GetOrder(Guid orderId);
        void DeleteOrder(Guid orderId);
        Order UpdateOrder(Order order);
        List<Order> GetAllCustomerOrders(Guid customerId);

    }
}
