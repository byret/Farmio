using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Building
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int LevelRequired { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public List<Item> Storage = new List<Item>();
        public int NumOfItemsInStorage;

        public Building(int id, string name, int x, int y, int levelRequired = 0)
        {
            ID = id;
            Name = name;
            LevelRequired = levelRequired;
            X = x;
            Y = y;
            NumOfItemsInStorage = 0;
        }

        public int addToStorage(Item item)
        {
                //bool isInStorage = false;
                //foreach (Item it in Storage)
                //{
                //    if (it.GetType() == item.GetType())
                //    {
                //        isInStorage = true;
                //        it.Number = it.Number + item.Number;
                //        break;
                //    }
                //}

                //if (!isInStorage)
                //{
                //    if (NumOfItemsInStorage < 10)
                //    {
                //        NumOfItemsInStorage++;
                //        Storage.Add(item);
                //    }
                //    else return 1000;
                //}

            return 0;
        }
    }
}
