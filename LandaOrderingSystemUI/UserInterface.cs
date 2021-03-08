using System;
using System.Collections.Generic;
using System.Text;
using LandaOrderingSystemLogic;

namespace LandaOrderingSystemUI
{
    public class UserInterface
    {
        private static bool s_quitOption = false;
        private static int legalInput;
        private static readonly IOrderManagementSystem supportInstance = new FileOrderOperation();
        private static readonly List<Guid> listOfOrdersById = new List<Guid>();
        private static readonly List<Guid> listOfCustomersId = new List<Guid>();
        private const string c_MainMenu =
   @"  welcome to Main Menu!!
      Please select one of the options:
   1. Create new order.
   2. Get order by order id.
   3. Delete order by order id.
   4. Updete order.
   5. Get all customer orders by customer id.
   6. Get all orders
   7. Quit.
   ";

        private const string c_UpdeteItem =
   @"Please select one of the options to edit:
   1. customer name.
   2. order details.
   3. order total sum.
   4. Quit.
   ";

        private static void ShowMenuForUpdate()
        {
            Console.WriteLine(c_UpdeteItem);
        }
        private static void ChooseOptionForUpdate(int i_NumberOfOrderInList, Order order)
        {
            switch (i_NumberOfOrderInList)
            {
                case 1:
                    Console.WriteLine("You choose to update Customer name, Please write the new name:");
                    order.CustomerName = Console.ReadLine(); ;
                    break;
                case 2:
                    Console.WriteLine("You choose to update order details, Please write the new details:");
                    order.Details = Console.ReadLine();
                    break;
                case 3:
                    Console.WriteLine("You choose to update Total sum, Please write the new total sum:");
                    order.TotalSum = double.Parse(Console.ReadLine());
                    break;
                case 4:
                    Console.WriteLine("You choose to exit!");
                    break;
                default:
                    Console.WriteLine("Wrong input!!");
                    break;
            }
        }
        public void MainRun()
        {

            int legalInput;
            while (!s_quitOption)
            {
                try
                {
                    Console.Clear();
                    ShowMainMenu();
                    legalInput = int.Parse(Console.ReadLine());
                    Console.WriteLine("We recived your choice!");
                    ChooseOptionMainMenu(legalInput);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ilegal format input. please try again");
                }
                Console.WriteLine("Press any key to continue.");
                Console.ReadLine();
            }
        }
        private static void ShowMainMenu()
        {
            Console.WriteLine(c_MainMenu);
        }
        private static void ChooseOptionMainMenu(int i_Choice)
        {
            switch (i_Choice)
            {
                case 1:
                    CreateOrderUI();
                    break;
                case 2:
                    GetOrderByOrderId();
                    break;
                case 3:
                    DeleteOrderByOrderId();
                    break;
                case 4:
                    UpdeteOrder();
                    break;
                case 5:
                    GetAllCustomerOrdersByCustomerId();
                    break;
                case 6:
                    GetAllOrders();
                    break;
                case 7:
                    Console.WriteLine("You choose to exit!! Good Bye!!");
                    s_quitOption = true;
                    break;
                default:
                    Console.WriteLine("Wrong input!!!");
                    break;
            }
        }
        private static void CreateOrderUI()
        {
            string customerName;
            string details;
            double totalSum;

            Console.WriteLine("Please enter your name:");
            customerName = Console.ReadLine();
            Console.WriteLine("Enter details for the order:");
            details = Console.ReadLine();
            Console.WriteLine("Enter total sum of the order:");
            totalSum = double.Parse(Console.ReadLine());

            Order order = new Order(customerName, details, totalSum);
            supportInstance.CreateOrder(order);
            Console.WriteLine("Stored!");
        }
        private static void GetOrderByOrderId()
        {
            ListOrdersId();
            try
            {
                if (listOfOrdersById.Count != 0)
                {
                    Console.WriteLine("Choose the order number you want:");
                    PrintListById(listOfOrdersById);
                    legalInput = int.Parse(Console.ReadLine());

                    Order order = supportInstance.GetOrder(listOfOrdersById[legalInput - 1]);

                    Console.WriteLine(order.ToString());
                }
                else
                {
                    Console.WriteLine("Order Not found!!!");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Ilegal format input!");
            }
        }
        private static void GetAllCustomerOrdersByCustomerId()
        {
            ListOrdersId();
            try
            {
                if (listOfCustomersId.Count != 0)
                {
                    Console.WriteLine("Choose the order number you want:");
                    PrintListById(listOfCustomersId);
                    int numberOfOrderInList = int.Parse(Console.ReadLine());
                    List<Order> CustomerOrders = supportInstance.GetAllCustomerOrders(listOfCustomersId[numberOfOrderInList - 1]);
                    PrintListOfOrders(CustomerOrders);
                }
                else
                {
                    Console.WriteLine("Order Not found!!!");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Ilegal format input!!");
            }
        }
        private static void DeleteOrderByOrderId()
        {
            ListOrdersId();
            try
            {
                if (listOfOrdersById.Count != 0)
                {
                    Console.WriteLine("Choose the order number you want to delete:");
                    PrintListById(listOfOrdersById);
                    int numberOfOrderInList = int.Parse(Console.ReadLine());
                    supportInstance.DeleteOrder(listOfOrdersById[numberOfOrderInList - 1]);
                }
                else
                {
                    Console.WriteLine("Order to delete not found!");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Ilegal format input!!");
            }
        }
        private static void UpdeteOrder()
        {
            List<Order> listOfOrders = supportInstance.GetAllOrders();
            if (listOfOrders.Count != 0)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Choose the order number you want to updete, select a number between 1 to {0}:", listOfOrders.Count);
                    int numberOfOrderInList = int.Parse(Console.ReadLine());//choose number from main menu      

                    Order updateOrder = listOfOrders[numberOfOrderInList - 1];

                    ShowMenuForUpdate();
                    int typeUpdate = int.Parse(Console.ReadLine()); //choose number from update menu
                    ChooseOptionForUpdate(typeUpdate, updateOrder);

                    Console.WriteLine(supportInstance.UpdateOrder(updateOrder).ToString());

                }
                catch (FormatException)
                {
                    Console.WriteLine("Ilegal format input!!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("No matching orders found!");
            }
        }
        private static void GetAllOrders()
        {
            if (supportInstance.GetAllOrders().Count <= 0)
            {
                {
                    Console.WriteLine("Orders not foud!");
                }
            }
            else
            {
                PrintListOfOrders(supportInstance.GetAllOrders());
            }
        }
        private static void ListOrdersId()
        {
            listOfCustomersId.Clear();
            listOfOrdersById.Clear();
            foreach (Order order in supportInstance.GetAllOrders())
            {

                listOfCustomersId.Add(order.CustomerId);
                listOfOrdersById.Add(order.OrderId);
            }
        }
        private static void PrintListOfOrders(List<Order> list)
        {
            foreach (Order order in list)
            {
                Console.WriteLine(@"Order information:
                                 order id: {0},
                                 customer id : {1},
                                 customer name: {2},
                                 order ditails: {3},
                                 total sum : {4} ", order.OrderId, order.CustomerId, order.CustomerName, order.Details, order.TotalSum);
            }
        }
        private static void PrintListById(List<Guid> i_GuidOrder)
        {
            int countlistById = i_GuidOrder.Count;
            int number = countlistById;
            foreach (Guid guid in i_GuidOrder)
            {
                Console.WriteLine(@"{0}. ID = {1} ", countlistById - (number - 1), guid.ToString());
                number--;
            }
        }

    }
}