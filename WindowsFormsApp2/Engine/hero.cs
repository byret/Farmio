using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Engine
{
    public class Hero
    {
        public int Gold { get; set; }
        public int Level { get; set; }
        public int Energy { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        public Bitmap [] Sprite = new Bitmap [12];

        public List<Item> Inventory = new List<Item>();

        public Hero()
        {
            Gold = 0;
            Level = 0;
            Energy = 200;
            x = 700;
            y = 300;
        }
        public void SetSprite (int i)
        {
            string str;
            for (int j = 0; j < 12; j++)
            {
                str = i + "sprite" + (j + 1) + ".png";
                Console.WriteLine(str);
                Bitmap bm = new Bitmap(str);
                Sprite[j] = bm;
            }
        }
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

        public void Move (char c)
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
}
