// See https://aka.ms/new-console-template for more information

using NET_Advanced02_Restoranas.Models;
using NET_Advanced02_Restoranas;
using NET_Advanced02_Restoranas.Models.table;

internal class OrderRepository
{
    List<Order> Orders { get; set; }

    public OrderRepository()
    {
        Orders = new List<Order>();

    }

    internal Order GetActiveOrCreateNewOrderByTableNo(int tableNo)
    {
        Order order = Orders.Find(o => o.TableNo == tableNo && o.OrderState == OrderState.Active);
        if (order == null)
        {
            order = new Order();
            order.OrderId = Orders.Count + 1;
            order.Timestamp = DateTime.Now;
            order.TableNo = tableNo;
            order.OrderState = OrderState.Active;

            Orders.Add(order);
            return order;
        }

        return order;
    }

    internal void CloseOrder(Order order)
    {
        order.OrderState = OrderState.Closed;
    }

    internal void AddItem(Order order, Item item)
    {
        order.AddItem(item);
    }

    internal bool HasActiveOrder(int tableNo)
    {
        var activeOrders = Orders.Where(o => o.TableNo == tableNo && o.OrderState == OrderState.Active).Count();

        return activeOrders > 0;
    }

    internal List<Order> GetClosedOrders()
    {
        return Orders.FindAll(o => o.OrderState == OrderState.Closed).ToList();
    }

    internal Order GetOrderById(int orderId)
    {
        return Orders.Find(o => o.OrderId == orderId);
    }
}
