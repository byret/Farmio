using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Item : ICloneable
    {
     //   public int ID { get; set; }
        public string Name { get; set; }
        public string NamePlural { get; set; }
        //public int LevelRequired { get; set; }
        public int Number { get; set; }
        public bool IsUsable;

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public Item(int number = 0)
        {
            Number = number;
            IsUsable = false;
        }
        public Item()
        {
            Number = 0;
            IsUsable = false;
        }

        public class Wood : Item
        {
            public Wood(int number)
                : base(number)
            {
                NamePlural = Name = "Drzewno";
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
                IsUsable = true;
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

        public class Apple : Food
        {
            public Apple(int number)
                : base(number)
            {
                Name = "Jabłko";
                NamePlural = "Jabłka";
            }
        }

        public class Potato : Food
        {
            public Potato(int number)
                : base(number)
            {
                Name = "Ziemniak";
                NamePlural = "Ziemniaki";
            }
        }

        public class Rice : Food
        {
            public Rice(int number)
                : base(number)
            {
                Name = NamePlural = "Ryż";
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
            public Fish(int number)
                : base(number)
            {
                Name = "Ryba";
                NamePlural = "Ryby";
            }
        }

        public class Seed : Food
        {
            public Seed(int number)
                : base(number)
            {
                Energy = 1;
            }
        }
        
        public class WheatSeed : Seed
        {
            public WheatSeed(int number)
                : base(number)
            {
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
                Name = "Nasienie marchwi";
                NamePlural = "Nasiona marchwi";
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
    }

}
