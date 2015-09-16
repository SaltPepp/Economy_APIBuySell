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
            Console.WriteLine("Begin testing");
            EconomyItem SomeBlock = new EconomyItem();
            SomeBlock.SubTypeName = "SomeBlock";
            SomeBlock.BuyPrice = 4.50m;
            SomeBlock.SellPrice = 3.50m;
            ItemAPI.addItem(SomeBlock);
            EconomyItem RedBlock = new EconomyItem();
            RedBlock.SubTypeName = "RedBlock";
            RedBlock.BuyPrice = 3.50m;
            RedBlock.SellPrice = 4.90m;
            RedBlock.isBlacklisted = true;
            ItemAPI.addItem(RedBlock);
            EconomyItem BlueBlock = new EconomyItem();
            BlueBlock.SubTypeName = "BlueBlock";
            BlueBlock.BuyPrice = 3.50m;
            BlueBlock.SellPrice = 4.90m;
            BlueBlock.isBlacklisted = false;
            ItemAPI.addItem(BlueBlock);
            EconomyItem TNTBlock = new EconomyItem();
            TNTBlock.SubTypeName = "TNTBlock";
            TNTBlock.BuyPrice = 90.00m;
            TNTBlock.SellPrice = 120.00m;
            TNTBlock.isBlacklisted = true;
            ItemAPI.addItem(TNTBlock);
            Console.WriteLine("Standard foreach loop through the whole list...");
            Console.WriteLine("================================== Items: ==================================");
            foreach (EconomyItem Item in ItemAPI.getItems())
            {
                Console.WriteLine(" Item: " + ItemAPI.getHumanFriendly(Item.SubTypeName) + " Buy Price: " + Item.BuyPrice + " Sell Price: " + Item.SellPrice + " Blacklisted: " + Item.isBlacklisted.ToString());
            }
            Console.WriteLine("============================================================================");
            Console.WriteLine("Gets the buy and sell price for each item in the whole list... This just proves that we can grab an item buy and sell price seperate using a command.");
            Console.WriteLine("================================== Items: ==================================");
            foreach (EconomyItem Item in ItemAPI.getItems())
            {
                Console.WriteLine(" Item: " + ItemAPI.getHumanFriendly(Item.SubTypeName) + " Buy Price: " + ItemAPI.getBuyPrice(Item).ToString() + " Sell Price: " + ItemAPI.getSellPrice(Item).ToString() + " Blacklisted: " + ItemAPI.checkBlacklisted(Item).ToString());
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
            Console.WriteLine("Check to see if 'Red Block' is blacklisted (It should be): " + ItemAPI.checkBlacklisted("RedBlock"));
            Console.WriteLine("Check to see if 'Blue Block' is blacklisted (It should not be): " + ItemAPI.checkBlacklisted("BlueBlock"));
            Console.WriteLine("Check to see if 'TNT Block' is blacklisted (It should be): " + ItemAPI.checkBlacklisted("TNTBlock"));
            Console.WriteLine("End of testing");
            Console.WriteLine("Press any key to exit this prototype...");
            Console.ReadKey();
        }
    }

    public class ItemAPI
    {
        static List<EconomyItem> Items = new List<EconomyItem>();
        public static void populateItems()
        {
            /* Populate local items list with either game items itelf or from a config.
             * If we can populate the list from the game items list itelf, in theory the mod will auto update this list.
             * 
             * E.g. 
             * SpaceEngineersItems = Space Engineers Item List;
             * foreach(SpaceEngineersItem GameItem in SpaceEngineersItems)
             * {
             *      EconomyItem newEconomyItem = new EconomyItem();
             *      newEconomyItem.TypeId = GameItem.TypeId;
             *      newEconomyItem.SubTypeName = GameItem.SubTypeName;
             *      newEconomyItem.isBlacklisted = ItemAPI.checkBlacklisted(GameItem.SubTypeName);
             *      ItemAPI.addItem(newEconomyItem);
             * }
             */
        }
        public static List<EconomyItem> getItems()
        {
            return Items;
        }
        public static void addItem(EconomyItem Item)
        {
            Items.Add(Item);
        }
        internal static void addItem(string Name, decimal buyPrice, decimal sellPrice, bool isBlacklisted = false)
        {
            EconomyItem newItem = new EconomyItem();
            newItem.ItemName = Name;
            newItem.BuyPrice = buyPrice;
            newItem.SellPrice = sellPrice;
            newItem.isBlacklisted = isBlacklisted;
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
        public static bool checkBlacklisted(EconomyItem Item)
        {
            if (Item.isBlacklisted)
            {
                return true;
            }
            return false;
        }
        internal static bool checkBlacklisted(string SubTypeName)
        {
            foreach (EconomyItem Item in Items)
            {
                if (Item.SubTypeName == SubTypeName)
                {
                    if (Item.isBlacklisted)
                    {
                        return true;
                    }
                }
            }
            return false;
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

    public class EconomyItem : MarketStruct {}

    public class MarketStruct
    {
        string _TypeId;
        string _SubTypeName;
        decimal _sellPrice;
        decimal _buyPrice;
        int _Quantity = 0;
        bool _isBlacklisted = false;
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
        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public bool isBlacklisted
        {
            get { return _isBlacklisted; }
            set { _isBlacklisted = value; }
        }
    }
}
