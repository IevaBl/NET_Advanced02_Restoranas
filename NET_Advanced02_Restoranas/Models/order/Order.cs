// See https://aka.ms/new-console-template for more information

using NET_Advanced02_Restoranas.Models.table;

internal class Order
{

    List<Item> Items { get; set; }

    public int OrderId { get; set; }
    public DateTime Timestamp { get; set; }

    public int TableNo { get; set; }

    public OrderState OrderState { get; set; }

    public Order()
    {
        Items = new List<Item>();
    }

    internal List<Item> GetItems()
    {
        return Items;
    }

    internal void AddItem(Item item)
    {
        Items.Add(item);

    }
}