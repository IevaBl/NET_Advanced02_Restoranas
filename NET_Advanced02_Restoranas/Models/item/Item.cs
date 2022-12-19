// See https://aka.ms/new-console-template for more information

using NET_Advanced02_Restoranas.Models.Item;

internal class Item
{
    public int Id { get; set; }

    public ItemType ItemType { get; set; }

    public string ItemSize { get; set; }

    public string Name { get; set; }
    public decimal Price { get; set; }


    public Item(ItemType itemType, int id, string name, string itemSize, decimal price)
    {
        Id = id;
        ItemType = itemType;
        Name = name;
        ItemSize = itemSize;
        Price = price;
    }
}