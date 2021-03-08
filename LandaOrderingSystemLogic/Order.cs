using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace LandaOrderingSystemLogic
{
    public class Order
    {          
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Details { get; set; }
        public double TotalSum { get; set; }
        public Order( string i_CustomerName, string i_Details, double i_TotalSum)
        {
            this.OrderId = Guid.NewGuid(); //random unique guid.
            this.CustomerId = Guid.NewGuid();

            this.CustomerName = i_CustomerName;
            this.Details = i_Details;
            this.TotalSum = i_TotalSum;

        }
        public Order() { }
        public override string ToString()
        {
         return string.Format(@"Order information:
                      order id: {0},
                      customer id : {1},
                      customer name: {2},
                      order ditails: {3},
                      total sum : {4} ", OrderId, CustomerId, CustomerName,Details,TotalSum);           
          
        }
    }
}
