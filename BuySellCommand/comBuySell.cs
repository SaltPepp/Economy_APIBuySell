using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySellCommand
{
    class comBuySell
    {
        static void Main(string[] args)
        {
            EconomyItem SomeBlock = new EconomyItem();
            SomeBlock.SubTypeName = "SomeBlock";
            SomeBlock.BuyPrice = 4.50m;
            SomeBlock.SellPrice = 3.50m;
            ItemAPI.addItem(SomeBlock);
            Console.WriteLine("Standard foreach loop through the whole list...");
            Console.WriteLine("================================== Items: ==================================");
            foreach (EconomyItem Item in ItemAPI.getItems())
            {
                Console.WriteLine(" Item: " + ItemAPI.getHumanFriendly(Item.SubTypeName) + " Buy Price: " + Item.BuyPrice + " Sell Price: " + Item.SellPrice);
            }
            Console.WriteLine("============================================================================");
            Console.WriteLine("Gets the buy and sell price for each item in the whole list... This just proves that we can grab an item buy and sell price seperate using a command.");
            Console.WriteLine("================================== Items: ==================================");
            foreach (EconomyItem Item in ItemAPI.getItems())
            {
                Console.WriteLine(" Item: " + ItemAPI.getHumanFriendly(Item.SubTypeName) + " Buy Price: " + ItemAPI.getBuyPrice(Item).ToString() + " Sell Price: " + ItemAPI.getSellPrice(Item).ToString());
            }
            Console.WriteLine("============================================================================");
            Console.WriteLine("Get a single block named 'Some Block' buy price by string input:" + ItemAPI.getBuyPrice("Some Block"));
            Console.WriteLine("Get a single block named 'Some Block' sell price by string input:" + ItemAPI.getSellPrice("Some Block"));
            Console.WriteLine("============================================================================");
            EconomyItem AnotherBlock = new EconomyItem();
            AnotherBlock.SubTypeName = "AnotherBlock";
            AnotherBlock.BuyPrice = 1.50m;
            AnotherBlock.SellPrice = 2.50m;
            ItemAPI.addItem(AnotherBlock);
            Console.WriteLine("Get a single block named 'Another Block' buy price by EconomyItem type input (Note we add Another Block to the list here):" + ItemAPI.getBuyPrice(AnotherBlock).ToString());
            ItemAPI.removeItem(AnotherBlock);
            Console.WriteLine("Get a single block named 'Another Block' sell price by EconomyItem type input (Note we add Another Block to the list here):" + ItemAPI.getSellPrice(AnotherBlock).ToString());
            Console.WriteLine("============================================================================");
            Console.WriteLine("Press any key to exit this prototype...");
            Console.ReadKey();
        }
    }

    public class ItemAPI
    {
        static List<EconomyItem> Items = new List<EconomyItem>();
        public static void populateItems()
        {
            // populate local items list with either game items itelf or from a config.
            // If we can populate the list from the game items list itelf, in theory the mod will auto update this list.
        }
        public static List<EconomyItem> getItems()
        {
            return Items;
        }
        public static void addItem(EconomyItem Item)
        {
            Items.Add(Item);
        }
        internal static void addItem(string Name, decimal buyPrice, decimal sellPrice)
        {
            EconomyItem newItem = new EconomyItem();
            newItem.ItemName = Name;
            newItem.BuyPrice = buyPrice;
            newItem.SellPrice = sellPrice;
            addItem(newItem);
        }
        public static void removeItem(EconomyItem Item)
        {
            Items.Remove(Item);
        }
        internal static void removeItem(string ItemName)
        {
            foreach(EconomyItem Item in Items)
            {
                if (Item.ItemName == ItemName)
                {
                    removeItem(Item);
                }
            }
        }
        public static decimal getBuyPrice(EconomyItem Item)
        {
            return Item.BuyPrice;
        }
        internal static decimal getBuyPrice(string ItemName)
        {
            decimal buyPrice = 0.00m;
            foreach (EconomyItem Item in Items)
            {
                if (Item.ItemName == ItemName)
                {
                    return Item.BuyPrice;
                }
            }
            return buyPrice;
        }
        public static decimal getSellPrice(EconomyItem Item)
        {
            return Item.SellPrice;
        }
        internal static decimal getSellPrice(string ItemName)
        {
            decimal sellPrice = 0.00m;
            foreach (EconomyItem Item in Items)
            {
                if (Item.ItemName == ItemName)
                {
                    return Item.SellPrice;
                }
            }
            return sellPrice;
        }
        public static string getTypeId(EconomyItem Item)
        {
            return Item.TypeId;
        }
        internal static string getTypeId(string SubTypeName)
        {
            string TypeId = "";
            foreach (EconomyItem Item in Items)
            {
                if (Item.SubTypeName == SubTypeName)
                {
                    return Item.TypeId;
                }
            }
            return TypeId;
        }
        public static string getHumanFriendly(string value)
        {
            string HumanFriendly = "";
            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsUpper(value[i]))
                    HumanFriendly += " ";
                HumanFriendly += value[i].ToString();
            }
            HumanFriendly.Replace("_", " ");
            return HumanFriendly;
        }
    }

    public class EconomyItem
    {
        string _TypeId;
        string _SubTypeName;
        decimal _sellPrice;
        decimal _buyPrice;
        public string TypeId
        {
            get { return _TypeId; }
            set { _TypeId = value; }
        }
        public string SubTypeName
        {
            get { return _SubTypeName; }
            set { _SubTypeName = value; }
        }
        public string ItemName
        {
            get { return _SubTypeName; }
            set { _SubTypeName = value; }
        }
        public decimal SellPrice
        {
            get { return _sellPrice; }
            set { _sellPrice = value; }
        }
        public decimal BuyPrice
        {
            get { return _buyPrice; }
            set { _buyPrice = value; }
        }
    }
}
