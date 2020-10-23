using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Item : ICloneable
    {
        public string Name { get; set; }
        public string NamePlural { get; set; }
        public int Number { get; set; }
        public bool IsUsable;
        public bool IsStacking;
        public bool IsBakeable;
        public bool IsFuel;

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public Item(int number = 0)
        {
            Number = number;
            IsUsable = IsBakeable = IsFuel = false;
            IsStacking = true;
        }

        public Item()
        {
            Number = 0;
            IsUsable = false;
            IsStacking = true;
        }

            // przedmioty, które możemy stworzyć za pomocą crafting table
        public class CraftableItem : Item
        {
            public List<Item> ItemsNeededForCraft = new List<Item>();
            public CraftableItem(int number)
                : base(number)
            {
                
            }  
        }

        public class Shovel : CraftableItem
        {
            public Shovel(int number)
                : base(number)
            {
                Name = "Łopata";
                NamePlural = "Łopaty";
                Item.Wood Wood = new Item.Wood(3);
                Item.Stone Stone = new Item.Stone(2);
                ItemsNeededForCraft.Add(Wood);
                ItemsNeededForCraft.Add(Stone);
            }
        }

        public class Bucket : CraftableItem
        {
            public bool hasWater
            {
                get { return _hasWater;  }
                set
                {
                    _hasWater = value;
                    if (_hasWater)
                    {
                        Name = "Wiadro z wodą";
                        NamePlural = "Wiadra z wodą";
                    }
                }
            }
            private bool _hasWater;
            public Bucket(int number)
                : base(number)
            {
                Name = "Wiadro";
                NamePlural = "Wiadra";
                Item.Wood Wood = new Item.Wood(4);
                ItemsNeededForCraft.Add(Wood);
                _hasWater = false;
                IsFuel = true;
            }
        }

        public class FishingRod : CraftableItem
        {
            public FishingRod(int number)
                :base(number)
            {
                Name = "Wędka";
                NamePlural = "Wędki";
                Item.Wood Wood = new Item.Wood(2);
                ItemsNeededForCraft.Add(Wood);
                IsFuel = true;
            }
        }

        public class Furnace : CraftableItem
        {
            public int Index;
            public int TimeForBaking;
            public Item Raw;
            public Item Fuel;
            public Item.BakedItem Result;
            public List<Item> ItemsInFurnace;
            public int NumOfItemsInFurnace;
            public Furnace(int number)
                : base(number)
            {
                Name = "Piec";
                NamePlural = "Piec";
                Item.Stone Stone = new Item.Stone(2);
                ItemsNeededForCraft.Add(Stone);
                IsStacking = false;
                Index = 0;
                NumOfItemsInFurnace = 0;
                TimeForBaking = 10;
                ItemsInFurnace = new List<Item>();
        }

            public int AddInFurnace(Item item)
            {
                if (item.IsFuel)
                {
                    if (Fuel == null)
                        Fuel = item;
                    else if (Fuel.GetType() == item.GetType())
                    {
                        if (Fuel.Number + item.Number < 100)
                            Fuel.Number += item.Number;
                    }
                }
                else if (item.IsBakeable)
                {
                    bool isInFurnace = false;
                    foreach (Item it in ItemsInFurnace)
                    {
                        if (it.GetType() == item.GetType())
                        {
                            isInFurnace = true;
                            it.Number += item.Number;
                            break;
                        }
                    }

                    if (!isInFurnace)
                    {
                        if (NumOfItemsInFurnace < 4)
                        {
                            NumOfItemsInFurnace++;
                            ItemsInFurnace.Add(item);
                        }
                        else return 1000;
                    }
                }
                return 0;
            }
        }



        public class Wood : Item
        {
            public Wood(int number)
                : base(number)
            {
                NamePlural = Name = "Drzewno";
                IsFuel = true;
            }
        }

        public class Stone : Item
        {
            public Stone(int number)
                : base(number)
            {
                Name = "Kamień";
                NamePlural = "Kamienie";
            }
        }



        public class Food : Item
        {
            public int Energy; 
            public Food(int number)
                : base(number)
            {
                IsUsable = IsBakeable = true;
            }
        }

        public class MushroomEdible : Food
        {
            public MushroomEdible(int number)
                : base(number)
            {
                Energy = 10;
                Name = "Grzyb";
                NamePlural = "Grzyby";
            }
        }

        public class MushroomNotEdible : Food
        {
            public MushroomNotEdible(int number)
                : base(number)
            {
                Energy = -195;
                Name = "Trując. grz.";
                NamePlural = "Trując. grz.";
            }
        }

        public class Corn : Food
        {
            public Corn(int number)
                : base(number)
            {
                Name = NamePlural = "Kukurydza";
            }
        }

        public class Carrot : Food
        {
            public Carrot(int number)
                : base(number)
            {
                Name = NamePlural = "Marchew";
            }
        }

        public class Turnip : Food
        {
            public Turnip(int number)
                : base(number)
            {
                Name = NamePlural = "Rzepa";
            }
        }

        public class Beet : Food
        {
            public Beet(int number)
                : base(number)
            {
                Name = "Burak";
                NamePlural = "Buraki";
            }
        }

        public class Apple : Food
        {
            public Apple(int number)
                : base(number)
            {
                Name = "Jabłko";
                NamePlural = "Jabłka";
            }
        }

        public class Wheat : Food
        {
            public Wheat(int number)
                : base(number)
            {
                Name = NamePlural = "Pszenica";
            }
        }

        public class Cabbage : Food
        {
            public Cabbage(int number)
                : base(number)
            {
                Name = "Kapusta";
                NamePlural = "Kapusty";
            }
        }

        public class Cucumber : Food
        {
            public Cucumber(int number)
                : base(number)
            {
                Name = "Ogórek";
                NamePlural = "Ogórki";
            }
        }

        public class Tomato : Food
        {
            public Tomato(int number)
                : base(number)
            {
                Name = "Pomidor";
                NamePlural = "Pomidory";
            }
        }

        public class Eggplant : Food
        {
            public Eggplant(int number)
                : base(number)
            {
                Name = "Bakłażan";
                NamePlural = "Bakłażany";
            }
        }

        public class Watermelon : Food
        {
            public Watermelon(int number)
                : base(number)
            {
                Name = "Arbuz";
                NamePlural = "Arbuzy";
            }
        }

        public class Pear : Food
        {
            public Pear(int number)
                : base(number)
            {
                Name = "Gruszka";
                NamePlural = "Gruszki";
            }
        }

        public class Cherry : Food
        {
            public Cherry(int number)
                : base(number)
            {
                Name = "Wiśnia";
                NamePlural = "Wiśnie";
            }
        }

        public class Pumpkin : Food
        {
            public Pumpkin(int number)
                : base(number)
            {
                Name = "Dynia";
                NamePlural = "Dynie";
            }
        }

        public class Peach : Food
        {
            public Peach(int number)
                : base(number)
            {
                Name = "Brzoskwinia";
                NamePlural = "Brzoskwinie";
            }
        }


        public class Fish : Food
        {
            public int id;
            public int Frequency;
            public Fish(int number)
                : base(number)
            {
                id = 0;
                Energy = 5;
                Frequency = 0;
            }
        }

        public class Carp : Fish
        {
            public Carp(int number)
                : base(number)
            {
                id = 1;
                Name = "Karp";
                NamePlural = "Karpie";
                Frequency = 10;
            }
        }

        public class GoldenCarp : Fish
        {
            public GoldenCarp(int number)
                : base(number)
            {
                id = 2;
                Name = "Złoty karp";
                NamePlural = "Złote karpie";
                Frequency = 1;
            }
        }

        public class Pike : Fish
        {
            public Pike(int number)
                : base(number)
            {
                id = 3;
                Name = "Szczupak";
                NamePlural = "Szczupaki";
                Frequency = 13;
            }
        }

        public class Perch : Fish
        {
            public Perch(int number)
                : base(number)
            {
                id = 4;
                Name = "Okoń";
                NamePlural = "Okonie";
                Frequency = 20;
            }
        }

        public class Asp : Fish
        {
            public Asp(int number)
                : base(number)
            {
                id = 5;
                Name = "Boleń";
                NamePlural = "Bolenie";
                Frequency = 15;
            }
        }

        public class Catfish : Fish
        {
            public Catfish(int number)
                : base(number)
            {
                id = 6;
                Name = "Sum";
                NamePlural = "Sumy";
                Frequency = 8;
            }
        }

        public class Roach : Fish
        {
            public Roach(int number)
                : base(number)
            {
                id = 7;
                Name = "Płotka";
                NamePlural = "Płotki";
                Frequency = 20;
            }
        }

        public class Bream : Fish
        {
            public Bream(int number)
                : base(number)
            {
                id = 8;
                Name = "Leszcz";
                NamePlural = "Leszcze";
                Frequency = 20;
            }
        }


        public class Seed : Food
        {
            public int id;
            public int id2;
            public Seed(int number)
                : base(number)
            {
                id = id2 = 0;
                Energy = 1;
            }
        }
        
        public class WheatSeed : Seed
        {
            public WheatSeed(int number)
                : base(number)
            {
                id = 2;
                id2 = 1;
                Name = "Ziarno pszenicy";
                NamePlural = "Ziarna pszenicy";
            }
        }

        public class CornSeed : Seed
        {
            public CornSeed(int number)
                : base(number)
            {
                Name = "Ziarno kukurydzy";
                NamePlural = "Ziarna kukurydzy";
                Energy = 2;
            }
        }

        public class CarrotSeed : Seed
        {
            public CarrotSeed(int number)
                : base(number)
            {
                id = 1;
                id2 = 1;
                Name = "Nasienie marchwi";
                NamePlural = "Nasiona marchwi";
            }
        }

        public class TurnipSeed : Seed
        {
            public TurnipSeed(int number)
                : base(number)
            {
                id = 1;
                id2 = 2;
                Name = "Nasienie rzepy";
                NamePlural = "Nasiona rzepy";
            }
        }

        public class BeetSeed : Seed
        {
            public BeetSeed(int number)
                : base(number)
            {
                id = 1;
                id2 = 3;
                Name = "Nasienie buraka";
                NamePlural = "Nasiona buraka";
            }
        }

        public class CucumberSeed : Seed
        {
            public CucumberSeed(int number)
                : base(number)
            {
                Name = "Nasienie ogórka";
                NamePlural = "Nasiona ogórka";
            }
        }

        public class PumpkinSeed : Seed
        {
            public PumpkinSeed(int number)
                : base(number)
            {
                Name = "Nasienie dyni";
                NamePlural = "Nasiona dyni";
                Energy = 2;
            }
        }

        public class AppleSeed : Seed
        {
            public AppleSeed(int number)
                : base(number)
            {
                Name = "Nasienie jabłka";
                NamePlural = "Nasiona jabłka";
            }
        }

        public class CabbageSeed : Seed
        {
            public CabbageSeed(int number)
                : base(number)
            {
                id = 3;
                id2 = 2;
                Name = "Nasienie kapusty";
                NamePlural = "Nasiona kapusty";
            }
        }

        public class Potato : Seed
        {
            public Potato(int number)
                : base(number)
            {
                Energy = 10;
                id = 3;
                id2 = 1;
                Name = "Ziemniak";
                NamePlural = "Ziemniaki";
            }
        }

            // jedzenie, które może być wykorzystane w piecu
        public class BakedItem : Item
        {
            public List<Item> ItemsNeededForBaking = new List<Item>();
            public int TimeForBaking;
            public BakedItem(int number)
                : base(number)
            {
                TimeForBaking = 50;
            }
        }

            // rezultat pieczenia tych czy innych BakedItem, czyli jedzenie, stworzone za pomocą pieca
        public class BakedFood : BakedItem
        {
            public int Energy;
            public BakedFood(int number)
                : base(number)
            {
                IsUsable = true;
                TimeForBaking = 20;
            }
        }

        public class RoastedCorn : BakedFood
        {
            public RoastedCorn(int number)
                : base(number)
            {
                Energy = 30;
                Name = NamePlural = "Pieczona kukurydza";
                Item.Food.Corn corn = new Item.Food.Corn(1);
                ItemsNeededForBaking.Add(corn);
            }
        }

        public class RoastedCarrot : BakedFood
        {
            public RoastedCarrot(int number)
                : base(number)
            {
                Energy = 30;
                Name = "Pieczona marchew";
                NamePlural = "Pieczone marchewki";
                Item.Food.Carrot carrot = new Item.Food.Carrot(1);
                ItemsNeededForBaking.Add(carrot);
            }
        }

        public class RoastedMushroomEdible : BakedFood
        {
            public RoastedMushroomEdible(int number)
                : base(number)
            {
                Energy = 20;
                Name = "Pieczony grzyb";
                NamePlural = "Grzyby pieczone";
                Item.Food.MushroomEdible mushroomEdible = new Item.Food.MushroomEdible(1);
                ItemsNeededForBaking.Add(mushroomEdible);
            }
        }

        public class RoastedMushroomNotEdible : BakedFood
        {
            public RoastedMushroomNotEdible(int number)
                : base(number)
            {
                Energy = -50;
                Name = "Grzyb truj. pieczony";
                NamePlural = "Grzyby truj. pieczone";
                Item.Food.MushroomNotEdible mushroomNotEdible = new Item.Food.MushroomNotEdible(1);
                ItemsNeededForBaking.Add(mushroomNotEdible);
            }
        }

        public class RoastedTurnip : BakedFood
        {
            public RoastedTurnip(int number)
                : base(number)
            {
                Energy = 30;
                Name = NamePlural = "Pieczona rzepa";
                Item.Food.Turnip turnip = new Item.Food.Turnip(1);
                ItemsNeededForBaking.Add(turnip);
            }
        }

        public class RoastedBeet : BakedFood
        {
            public RoastedBeet(int number)
                : base(number)
            {
                Energy = 30;
                Name = "Pieczony burak";
                NamePlural = "Pieczone buraki";
                Item.Food.Beet beet = new Item.Food.Beet(1);
                ItemsNeededForBaking.Add(beet);
            }
        }

        public class RoastedApple : BakedFood
        {
            public RoastedApple(int number)
                : base(number)
            {
                Energy = 30;
                Name = "Pieczone jabłko";
                NamePlural = "Pieczone jabłka";
                Item.Food.Apple apple = new Item.Food.Apple(1);
                ItemsNeededForBaking.Add(apple);
            }
        }

        public class RoastedCabbage : BakedFood
        {
            public RoastedCabbage(int number)
                : base(number)
            {
                Energy = 30;
                Name = NamePlural = "Pieczona kapusta";
                Item.Food.Cabbage cabbage = new Item.Food.Cabbage(1);
                ItemsNeededForBaking.Add(cabbage);
            }
        }

        public class RoastedCucumber : BakedFood
        {
            public RoastedCucumber(int number)
                : base(number)
            {
                Energy = 30;
                Name = "Pieczony ogórek";
                NamePlural = "Pieczone ogórki";
                Item.Food.Cucumber cucumber = new Item.Food.Cucumber(1);
                ItemsNeededForBaking.Add(cucumber);
            }
        }

        public class RoastedTomato : BakedFood
        {
            public RoastedTomato(int number)
                : base(number)
            {
                Energy = 30;
                Name = "Pieczony pomidor";
                NamePlural = "Pieczone pomidory";
                Item.Food.Tomato tomato = new Item.Food.Tomato(1);
                ItemsNeededForBaking.Add(tomato);
            }
        }

        public class RoastedEggplant : BakedFood
        {
            public RoastedEggplant(int number)
                : base(number)
            {
                Energy = 30;
                Name = "Pieczony bakłażan";
                NamePlural = "Pieczone bakłażany";
                Item.Food.Eggplant eggplant = new Item.Food.Eggplant(1);
                ItemsNeededForBaking.Add(eggplant);
            }
        }

        public class RoastedPear : BakedFood
        {
            public RoastedPear(int number)
                : base(number)
            {
                Energy = 30;
                Name = "Pieczona gruszka";
                NamePlural = "Pieczone gruszki";
                Item.Food.Pear pear = new Item.Food.Pear(1);
                ItemsNeededForBaking.Add(pear);
            }
        }

        public class RoastedPumpkin : BakedFood
        {
            public RoastedPumpkin(int number)
                : base(number)
            {
                Energy = 30;
                Name = "Pieczona dynia";
                NamePlural = "Pieczone dynie";
                Item.Food.Pumpkin pumpkin = new Item.Food.Pumpkin(1);
                ItemsNeededForBaking.Add(pumpkin);
            }
        }

        public class RoastedPeach : BakedFood
        {
            public RoastedPeach(int number)
                : base(number)
            {
                Energy = 30;
                Name = "Pieczona brzoskwinia";
                NamePlural = "Pieczone brzoskwinie";
                Item.Food.Peach peach = new Item.Food.Peach(1);
                ItemsNeededForBaking.Add(peach);
            }
        }

        public class BakedCarp : BakedFood
        {
            public BakedCarp(int number)
                : base(number)
            {
                Energy = 30;
                Name = "Pieczony karp";
                NamePlural = "Pieczone karpie";
                Item.Food.Fish.Carp carp = new Item.Food.Fish.Carp(1);
                ItemsNeededForBaking.Add(carp);
            }
        }

        public class BakedGoldenCarp : BakedFood
        {
            public BakedGoldenCarp(int number)
                : base(number)
            {
                Energy = 200;
                Name = "Pieczony karp zł.";
                NamePlural = "Pieczone karpie zł.";
                Item.Food.Fish.GoldenCarp goldenCarp = new Item.Food.Fish.GoldenCarp(1);
                ItemsNeededForBaking.Add(goldenCarp);
            }
        }

        public class BakedPike : BakedFood
        {
            public BakedPike(int number)
                : base(number)
            {
                Energy = 25;
                Name = "Pieczony szczupak";
                NamePlural = "Pieczone szczupaki";
                Item.Food.Fish.Pike pike = new Item.Food.Fish.Pike(1);
                ItemsNeededForBaking.Add(pike);
            }
        }

        public class BakedPerch : BakedFood
        {
            public BakedPerch(int number)
                : base(number)
            {
                Energy = 20;
                Name = "Pieczony okoń";
                NamePlural = "Pieczone okonie";
                Item.Food.Fish.Perch perch = new Item.Food.Fish.Perch(1);
                ItemsNeededForBaking.Add(perch);
            }
        }

        public class BakedAsp : BakedFood
        {
            public BakedAsp(int number)
                : base(number)
            {
                Energy = 30;
                Name = "Pieczony boleń";
                NamePlural = "Pieczone bolenie";
                Item.Food.Fish.Asp asp = new Item.Food.Fish.Asp(1);
                ItemsNeededForBaking.Add(asp);
            }
        }

        public class BakedCatfish : BakedFood
        {
            public BakedCatfish(int number)
                : base(number)
            {
                Energy = 35;
                Name = "Pieczony sum";
                NamePlural = "Pieczone sumy";
                Item.Food.Fish.Catfish catfish = new Item.Food.Fish.Catfish(1);
                ItemsNeededForBaking.Add(catfish);
            }
        }

        public class BakedRoach : BakedFood
        {
            public BakedRoach(int number)
                : base(number)
            {
                Energy = 20;
                Name = "Pieczona płotka";
                NamePlural = "Pieczone płotki";
                Item.Food.Fish.Roach roach = new Item.Food.Fish.Roach(1);
                ItemsNeededForBaking.Add(roach);
            }
        }

        public class BakedBream : BakedFood
        {
            public BakedBream(int number)
                : base(number)
            {
                Energy = 20;
                Name = "Pieczony leszcz";
                NamePlural = "Pieczone leszcze";
                Item.Food.Fish.Bream bream = new Item.Food.Fish.Bream(1);
                ItemsNeededForBaking.Add(bream);
            }
        }

        public class BakedPotato : BakedFood
        {
            public BakedPotato(int number)
                : base(number)
            {
                Energy = 30;
                Name = "Pieczona ziemniak";
                NamePlural = "Pieczone ziemniaki";
                Item.Food.Potato potato = new Item.Food.Potato(1);
                ItemsNeededForBaking.Add(potato);
            }
        }

    }
}
