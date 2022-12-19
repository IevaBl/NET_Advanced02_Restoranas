using NET_Advanced02_Restoranas.Models;
using NET_Advanced02_Restoranas.Models.Item;
using System;

namespace NET_Advanced02_Restoranas.Repositories
{
    internal class ItemsRepository
    {
       List<Item> Items { get; set; }

        public ItemsRepository()
        {
            Items = new List<Item> ();
            Items.AddRange(LoadDrinks());
            Items.AddRange(LoadMeals());

        }

        private List<Item> LoadDrinks()
        {
            var drinks = new List<Item>();
            string[] arr = File.ReadAllLines(@"C:\Users\ievau\source\repos\C# ADVANCED\NET_Advanced02_Restoranas\NET_Advanced02_Restoranas\bin\Drinks.txt");

            foreach (string drink in arr)
            {
                List<string> values = drink.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                int id = Int32.Parse(values[0]);
                string name = values[1];
                string size = values[2];
                decimal price = Convert.ToDecimal(values[3]);

                drinks.Add(new Item(Models.Item.ItemType.Drink, id, name, size, price));
            }

            return drinks;
        }

        private List<Item> LoadMeals()
        {
            var meals = new List<Item>();
            string[] foods = File.ReadAllLines(@"C:\Users\ievau\source\repos\C# ADVANCED\NET_Advanced02_Restoranas\NET_Advanced02_Restoranas\bin\Food.txt");

            foreach (string food in foods)
            {
                List<string> values = food.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                int id = Int32.Parse(values[0]);
                string name = values[1];
                decimal price = Convert.ToDecimal(values[2]);

                meals.Add(new Item(Models.Item.ItemType.Meal, id, name,"M", price));

            }

            return meals;

        }

        internal List<Item>? GetItems(ItemType itemType)
        {
            return Items.FindAll(i => i.ItemType == itemType);
        }

        internal Item? GetItemById(int id)
        {
            return Items.Find(i => i.Id == id);
        }
    }
}
