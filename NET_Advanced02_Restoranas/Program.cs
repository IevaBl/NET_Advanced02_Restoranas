// See https://aka.ms/new-console-template for more information

using NET_Advanced02_Restoranas.Models.Item;
using NET_Advanced02_Restoranas.Models.receipt;
using NET_Advanced02_Restoranas.Models.table;
using NET_Advanced02_Restoranas.Repositories;
using NET_Advanced02_Restoranas.Services;
using System;

ItemsRepository itemRepo = new ItemsRepository();
TablesRepository tablesRepo = new TablesRepository();
OrderRepository orderRepo = new OrderRepository();
EmailSender emailSender = new EmailSender();


while (true)
{
    Console.Clear();
    Console.WriteLine("------------------------ Main menu --------------------------");
    Console.WriteLine("1. Tables");
    Console.WriteLine("2. Customer receipt");
    Console.WriteLine("3. Restorant receipt");
    Console.WriteLine("-------------------------------------------------------------");

    var key = Console.ReadKey();
    if (key.KeyChar == '1')
    {
        printTablesMenu();
    }
    else if (key.KeyChar == '2')
    {
        printAllCusomerReceitsMenu();
    }
    else if (key.KeyChar == '3')
    {
        //printRestorantReceitMenu();
    }


};


void printTablesMenu()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("------------------------ Tables -----------------------------");
        Console.WriteLine(" n - Enter table number");
        Console.WriteLine(" b - Back");
        Console.WriteLine("");

        List<Table> tables = tablesRepo.GetTables();
        foreach (Table table in tables)
        {

            bool hasActiveOrders = orderRepo.HasActiveOrder(table.TableNo);
            string tableStatus = hasActiveOrders ? "BUSY" : "FREE";
            Console.WriteLine($"  {table.TableNo}  {string.Format("{0,-12}", table.Description)} Places: {table.Places}       Status: {tableStatus}");
        }
        Console.WriteLine("");
        Console.WriteLine("-------------------------------------------------------------");

        var key = Console.ReadKey();
        if (key.KeyChar == 'b')
        {
            return;
        }
        else
        {
            int no = Int32.Parse(key.KeyChar.ToString());
            Table selectedTable = tablesRepo.FindTable(no);
            if (selectedTable == null)
            {
                Console.WriteLine("");
                Console.WriteLine($"Table {key} not found. Press any key to continue");
                Console.ReadLine();
            }
            else
            {
                printSelectedTableMenu(selectedTable);
            }
        }
    }
}


void printSelectedTableMenu(Table table)
{

    Order order = orderRepo.GetActiveOrCreateNewOrderByTableNo(table.TableNo);
    while (true) {
        Console.Clear();
        Console.WriteLine($"----- Entering order for Table {table.TableNo} ({string.Format("{0,-12}", table.Description)}) -------------");
        Console.WriteLine(" d - Select drink");
        Console.WriteLine(" m - Select meal");
        Console.WriteLine(" x - Close order");
        Console.WriteLine(" b - Back");
        Console.WriteLine("");
        Console.WriteLine("-------------------------------------------------------------");


        int itemCount = order.GetItems().Count;
        decimal sum = order.GetItems().Sum(i => i.Price);
        Console.WriteLine($"Number of orders: {itemCount}       Sum {sum}");
        Console.WriteLine("");

        foreach (Item item in order.GetItems())
        {
            printItem(item);
        }

        var key = Console.ReadKey();
        if (key.KeyChar == 'b')
        {
            return;
        }
        else if (key.KeyChar == 'd')
        {
            Item drink = readDrink();
            if (drink == null)
            {
                Console.WriteLine("Not found");
            }
            else
            {
                order.AddItem(drink);
            }
        }
        else if (key.KeyChar == 'm')
        {
            Item meal = readMeal();
            if (meal == null)
            {
                Console.WriteLine("Not found");
            }
            else
            {
                order.AddItem(meal);
            }
        }
        else if (key.KeyChar == 'x')
        {
            orderRepo.CloseOrder(order);
            Console.WriteLine("");
            Console.WriteLine($"Order has been closed. Press any key to continue");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("");
        Console.WriteLine("-------------------------------------------------------------");
    }    
}


Item readDrink()
{
    Console.Clear();
    Console.WriteLine($"------------------------- Drink selection -----------------------");
    Console.WriteLine(" i - drink ID");
    Console.WriteLine(" b - Back");
    Console.WriteLine("");

    var drinks = itemRepo.GetItems(ItemType.Drink);

    foreach (Item drink in drinks)
    {
        printItem(drink);
    }

    var key = Console.ReadLine();
    if (key == "b")
    {
        return null;
    }
    else
    {
        return itemRepo.GetItemById(Int32.Parse(key));
    }    
}

Item readMeal()
{
    return null;



}

void printItem(Item item)
{
    Console.WriteLine($"  {string.Format("{0,-5}", item.Id)} {string.Format("{0,-20}", item.Name)} Size: {string.Format("{0,-5}", item.ItemSize)} Price: {string.Format("{0,-5}", item.Price)}");
}



void printAllCusomerReceitsMenu()
{
    Console.Clear();
    Console.WriteLine("------------------------ Customers Orders -----------------------------");
    Console.WriteLine(" n - Select Order");
    Console.WriteLine(" b - Back");
    Console.WriteLine("");
    Console.WriteLine("-------------------------------------------------------------");


    while (true)
    {
        List<Order> orders = orderRepo.GetClosedOrders();
        while (true)
        {
            foreach (Order order in orders)
            {
                Console.WriteLine($" {order.OrderId} When: {order.Timestamp} Table: {order.TableNo}");
            }

            var key = Console.ReadLine();
            if (key == "b")
            {
                return;
            }
            else
            {
                Order order = orderRepo.GetOrderById(Int32.Parse(key));
                printCusomerReceitMenu(order);
            }
        }


    }

    Console.WriteLine("-------------------------------------------------------------");


}


void printCusomerReceitMenu(Order order)
{
    Console.Clear();
    Console.WriteLine($"------------------------ Order {order.OrderId} -----------------------------");
    Console.WriteLine(" p - Print receipt");
    Console.WriteLine(" s - Send via email");
    Console.WriteLine(" b - Back");
    Console.WriteLine("");
    Console.WriteLine("-------------------------------------------------------------");


    CustomerReceipt receipt = new CustomerReceipt(order);

    while (true)
    {
        var key = Console.ReadKey();
        if (key.KeyChar == 'b')
        {
            return;
        }
        else if (key.KeyChar == 'p')
        {
            Console.WriteLine(receipt.GetBody());
        }
        else if (key.KeyChar == 's')
        {
            Console.WriteLine("Enter email");
            var email = Console.ReadLine();
            emailSender.SendReceipt(email, receipt);

        }
    }
    Console.WriteLine("-------------------------------------------------------------");

}
































//drinkRepo.Print();
//foodRepo.Print();
//tablesRepo.Print();
