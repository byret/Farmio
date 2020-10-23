using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;
using System.Timers;

namespace Engine
{
    public class Map
    {
        public Bitmap Trees = new Bitmap(1300, 805);
        public MapPoint[,] mapTab = new MapPoint[1700, 900];

        // każdy "zajęty" punkt na mapie
        public class MapPoint
        {
            public int X;       // wskazują na MainPoint,
            public int Y;       // do którego należą
            public bool Collision;

            public MapPoint(int x, int y, bool collision = false)
            {
                X = x;
                Y = y;
                Collision = collision;
            }
        }

        // przechowuje całą informację o obiekcie
        public class MapMainPoint : MapPoint    // najbardziej górny z lewej strony 
        {                                      // czyli punkt o najmniejszych X i Y danego obiektu 
            public string Name { get; set; }
            //public int ID { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public int Weight { get; set; }
            public Bitmap Bitmap;
            public Item loot { get; set; }
            public int LootMax { get; set; }
            public int LootMin { get; set; }
            public MapMainPoint(int x, int y, string name, int width, int height, Bitmap bm, bool Collision = false)
                : base(x, y, Collision)
            {
                Name = name;
                X = x;
                Y = y;
                Width = width;
                Height = height;
                Bitmap = bm;
            }

            public class Grass : MapMainPoint
            {
                public Grass(int x, int y, string name, int Width, int Height, Bitmap bm, Map map)
                    : base(x, y, name, Width, Height, bm)
                {
                    map.Fill(x, y, Width, Height, 0, 0, 0, 0);
                    Random random = new Random();
                    if (name == "g9")
                        Weight = random.Next(3, 4);
                    else
                        Weight = random.Next(1, 2);
                }

                public Item.Seed DestroyGrass(Map map)
                {
                    Bitmap = null;
                    map.reFill(X, Y, Width, Height);
                    int numOfSeeds = Weight * 2;
                    Random random = new Random();
                    int caseSwitch = random.Next(1, 15);
                    Item.Seed seed;
                    switch (caseSwitch)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            seed = new Item.Seed.WheatSeed(numOfSeeds);
                            break;
                        case 6:
                        case 7:
                            seed = new Item.Seed.CornSeed(numOfSeeds);
                            break;
                        case 8:
                            seed = new Item.Seed.CarrotSeed(numOfSeeds);
                            break;
                        case 9:
                            seed = new Item.Seed.CucumberSeed(numOfSeeds);
                            break;
                        case 10:
                            seed = new Item.Seed.PumpkinSeed(numOfSeeds / 2);
                            break;

                        default:
                            seed = new Item.Seed(0);
                            break;
                    }
                    return seed;
                }
            }

            public class Tree : MapMainPoint
            {

                public Tree(int x, int y, string name, int Width, int Height, Bitmap bm, Map map)
                    : base(x, y, name, Width, Height, bm)
                {
                    Random random = new Random();
                    if (name[1] == '1')
                    {
                        Weight = random.Next(9, 16);
                        map.Fill(x, y, Width, Height, 0, 41, 53, 74);
                    }
                    else if (name[1] == '2')
                    {
                        Weight = random.Next(25, 30);
                        map.Fill(x, y, Width, Height, 0, 96, 53, 74);
                    }
                    else if (name[1] == '3')
                    {
                        Weight = random.Next(8, 14);
                        map.Fill(x, y, Width, Height, 0, 28, 68, 92);
                    }
                    else if (name[1] == '4')
                    {
                        Weight = random.Next(27, 33);
                        map.Fill(x, y, Width, Height, 0, 89, 68, 92);
                    }
                }

                public Item.Wood DestroyTree(Map map)
                {
                    Bitmap = null;
                    map.reFill(X, Y, Width, Height);
                    if (Name[1] == '1')
                        map.MakeObjectRightHere(X, Y + 52, "p1", map, true);
                    else if (Name[1] == '3')
                        map.MakeObjectRightHere(X, Y + 68, "p2", map, true);
                    else if (Name[1] == '2')
                        map.MakeObjectRightHere(X, Y + 52, "p3", map, true);
                    else if (Name[1] == '4')
                        map.MakeObjectRightHere(X, Y + 68, "p4", map, true);
                    int numOfWood = Weight / 2;
                    Item.Wood wood = new Item.Wood(numOfWood);
                    return wood;

                }
            }

            public class Stone : MapMainPoint
            {
                public Stone(int x, int y, string name, int Width, int Height, Bitmap bm, Map map)
                    : base(x, y, name, Width, Height, bm)
                {
                    Random random = new Random();
                    if (Width == 28)
                    {
                        Weight = random.Next(8, 14);
                        map.Fill(x, y, Width, Height, 5, 21, 0, 12);
                    }
                    else if (Width == 24)
                    {
                        Weight = random.Next(9, 19);
                        map.Fill(x, y, Width, Height, 4, 19, 0, 14);
                    }
                    else if (Width == 46)
                    {
                        Weight = random.Next(25, 30);
                        map.Fill(x, y, Width, Height, 9, 37, 0, 29);
                    }

                }

                public Item.Stone DestroyStone(Map map)
                {
                    Bitmap = null;
                    map.reFill(X, Y, Width, Height);
                    int numOfStones = Weight / 3;
                    Item.Stone stone = new Item.Stone(numOfStones);
                    return stone;
                }
            }

            public class Stump : MapMainPoint   // 'p'
            {
                public Stump(int x, int y, string name, int Width, int Height, Bitmap bm, Map map)
                    : base(x, y, name, Width, Height, bm)
                {
                    Random random = new Random();
                    if (name[1] == '1')
                    {
                        Weight = random.Next(4, 6);
                        map.Fill(x, y, Width, Height, 21, 44, 0, 22);
                    }
                    else if (name[1] == '2')
                    {
                        Weight = random.Next(3, 5);
                        map.Fill(x, y, Width, Height, 26, 45, 5, 24);
                    }
                    else if (name[1] == '3')
                    {
                        Weight = random.Next(8, 12);
                        map.Fill(x, y, Width, Height, 21, 92, 0, 22);
                    }
                    else if (name[1] == '4')
                    {
                        Weight = random.Next(6, 10);
                        map.Fill(x, y, Width, Height, 26, 93, 5, 24);
                    }
                }

                public Item.Wood DestroyStump(Map map)
                {
                    Bitmap = null;
                    map.reFill(X, Y, Width, Height);
                    int numOfWood = Weight / 2;
                    Item.Wood wood = new Item.Wood(numOfWood);
                    return wood;
                }
            }

            public class Mushroom : MapMainPoint
            {
                public Mushroom(int x, int y, string name, int Width, int Height, Bitmap bm, Map map)
                    : base(x, y, name, Width, Height, bm)
                {
                    map.Fill(x, y, Width, Height, 0, 0, 0, 0);
                    Weight = Name[2] - '0';
                }

                public Item.MushroomEdible DestroyMushroom(Map map)
                {
                    Bitmap = null;
                    map.reFill(X, Y, Width, Height);
                    int numOfMushrooms = Weight;
                    Item.MushroomEdible mushroom = new Item.MushroomEdible(numOfMushrooms);
                    return mushroom;
                }

                public Item.MushroomNotEdible DestroyMushroomNE(Map map)
                {
                    Bitmap = null;
                    map.reFill(X, Y, Width, Height);
                    int numOfMushrooms = Weight;
                    Item.MushroomNotEdible mushroom = new Item.MushroomNotEdible(numOfMushrooms);
                    return mushroom;
                }
            }

            public class Building : MapMainPoint   // 'p'
            {
                public List<Item> Storage = new List<Item>();
                public List<Item> CraftingTable = new List<Item>();
                public int NumOfItemsInStorage;
                public int NumOfItemsOnTable;
                public Building(int x, int y, string name, int Width, int Height, Bitmap bm, Map map)
                    : base(x, y, name, Width, Height, bm)
                {
                    map.Fill(x, y, Width, Height, 0, 94, 0, 84);
                    NumOfItemsInStorage = 0;

            }

                public void DestroyBuilding(Map map)
                {
                    Bitmap = null;
                    map.reFill(X, Y, Width, Height);
                }

                public int AddToStorage(Item item)
                {
                    bool isInStorage = false;
                    if (item.IsStacking)
                    {
                        foreach (Item it in this.Storage)
                        {
                            if (it.GetType() == item.GetType())
                            {
                                isInStorage = true;
                                it.Number += item.Number;
                                break;
                            }
                        }
                    }

                    if (!isInStorage)
                    {
                        if (NumOfItemsInStorage < 10)
                        {
                            NumOfItemsInStorage++;
                            Storage.Add(item);
                        }
                        else return 1000;
                    }

                    return 0;
                }

                public int AddOnCraftingTable(Item item)
                {
                    bool isOnTable = false;
                    foreach (Item it in this.CraftingTable)
                    {
                        if (it.GetType() == item.GetType())
                        {
                            isOnTable = true;
                            it.Number += item.Number;
                            break;
                        }
                    }

                    if (!isOnTable)
                    {
                        if (NumOfItemsOnTable < 4)
                        {
                            NumOfItemsOnTable++;
                            CraftingTable.Add(item);
                        }
                        else return 1000;
                    }

                    return 0;
                }

            }

            public class PlowedSoil : MapMainPoint
            {
                private double UpdateInterval = 30000;
                public Item.Food.Seed seed;
                private System.Timers.Timer timerOfWatering;
                private System.Timers.Timer timerOfGrowth;
                public int stageOfGrowth
                {
                    get { return _stageOfGrowth; }
                    set
                    {
                        _stageOfGrowth = value;
                        string iswatered;
                        if (_stageOfWatering == 0)
                            iswatered = "Unwatered";
                        else if (_stageOfWatering == 1)
                            iswatered = "NRWatered";
                        else iswatered = "Watered";
                        if (_stageOfGrowth == 0)
                            this.Bitmap = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\PlowedSoil" + iswatered + ".png"));
                        else if (_stageOfGrowth == 1 || _stageOfGrowth == 2 || (_stageOfGrowth == 3 && seed.id != 2))
                            this.Bitmap = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\SowedSoil" + iswatered + seed.Number.ToString() + '-' + seed.id.ToString() + '-' + _stageOfGrowth.ToString() + ".png"));
                        else if (_stageOfGrowth == 4 && seed.id != 2)
                            this.Bitmap = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\SowedSoil" + iswatered + seed.Number.ToString() + '-' + seed.id.ToString() + '-' + _stageOfGrowth.ToString() + '-' + seed.id2.ToString() + ".png"));
                        else if (_stageOfGrowth == 4)
                            this.Bitmap = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\SowedSoil" + iswatered + seed.Number.ToString() + '-' + seed.id.ToString() + '-' + _stageOfGrowth.ToString() + ".png"));
                    }
                }
                public int stageOfWatering
                {
                    get { return _stageOfWatering; }
                    set
                    {
                        _stageOfWatering = value;

                        string iswatered;
                        if (_stageOfWatering == 0)
                            iswatered = "Unwatered";
                        else if (_stageOfWatering == 1)
                            iswatered = "NRWatered";
                        else iswatered = "Watered";
                        timerOfWatering.Start();

                        if (_isSowed)
                        {
                            timerOfGrowth.Start();
                            string numOfSeeds = seed.Number.ToString();
                            if (_stageOfGrowth == 1 || _stageOfGrowth == 2 || (_stageOfGrowth == 3 && seed.id != 2))
                            {
                                this.Bitmap = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\SowedSoil" + iswatered + seed.Number.ToString() + '-' + seed.id.ToString() + '-' + _stageOfGrowth.ToString() + ".png"));
                                Console.WriteLine("WindowsFormsApp2\\Resources\\SowedSoil" + iswatered + seed.Number.ToString() + '-' + seed.id.ToString() + '-' + _stageOfGrowth.ToString() + ".png");
                            }
                                
                            else if (_stageOfGrowth == 4 && seed.id != 2)
                                this.Bitmap = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\SowedSoil" + iswatered + seed.Number.ToString() + '-' + seed.id.ToString() + '-' + _stageOfGrowth.ToString() + '-' + seed.id2.ToString() + ".png"));
                            else if (_stageOfGrowth == 4)
                                this.Bitmap = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\SowedSoil" + iswatered + seed.Number.ToString() + '-' + seed.id.ToString() + '-' + _stageOfGrowth.ToString() + ".png"));
                            else if (_stageOfGrowth == 0)
                                this.Bitmap = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\PlowedSoil" + iswatered + ".png"));
                        }
                        else
                        {
                            this.Bitmap = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\PlowedSoil" + iswatered + ".png"));
                        }
                    }
                }
                public bool isSowed
                {
                    get { return _isSowed; }
                    set
                    {
                        _isSowed = value;
                        if (_isSowed)
                        {
                            string iswatered;
                            string numOfSeeds = seed.Number.ToString();
                        }
                    }
                }
                private bool _isWatered;
                private bool _isSowed;
                private int _stageOfGrowth;
                private int _stageOfWatering;

                public PlowedSoil(int x, int y, string name, int Width, int Height, Bitmap bm, Map map)
                    : base(x, y, name, Width, Height, bm)
                {
                    timerOfWatering =  new System.Timers.Timer(UpdateInterval * 2);
                    timerOfWatering.Elapsed += timerOfWateringTimer_Elapsed;
                    timerOfGrowth = new System.Timers.Timer(UpdateInterval);
                    timerOfGrowth.Elapsed += timerOfGrowthTimer_Elapsed;
                    map.Fill(x, y, Width, Height, 1, 53, 1, 40);
                    isSowed = false;
                    stageOfGrowth = 0;
                    stageOfWatering = 0;
                }

                private void timerOfWateringTimer_Elapsed(object sender, ElapsedEventArgs e)
                {
                    if (stageOfWatering > 0)
                        stageOfWatering--;
                }

                private void timerOfGrowthTimer_Elapsed(object sender, ElapsedEventArgs e)
                {
                    if (stageOfWatering > 0 && stageOfGrowth < 4)
                        stageOfGrowth++;
                }

                public Item Harvesting ()
                {
                    Item.Food result = new Item.Food(1);
                    if (seed is Item.Food.Seed.CarrotSeed)
                        result = new Item.Food.Carrot(seed.Number);
                    else if (seed is Item.Food.Seed.TurnipSeed)
                        result = new Item.Food.Turnip(seed.Number);
                    else if (seed is Item.Food.Seed.BeetSeed)
                        result = new Item.Food.Beet(seed.Number);
                    else if (seed is Item.Food.Seed.WheatSeed)
                        result = new Item.Food.Wheat(seed.Number*2);
                    else if (seed is Item.Food.Seed.Potato)
                        result = new Item.Food.Potato(seed.Number*3);
                    else if (seed is Item.Food.Seed.CabbageSeed)
                        result = new Item.Food.Cabbage(seed.Number);
                    seed = null;
                    timerOfGrowth.Stop();
                    isSowed = false;
                    stageOfGrowth = 0;
                    return result;
                }
            }

            public class Pond : MapMainPoint
            {
                public Pond(int x, int y, string name, int Width, int Height, Bitmap bm, Map map)
                    : base(x, y, name, Width, Height, bm)
                {
                    map.Fill(x, y, Width, Height, 0, 62, 0, 52);
                }
            }

        }

        // "wypełnia" całą zajętą przez obiekt przestrzeń MapPointami
        public void Fill(int x, int y, int w, int h, int cleft, int cright, int ctop, int cbottom)
        {
            for (int i = y; i <= y + h; i++)
                for (int j = x; j <= x + w; j++)
                    if (j != x || i != y)
                    {
                        if (mapTab[j, i] != null)
                            mapTab[j, i] = null;
                        mapTab[j, i] = new MapPoint(x, y);
                    }
            for (int i = y + ctop; i < y + cbottom; i++)
                for (int j = x + cleft; j < x + cright; j++)
                {
                    if (mapTab[j, i] != null)
                        mapTab[j, i].Collision = true;
                }
        }

        public void reFill(int x, int y, int w, int h)
        {
            for (int i = y; i <= y + h; i++)
                for (int j = x; j <= x + w; j++)
                    if (mapTab[j, i] != null)
                        mapTab[j, i] = null;
        }

        // tworzy odpowiedni obiekt w odpowiednim punkcie na mapie
        public void MakeObjectRightHere(int x, int y, string name, Map map, bool n)
        {
            int Width = 0, Height = 0;
            Bitmap bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\justgreen.png"));
            if (name[0] == 't')
            {
                if (name == "t1")
                {
                    Width = 64;
                    Height = 95;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\tree1.png"));
                }
                else if (name == "t2")
                {
                    Width = 112;
                    Height = 96;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\tree2.png"));
                }

                else if (name == "t3")
                {
                    Width = 69;
                    Height = 111;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\tree3.png"));
                }

                else if (name == "t4")
                {
                    Width = 117;
                    Height = 111;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\tree4.png"));
                }
            }

            else if (name[0] == 'g')
            {
                if (name == "g1")
                { 
                    Width = 11;
                    Height = 8;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\grass1.png"));
                }

                else if (name == "g2")
                {
                    Width = 10;
                    Height = 8;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\grass2.png"));
                }

                else if (name == "g3")
                {
                    Width = 16;
                    Height = 14;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\grass3.png"));
                }

                else if (name == "g4")
                {
                    Width = 13;
                    Height = 12;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\grass4.png"));
                }

                else if (name == "g5")
                {
                    Width = 10;
                    Height = 13;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\grass5.png"));
                }

                else if (name == "g6")
                {
                    Width = 12;
                    Height = 11;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\grass6.png"));
                }

                else if (name == "g7")
                {
                    Width = 13;
                    Height = 15;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\grass7.png"));
                }

                else if (name == "g8")
                {
                    Width = 15;
                    Height = 13;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\grass8.png"));
                }

                else if (name == "g9")
                {
                    Width = 25;
                    Height = 21;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\grass9.png"));
                }
            }

            else if (name[0] == 's')
            {
                if (name == "s1")
                {
                    Width = 28;
                    Height = 16;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\stone1.png"));
                }

                else if (name == "s2")
                {
                    Width = 24;
                    Height = 16;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\stone2.png"));
                }

                else if (name == "s3")
                {
                    Width = 46;
                    Height = 39;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\stone3.png"));
                }

            }

            else if (name[0] == 'p')
            {
                if (name == "p1")
                {
                    Width = 64;
                    Height = 43;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\stump1.png"));
                }

                else if (name == "p2")
                {
                    Width = 69;
                    Height = 43;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\stump2.png"));
                }

                else if (name == "p3")
                {
                    Width = 112;
                    Height = 44;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\stump3.png"));
                }

                else if (name == "p4")
                {
                    Width = 117;
                    Height = 43;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\stump4.png"));
                }

                else if (name == "ps1")
                {
                    Width = 54;
                    Height = 41;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\plowedSoilUnwatered.png"));
                }
            }
            else if (name[0] == 'm')
            {
                if (name == "me1")
                {
                    Width = 10;
                    Height = 8;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\Mushroom1.png"));
                }

                else if (name == "me2")
                {
                    Width = 13;
                    Height = 13;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\Mushroom2.png"));
                }

                else if (name == "me3")
                {
                    Width = 15;
                    Height = 14;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\Mushroom3.png"));
                }


                else if (name == "mn1")
                {
                    Width = 10;
                    Height = 8;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\Mushroom4.png"));
                }

                else if (name == "mn2")
                {
                    Width = 13;
                    Height = 13;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\Mushroom5.png"));
                }
            }

            else if (name[0] == 'b')
            {
                if (name == "bh1")
                {
                    Width = 94;
                    Height = 87;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\house1.png"));
                }
            }

            else if (name[0] == 'w')
            {
                if (name == "w1")
                {
                    Width = 62;
                    Height = 62;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\pond1.png"));
                }
                else if (name == "w2")
                {
                    Width = 62;
                    Height = 52;
                    bm = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\pond2.png"));
                }
            }

            bool isFree = true;
            if (n)
            {
                for (int i = y; i <= y + Height; i++)
                    for (int j = x; j <= x + Width; j++)
                        if (i != y || j != x)
                            if (map.mapTab[j, i] != null)
                            {
                                isFree = false;
                                break;
                            }
            }
            else
            {
                for (int i = y; i <= y + Height; i++)
                    for (int j = x; j <= x + Width; j++)
                        if (i != y || j != x)
                            if (map.mapTab[j, i] != null)
                            {
                                MapMainPoint tmp = (MapMainPoint)map.mapTab[map.mapTab[j, i].X, map.mapTab[j, i].Y];
                                if (tmp.Name[0] != 'g' && tmp.Name[0]!= 'm')
                                {
                                    isFree = false;
                                    break;
                                }
                            }
            }

            if (isFree)
            {
                if (name[0] == 't')
                    map.mapTab[x, y] = new MapMainPoint.Tree(x, y, name, Width, Height, bm, map);
                else if (name[0] == 'g')
                    map.mapTab[x, y] = new MapMainPoint.Grass(x, y, name, Width, Height, bm, map);
                else if (name[0] == 's')
                    map.mapTab[x, y] = new MapMainPoint.Stone(x, y, name, Width, Height, bm, map);
                else if (name[0] == 'p')
                {
                    if (name[1] == 's')
                        map.mapTab[x, y] = new MapMainPoint.PlowedSoil(x, y, name, Width, Height, bm, map);
                    else
                        map.mapTab[x, y] = new MapMainPoint.Stump(x, y, name, Width, Height, bm, map);
                }

                else if (name[0] == 'm')
                    map.mapTab[x, y] = new MapMainPoint.Mushroom(x, y, name, Width, Height, bm, map);
                else if (name[0] == 'b')
                    map.mapTab[x, y] = new MapMainPoint.Building(x, y, name, Width, Height, bm, map);
                else if (name[0] == 'w')
                    map.mapTab[x, y] = new MapMainPoint.Pond(x, y, name, Width, Height, bm, map);
            }

            else
                map.mapTab[x, y] = null;
        }

        // losowo (mniej-więcej) decyduje, gdzie i jaki przedmiot będzie stworzony
        public void MapGeneration(Map map)
        {
            string str = "";
            string id;
            Random random = new Random();
            int x = 0, y = 0;
            int xh = random.Next(400, 900);
            int yh = random.Next(300, 450);

            MakeObjectRightHere(xh, yh, "bh1", map, true);
            int xw = xh;
            while (Math.Abs(xw - xh) < 100)
                xw = random.Next(400, 900);
            int yw = yh;
            while (Math.Abs(yw - yh) < 100)
                yw = random.Next(300, 450);
            MakeObjectRightHere(xw, yw, "w2", map, true);
            int index = 1;
            bool isFree;
            while (true)
            {
                isFree = true;
                int xr = random.Next(50, 100);
                x += xr;
                int yr = random.Next(-49, 49);
                if (x > 1270)
                {
                    x = random.Next(0, 50);
                    yr = random.Next(50, 100);
                    y += yr;
                }
                if (y + yr < 0)
                    continue;

                if (y + yr > 750)
                    break;

                int caseSwitch = random.Next(1, 26);
                switch (caseSwitch)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        str = "g";
                        index = random.Next(1, 10);
                        break;
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                    case 17:
                    case 18:
                        str = "t";
                        int caseSwitch2 = random.Next(1, 13);
                        switch (caseSwitch2)
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                                index = 1;
                                break;
                            case 6:
                                index = 2;
                                break;
                            case 7:
                            case 8:
                            case 9:
                            case 10:
                            case 11:
                                index = 3;
                                break;
                            case 12:
                                index = 4;
                                break;
                        }
                        break;
                    case 19:
                    case 20:
                    case 21:
                    case 22:
                        str = "s";
                        index = random.Next(1, 4);
                        break;
                    case 23:
                    case 24:
                    case 25:
                        int caseSwitch3 = random.Next(1, 4);
                        switch (caseSwitch3)
                        {
                            case 1:
                            case 2:
                                str = "me";
                                index = random.Next(1, 4);
                                break;
                            case 3:
                                str = "mn";
                                index = random.Next(1, 3);
                                break;
                        }
                        break;


                }
                str += index;
                MakeObjectRightHere(x, y + yr, str, map, true);
            }
        }

        // "poprawia" już istniejącą mapę (na przykład generując nowe drzewa, trawę itp, co się dzieje każdego dnia)
        public void MapUpdate(Map map)
        {
            string str = "";
            string id;
            Random random = new Random();
            int x = 0, y = 0;
            int index = 1;
            bool Gen;
            while (true)
            {
                Gen = true;
                int xr = random.Next(50, 100);
                x += xr;
                int yr = random.Next(-49, 49);
                if (x > 1270)
                {
                    x = random.Next(0, 50);
                    yr = random.Next(50, 100);
                    y += yr;
                }
                if (y + yr < 0)
                    continue;

                if (y + yr > 750)
                    break;

                int caseSwitch = random.Next(1, 200);
                switch (caseSwitch)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        str = "g";
                        index = random.Next(1, 10);
                        break;
                    case 11:
                    case 12:
                    case 13:
                        str = "t";
                        int caseSwitch2 = random.Next(1, 13);
                        switch (caseSwitch2)
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                                index = 1;
                                break;
                            case 6:
                                index = 2;
                                break;
                            case 7:
                            case 8:
                            case 9:
                            case 10:
                            case 11:
                                index = 3;
                                break;
                            case 12:
                                index = 4;
                                break;
                        }
                        break;
                    case 14:
                        str = "s";
                        index = random.Next(1, 4);
                        break;
                    case 15:
                    case 16:
                    case 17:
                    case 18:
                    case 19:
                    case 20:
                    case 21:
                    case 22:
                    case 23:
                        int caseSwitch3 = random.Next(1, 4);
                        switch (caseSwitch3)
                        {
                            case 1:
                            case 2:
                                str = "me";
                                index = random.Next(1, 4);
                                break;
                            case 3:
                                str = "mn";
                                index = random.Next(1, 3);
                                break;
                        }
                        break;
                    default:
                        Gen = false;
                        break;

                }

                if (Gen)
                {
                    str += index;
                    MakeObjectRightHere(x, y + yr, str, map, true);
                }
            }
        }

        // tworzy sam obraz mapy
        public Image DrawMap()
        {
            Bitmap bm;
            Image files = new Bitmap(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "WindowsFormsApp2\\Resources\\justgreen.png"));
            Bitmap bitmap;
            Bitmap finalImage = new Bitmap(1300, 805);
            Image TreeFiles = (Image)Trees;

            foreach (MapPoint obj in this.mapTab)
            {

                if (obj != null && (obj is MapMainPoint))
                {
                    MapMainPoint tmp = (MapMainPoint)obj;
                    files = new Bitmap(tmp.Bitmap, tmp.Width, tmp.Height);
                    using (Graphics g = Graphics.FromImage(finalImage))
                    {
                        g.DrawImage(files, new Point(tmp.X, tmp.Y));
                    }
                    if (tmp is MapMainPoint.Tree)
                    {
                        using (Graphics g = Graphics.FromImage(TreeFiles))
                        {
                            g.DrawImage(files, new Point(tmp.X, tmp.Y));
                        }
                    }
                }
            }

            Trees = (Bitmap)TreeFiles;
            return finalImage;
        }

        // sprawdza, czy NPC będzie mógł przejść z odpowiedniego punktu w odpowiednim kierunku 
        // (czy nie przeszkodzi mu kolizja)
        public bool isFree(int x, int y, char dir)
        {
            int step = 5;
            if (dir == 's')
            {
                for (int j = x; j < x + 18; j++)
                    for (int i = y + 25; i <= y + step + 25; i++)
                        if (this.mapTab[j, i] != null)
                            if (this.mapTab[j, i].Collision == true)
                                return false;

                return true;
            }

            else if (dir == 'a')
            {
                for (int j = y + 15; j <= y + 25; j++)
                    for (int i = x - step; i <= x; i++)
                        if (this.mapTab[i, j] != null)
                            if (this.mapTab[i, j].Collision == true)
                                return false;
                return true;
            }

            else if (dir == 'd')
            {
                for (int j = y + 15; j <= y + 25; j++)
                    for (int i = x + 7; i < x + step + 17; i++)
                        if (i > 0)
                            if (this.mapTab[i, j] != null)
                                if (this.mapTab[i, j].Collision == true)
                                    return false;
                return true;
            }

            else if (dir == 'w')
            {
                for (int j = x; j < x + 18; j++)
                    for (int i = y; i < y + step; i++)
                        if (this.mapTab[j, i] != null)
                            if (this.mapTab[j, i].Collision == true)
                                return false;
                return true;
            }

            return true;
        }
    }
}