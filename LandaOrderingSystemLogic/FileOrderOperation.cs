using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LandaOrderingSystemLogic
{
    public class FileOrderOperation : IOrderManagementSystem
    {
        public FileOrderOperation()
        {
            List<Order> listOfOrders = ReadFromFile();
            listOfOrders.RemoveRange(0, listOfOrders.Count);
            WriteToFile(listOfOrders);
        }
        public List<Order> GetAllOrders()
        {
            return ReadFromFile();
        }
        public Order CreateOrder(Order i_Order)
        {
            List<Order> listOfOrders = ReadFromFile();
            listOfOrders.Add(i_Order);
            WriteToFile(listOfOrders);
            return i_Order;
        }
        public Order GetOrder(Guid i_OrderId)
        {
            return ReadFromFile().Find(obj => obj.OrderId == i_OrderId);
        }
        public void DeleteOrder(Guid i_OrderId)
        {
            List<Order> listOfOrders = ReadFromFile();

            Order orderToDelete = listOfOrders.Find(obj => obj.OrderId == i_OrderId);
            listOfOrders.Remove(orderToDelete);
            WriteToFile(listOfOrders); //write the new data

        }
        public Order UpdateOrder(Order i_Order)
        {
            List<Order> listOfOrders = ReadFromFile();
            Order orderForUpdate = listOfOrders.FirstOrDefault(order => order.OrderId == i_Order.OrderId);
            if (orderForUpdate != null) { orderForUpdate.CustomerName = i_Order.CustomerName; orderForUpdate.Details = i_Order.Details; orderForUpdate.TotalSum = i_Order.TotalSum; };

            WriteToFile(listOfOrders);

            return orderForUpdate;
        }
        public List<Order> GetAllCustomerOrders(Guid i_CustomerId)
        {
            return ReadFromFile().Where(Order => Order.CustomerId == i_CustomerId).ToList();
        }
        private void WriteToFile(List<Order> ListOfOrders)
        {
            string result = JsonConvert.SerializeObject(ListOfOrders, Formatting.Indented);
            File.WriteAllText(@"Orders.json", result);
        }
        private List<Order> ReadFromFile()
        {
            List<Order> resultOrder = JsonConvert.DeserializeObject<List<Order>>(File.ReadAllText(@"Orders.json"));
            return resultOrder;

        }

    }

}
