using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Hero
    {
        public int Gold { get; set; }
        public int Level { get; set; }
        public int Energy { get; set; }
        public string Name { get; set; }
        bool Gender { get; set; }
        int x { get; set; }
        int y { get; set; }
        public List<Item> Inventory = new List<Item>();

        public void addToInventory (Item item)
        {
            bool isInInventory = false;
            foreach (Item it in Inventory)
            {
                if (it.GetType() == item.GetType())
                {
                    isInInventory = true;
                    it.Number = it.Number + item.Number;
                    Console.WriteLine(item.GetType() + " " + it.Number);
                    break;
                }
            }

            if (!isInInventory)
            {
                Inventory.Add(item);
                Console.WriteLine("hi there :) ");
                Console.WriteLine(item.GetType() + " " + item.Number);
            }
        }

    }
}
