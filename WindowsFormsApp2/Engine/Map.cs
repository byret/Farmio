using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Engine
{
    
    public class Map
    {

        public Bitmap Trees = new Bitmap(1300, 805);
        public MapPoint[,] mapTab = new MapPoint [1700,900];

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
        //przechowuje całą informację o obiekcie
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
                    int numOfSeeds = Weight*2;
                    Item.Seed seed = new Item.Seed(numOfSeeds);
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
                        Weight = random.Next(25,30);
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
                        map.MakeObjectRightHere(X, Y + 52, "p1", map);
                    else if (Name[1] == '3')
                        map.MakeObjectRightHere(X, Y + 68, "p2", map);
                    else if (Name[1] == '2')
                        map.MakeObjectRightHere(X, Y + 52, "p3", map);
                    else if (Name[1] == '4')
                        map.MakeObjectRightHere(X, Y + 68, "p4", map);
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
                public Building(int x, int y, string name, int Width, int Height, Bitmap bm, Map map)
                    : base(x, y, name, Width, Height, bm)
                {
                    map.Fill(x, y, Width, Height, 0, 94, 0, 84);                   
                }

                public void DestroyBuilding(Map map)
                {
                    Bitmap = null;
                    map.reFill(X, Y, Width, Height);                   
                }
            }



        }

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

        public void MakeObjectRightHere(int x, int y, string name, Map map)
        {
            int Width = 0, Height = 0;
            Bitmap bm = new Bitmap("justgreen.png");
            if (name[0] == 't')
            {
                if (name == "t1")
                {
                    Width = 64;
                    Height = 95;
                    bm = new Bitmap("tree1.png");
                }
                else if (name == "t2")
                {
                    Width = 112;
                    Height = 96;
                    bm = new Bitmap("tree2.png");
                }

                else if (name == "t3")
                {
                    Width = 69;
                    Height = 111;
                    bm = new Bitmap("tree3.png");
                }

                else if (name == "t4")
                {
                    Width = 117;
                    Height = 111;
                    bm = new Bitmap("tree4.png");
                }
            }

            else if (name[0] == 'g')
            {
                if (name == "g1")
                {
                    Width = 11;
                    Height = 8;
                    bm = new Bitmap("grass1.png");
                }

                else if (name == "g2")
                {
                    Width = 10;
                    Height = 8;
                    bm = new Bitmap("grass2.png");
                }

                else if (name == "g3")
                {
                    Width = 16;
                    Height = 14;
                    bm = new Bitmap("grass3.png");
                }

                else if (name == "g4")
                {
                    Width = 13;
                    Height = 12;
                    bm = new Bitmap("grass4.png");
                }

                else if (name == "g5")
                {
                    Width = 10;
                    Height = 13;
                    bm = new Bitmap("grass5.png");
                }

                else if (name == "g6")
                {
                    Width = 12;
                    Height = 11;
                    bm = new Bitmap("grass6.png");
                }

                else if (name == "g7")
                {
                    Width = 13;
                    Height = 15;
                    bm = new Bitmap("grass7.png");
                }

                else if (name == "g8")
                {
                    Width = 15;
                    Height = 13;
                    bm = new Bitmap("grass8.png");
                }

                else if (name == "g9")
                {
                    Width = 25;
                    Height = 21;
                    bm = new Bitmap("grass9.png");
                }
            }

            else if (name[0] == 's')
            {
                if (name == "s1")
                {
                    Width = 28;
                    Height = 16;
                    bm = new Bitmap("stone1.png");
                }

                else if (name == "s2")
                {
                    Width = 24;
                    Height = 16;
                    bm = new Bitmap("stone2.png");
                }

                else if (name == "s3")
                {
                    Width = 46;
                    Height = 39;
                    bm = new Bitmap("stone3.png");
                }

            }

            else if (name[0] == 'p')
            {
                if (name == "p1")
                {
                    Width = 64;
                    Height = 43;
                    bm = new Bitmap("stump1.png");
                }

                else if (name == "p2")
                {
                    Width = 69;
                    Height = 43;
                    bm = new Bitmap("stump2.png");
                }

                else if (name == "p3")
                {
                    Width = 112;
                    Height = 44;
                    bm = new Bitmap("stump3.png");
                }

                else if (name == "p4")
                {
                    Width = 117;
                    Height = 43;
                    bm = new Bitmap("stump4.png");
                }

            }
            else if (name[0] == 'm')
            {
                if (name == "me1")
                {
                    Width = 10;
                    Height = 8;
                    bm = new Bitmap("Mushroom1.png");
                }

                else if (name == "me2")
                {
                    Width = 13;
                    Height = 13;
                    bm = new Bitmap("Mushroom2.png");
                }

                else if (name == "me3")
                {
                    Width = 15;
                    Height = 14;
                    bm = new Bitmap("Mushroom3.png");
                }


                else if (name == "mn1")
                {
                    Width = 10;
                    Height = 8;
                    bm = new Bitmap("Mushroom4.png");
                }

                else if (name == "mn2")
                {
                    Width = 13;
                    Height = 13;
                    bm = new Bitmap("Mushroom5.png");
                }
            }

            else if (name[0] == 'b')
            {
                if (name == "bh1")
                {
                    Width = 94;
                    Height = 87;
                    bm = new Bitmap("house1.png");
                }
            }
 

            bool isFree = true;
            for (int i = y; i <= y + Height; i++)
                for (int j = x; j <= x + Width; j++)
                    if (i != y || j != x)
                        if (map.mapTab[j, i] != null)
                        {
                            isFree = false;
                            break;
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
                        map.mapTab[x, y] = new MapMainPoint.Stump(x, y, name, Width, Height, bm, map);
                    else if (name[0] == 'm')
                        map.mapTab[x, y] = new MapMainPoint.Mushroom(x, y, name, Width, Height, bm, map);
                    else if (name[0] == 'b')
                        map.mapTab[x, y] = new MapMainPoint.Building(x, y, name, Width, Height, bm, map);
            }
                
            else
                map.mapTab[x, y] = null;

        }

        public void MapGeneration(Map map)
        {
            string str = "";
            string id;
            Random random = new Random();
            int x = 0, y = 0;
            int xh = random.Next(400, 900);
            int yh = random.Next(300, 450);

            MakeObjectRightHere(xh, yh, "bh1", map);
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

                int caseSwitch = random.Next(1, 10);
                switch (caseSwitch)
                {
                    case 1:
                    case 2:
                    case 3:
                        str = "g";
                        index = random.Next(1, 10);
                        break;
                    case 4:
                    case 5:
                    case 6:
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
                    case 7:
                    case 8:
                        str = "s";
                        index = random.Next(1, 4);
                        break;
                    case 9:
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
                MakeObjectRightHere(x, y + yr, str, map);
            }

        }

        public Image DrawMap()
        {
  
            Bitmap bm;
            Image files = new Bitmap("justgreen.png");
            Bitmap bitmap;
            Bitmap finalImage = new Bitmap(1300, 805);
            Image TreeFiles = (Image)Trees;

            foreach(MapPoint obj in this.mapTab)
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

        public bool isFree (int x, int y, char dir)
        {
            int step = 5;
            if (dir == 's')
            {
                for (int j = x; j < x + 18; j++)
                    for (int i = y + 25; i <= y + step+ 25; i++)
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