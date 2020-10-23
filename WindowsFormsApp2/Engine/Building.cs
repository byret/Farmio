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
    }
}
