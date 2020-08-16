using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;

namespace Engine
{

    public class NPC
    {
        public int Index { get; set; }
        public int Gold { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public Bitmap[] Sprite = new Bitmap[12];
        public List<Item> Inventory = new List<Item>();
        public int NumOfItemsInInventory;

        public NPC()
        {
            Gold = 0;
            x = 0;
            y = 0;
            NumOfItemsInInventory = 0;
        }

        public void SetSprite(int i)
        {
            string str;
            for (int j = 0; j < 12; j++)
            {
                Bitmap bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources", (i + "sprite" + (j + 1) + ".png")));
                Sprite[j] = bm;
            }
        }
        public void Move(char c)
        {
            if (c == 'w')
                this.y--;
            else if (c == 's')
                this.y++;
            else if (c == 'a')
                this.x--;
            else if (c == 'd')
                this.x++;
        }
    }

    public class Hero : NPC
    {
        public int Day { get; set; }
        public int Energy { get; set; }
        public int Saturation { get; set; }
        public List<Item.CraftableItem> ListOfCraftableItems = new List<Item.CraftableItem>();
        public List<Item.BakedItem> BakeableItems = new List<Item.BakedItem>();

        public Hero()
        {
            Gold = 0;
            Day = 0;
            Energy = 200;
            x = 700;
            y = 300;
            Saturation = 50;
            NumOfItemsInInventory = 0;
            Index = 1;

            {
                Item.CraftableItem.Shovel shovel = new Item.Shovel(1);
                ListOfCraftableItems.Add(shovel);

                Item.CraftableItem.Bucket bucket = new Item.Bucket(1);
                ListOfCraftableItems.Add(bucket);

                Item.CraftableItem.FishingRod fishingRod = new Item.FishingRod(1);
                ListOfCraftableItems.Add(fishingRod);

                Item.CraftableItem.Furnace furnace = new Item.Furnace(1);
                ListOfCraftableItems.Add(furnace);
            }

            {
                Item.BakedItem.BakedFood.RoastedCorn roastedCorn = new Item.BakedItem.BakedFood.RoastedCorn(1);
                BakeableItems.Add(roastedCorn);

                Item.BakedItem.BakedFood.RoastedCarrot roastedCarrot = new Item.BakedItem.BakedFood.RoastedCarrot(1);
                BakeableItems.Add(roastedCarrot);

                Item.BakedItem.BakedFood.RoastedMushroomEdible roastedMushroomEdible = new Item.BakedItem.BakedFood.RoastedMushroomEdible(1);
                BakeableItems.Add(roastedMushroomEdible);

                Item.BakedItem.BakedFood.RoastedMushroomNotEdible roastedMushroomNotEdible = new Item.BakedItem.BakedFood.RoastedMushroomNotEdible(1);
                BakeableItems.Add(roastedMushroomNotEdible);

                Item.BakedItem.BakedFood.RoastedTurnip roastedTurnip = new Item.BakedItem.BakedFood.RoastedTurnip(1);
                BakeableItems.Add(roastedTurnip);

                Item.BakedItem.BakedFood.RoastedBeet roastedBeet = new Item.BakedItem.BakedFood.RoastedBeet(1);
                BakeableItems.Add(roastedBeet);

                Item.BakedItem.BakedFood.RoastedApple roastedApple = new Item.BakedItem.BakedFood.RoastedApple(1);
                BakeableItems.Add(roastedApple);

                Item.BakedItem.BakedFood.RoastedCabbage roastedCabbage = new Item.BakedItem.BakedFood.RoastedCabbage(1);
                BakeableItems.Add(roastedCabbage);

                Item.BakedItem.BakedFood.RoastedCucumber roastedCucumber = new Item.BakedItem.BakedFood.RoastedCucumber(1);
                BakeableItems.Add(roastedCucumber);

                Item.BakedItem.BakedFood.RoastedTomato roastedTomato = new Item.BakedItem.BakedFood.RoastedTomato(1);
                BakeableItems.Add(roastedTomato);

                Item.BakedItem.BakedFood.RoastedEggplant roastedEggplant = new Item.BakedItem.BakedFood.RoastedEggplant(1);
                BakeableItems.Add(roastedEggplant);

                Item.BakedItem.BakedFood.RoastedPear roastedPear = new Item.BakedItem.BakedFood.RoastedPear(1);
                BakeableItems.Add(roastedPear);

                Item.BakedItem.BakedFood.RoastedPumpkin roastedPumpkin = new Item.BakedItem.BakedFood.RoastedPumpkin(1);
                BakeableItems.Add(roastedPumpkin);

                Item.BakedItem.BakedFood.RoastedPeach roastedPeach = new Item.BakedItem.BakedFood.RoastedPeach(1);
                BakeableItems.Add(roastedPeach);

                Item.BakedItem.BakedFood.BakedCarp bakedCarp = new Item.BakedItem.BakedFood.BakedCarp(1);
                BakeableItems.Add(bakedCarp);

                Item.BakedItem.BakedFood.BakedGoldenCarp bakedGoldenCarp = new Item.BakedItem.BakedFood.BakedGoldenCarp(1);
                BakeableItems.Add(bakedGoldenCarp);

                Item.BakedItem.BakedFood.BakedPike bakedPike = new Item.BakedItem.BakedFood.BakedPike(1);
                BakeableItems.Add(bakedPike);

                Item.BakedItem.BakedFood.BakedPerch bakedPerch = new Item.BakedItem.BakedFood.BakedPerch(1);
                BakeableItems.Add(bakedPerch);

                Item.BakedItem.BakedFood.BakedAsp bakedAsp = new Item.BakedItem.BakedFood.BakedAsp(1);
                BakeableItems.Add(bakedAsp);

                Item.BakedItem.BakedFood.BakedCatfish bakedCatfish = new Item.BakedItem.BakedFood.BakedCatfish(1);
                BakeableItems.Add(bakedCatfish);

                Item.BakedItem.BakedFood.BakedRoach bakedRoach = new Item.BakedItem.BakedFood.BakedRoach(1);
                BakeableItems.Add(bakedRoach);

                Item.BakedItem.BakedFood.BakedBream bakedBream = new Item.BakedItem.BakedFood.BakedBream(1);
                BakeableItems.Add(bakedBream);

                Item.BakedItem.BakedFood.BakedPotato bakedPotato = new Item.BakedItem.BakedFood.BakedPotato(1);
                BakeableItems.Add(bakedPotato);
            }
        }

        public int AddToInventory (Item item)
        {
            if (item.Number > 0)
            {
                bool isInInventory = false;
                if (item.IsStacking)
                {
                    foreach (Item it in Inventory)
                    {
                        if (it.GetType() == item.GetType())
                        {
                            isInInventory = true;
                            it.Number = it.Number + item.Number;
                            break;
                        }
                    }
                }

                if (!isInInventory)
                {
                    if (NumOfItemsInInventory < 5)
                    {
                        NumOfItemsInInventory++;
                        Inventory.Add(item);
                    }
                    else return 1000;
                }
            }
            return item.Number;
        }

    }
}
