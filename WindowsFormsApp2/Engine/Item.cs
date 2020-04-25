using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Item
    {
     //   public int ID { get; set; }
        public string Name { get; set; }
        public string NamePlural { get; set; }
        //public int LevelRequired { get; set; }
        public int Number { get; set; }

        public Item(int number)
        {
            Number = number;
           // ID = id;
            //Name = name;
            //NamePlural = namePlural;
            //LevelRequired = levelRequired;
        }

        public class Seed : Item
        {
            public Seed(int number)
                : base(number)
            {
                Name = "Nasienie";
                NamePlural = "Nasiona";
            }
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
            public Food(int number)
                : base(number)
            {

            }
        }

        public class MushroomEdible : Food
        {
            public MushroomEdible(int number)
                : base(number)
            {
                Name = "Grzyb";
                NamePlural = "Grzyby";
            }
        }

        public class MushroomNotEdible : Food
        {
            public MushroomNotEdible(int number)
                : base(number)
            {
                Name = "Grzyb trujący";
                NamePlural = "Trujące grzyby";
            }
        }
    }

}
