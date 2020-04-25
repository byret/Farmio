using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Engine;
namespace Farmio
{
    public class GameMap
    {

        public string[] array = new string[500];
        public string[,] formArray = new string[1499,999];
        string str;
        Random random = new Random();
        public void ArrayGen()
        { for (int i = 0; i < 500; i++)
            {
                
                //random.Next(1, max);
                int caseSwitch = random.Next(1, 32);
                //Console.WriteLine(caseSwitch);
                switch (caseSwitch)
                {
                    case 1:
                    case 2:
                    case 3:
                        str = "g1";
                        break;
                    case 4:
                    case 5:
                    case 6:
                        str = "g2";
                        break;
                    case 7:
                    case 8:
                    case 9:
                        str = "g3";
                        break;
                    case 10:
                    case 11:
                    case 12:
                        str = "g4";
                        break;
                    case 13:
                    case 14:
                    case 15:
                        str = "g5";
                        break;
                    case 16:
                    case 17:
                    case 18:
                        str = "g6";
                        break;
                    case 19:
                    case 20:
                    case 21:
                        str = "g7";
                        break;
                    case 22:
                    case 23:
                    case 24:
                        str = "g8";
                        break;
                    case 25:
                    case 26:
                        str = "t1";
                        break;
                    case 27:
                    case 28:
                        str = "t3";
                        break;
                    case 29:
                        str = "s1";
                        break;
                    case 30:
                        str = "s2";
                        break;
                    case 31:
                        str = "s3";
                        break;
                    //case 15:
                    //    str = "g15";
                    //    break;
                    default:
                        str = "g";
                        break;
                }
                     array[i] = str;
                }
             }
           
    




        public void MapGen()
        {
            int x = 0, y = 0;

            // grass
            foreach (string itm in array)
            {
                if (itm == null)
                    continue;

                int xr = random.Next(50, 100);

                x += xr;

                if (x > 1270)
                {
                    x = 0;
                    int yr = random.Next(50, 100);
                    y += yr;
                }
                int yr1 = random.Next(-49, 49);
                if (y + yr1 > 680)
                    break;

                if (y + yr1 < 0)
                    continue;

                str = itm + (x+xr) + "-" + (y + yr1);
                string str1 = "." + str;
                //Console.WriteLine(str);
                // trawa
                if (itm == "g1")
                {
                    for (int i = y + yr1; i <= y + yr1 + 8; i++)
                        for (int j = x + xr; j <= x + xr + 11; j++)
                            formArray[j, i] = str;
                    formArray[x + xr, y + yr1] = str1;
                }

                else if (itm == "g2")
                {
                    for (int i = y + yr1; i < y + yr1 + 8; i++)
                        for (int j = x + xr; j < x + xr + 10; j++)
                            formArray[j, i] = str;
                    formArray[x + xr, y + yr1] = str1;
                }

                else if (itm == "g3")
                {
                    for (int i = y + yr1; i < y + yr1 + 14; i++)
                        for (int j = x + xr; j < x + xr + 16; j++)
                            formArray[j, i] = str;
                    formArray[x + xr, y + yr1] = str1;
                }

                else if (itm == "g4")
                {
                    for (int i = y + yr1; i < y + yr1 + 12; i++)
                        for (int j = x + xr; j < x + xr + 13; j++)
                            formArray[j, i] = str;
                    formArray[x + xr, y + yr1] = str1;
                }

                else if (itm == "g5")
                {
                    for (int i = y + yr1; i < y + yr1 + 13; i++)
                        for (int j = x + xr; j < x + xr + 10; j++)
                            formArray[j, i] = str;
                    formArray[x + xr, y + yr1] = str1;
                }

                else if (itm == "g6")
                {
                    for (int i = y + yr1; i < y + yr1 + 11; i++)
                        for (int j = x + xr; j < x + xr + 12; j++)
                            formArray[j, i] = str;
                    formArray[x + xr, y + yr1] = str1;
                }

                else if (itm == "g7")
                {
                    for (int i = y + yr1; i < y + yr1 + 15; i++)
                        for (int j = x + xr; j < x + xr + 13; j++)
                            formArray[j, i] = str;
                    formArray[x + xr, y + yr1] = str1;
                }

                else if (itm == "g8")
                {
                 for (int i = y + yr1; i < y + yr1 + 13; i++)
                        for (int j = x + xr; j < x + xr + 15; j++)
                            formArray[j, i] = str;
                    formArray[x + xr, y + yr1] = str1;
                }

                else if (itm == "g9")
                {
                for (int i = y + yr1; i < y + yr1 + 21; i++)
                        for (int j = x + xr; j < x + xr + 25; j++)
                            formArray[j, i] = str;
                    formArray[x + xr, y + yr1] = str1;
                }


                else if (itm == "s1")
                {
                    for (int i = y + yr1; i < y + yr1 + 16; i++)
                        for (int j = x + xr; j < x + xr + 28; j++)
                            formArray[j, i] = str;
                    formArray[x + xr, y + yr1] = str1;
                }

                else if (itm == "s2")
                {
                    for (int i = y + yr1; i < y + yr1 + 16; i++)
                        for (int j = x + xr; j < x + xr + 24; j++)
                            formArray[j, i] = str;
                    formArray[x + xr, y + yr1] = str1;
                }

                else if (itm == "s3")
                {
                    for (int i = y + yr1; i < y + yr1 + 39; i++)
                        for (int j = x + xr; j < x + xr + 46; j++)
                            formArray[j, i] = str;
                    formArray[x + xr, y + yr1] = str1;
                }


                else if (itm == "t1")
                {
                   for (int i = y + yr1; i < y + yr1 + 95; i++)
                            for (int j = x + xr; j < x + xr + 64; j++)
                                formArray[j, i] = str;
                        formArray[x + xr, y + yr1] = str1;
                   
                }

                else if (itm == "t2")
                {

                     for (int i = y + yr1; i < y + yr1 + 96; i++)
                            for (int j = x + xr; j < x + xr + 112; j++)
                                formArray[j, i] = str;
                        formArray[x + xr, y + yr1] = str1;
                }

                else if (itm == "t3")
                {
                        for (int i = y + yr1; i < y + yr1 + 111; i++)
                            for (int j = x + xr; j < x + xr + 69; j++)
                                formArray[j, i] = str;
                        formArray[x + xr, y + yr1] = str1;
                }


            }


            // buildings
            //TODO


        }

        public Image MapCutGrass(int x, int y, int h, int w, GameMap map)
        {
            if (map.formArray[x, y] == null)
                return map.DrawMap();
            for (int i = y; i < y + h; i++)
                for (int j = x; j <= x + w; j++)
                    map.formArray[j, i] = null;
            return map.DrawMap();
        }

        public Image MapGetStone(int x, int y, int h, int w, GameMap map)
        {
            if (map.formArray[x, y] == null)
                return map.DrawMap();
            for (int i = y; i < y + h; i++)
                for (int j = x; j < x + w; j++)
                    if (map.formArray[j, i].Substring(0,1) == "s" || map.formArray[j, i].Substring(0, 2) == ".s")
                    map.formArray[j, i] = null;
            return map.DrawMap();
        }

        public Image MapCutDTree(int x, int y, int h, int w, GameMap map)
        {
            if (map.formArray[x, y] == null)
                return map.DrawMap();
            string str = "";
            if (h == 95)
                str = "r1" + x + "-" + y;
            else if (h == 111)
                str = "r2" + x + "-" + y;
            string str1 = "." + str;
            string tmp;
            Console.WriteLine(x + " " + w + ", " + y + " " + h);
            map.formArray[x, y] = null;
            for (int i = y; i < y + h; i++)
                for (int j = x + 1; j < x + w; j++)
                {
                    if (map.formArray[j, i] != null)
                    {

                        tmp = map.formArray[j, i];
                        if (tmp.Substring(0, 1) != "t")
                            continue;
                        map.formArray[j, i] = null ;
                    }
                }
            return map.DrawMap();
        }

        public Image DrawMap()
        {
            int x = 0, y = 0, img = 0;
           // Console.WriteLine("!!!");
            Bitmap bm;

            Image files = new Bitmap("justgreen.png");

            Bitmap bitmap;

            Bitmap finalImage = new Bitmap(1300, 805);

            //// pni
            //for (int i = 0; i < 805; i++)
            //    for (int j = 0; j < 1300; j++)
            //    {
            //        if (formArray[j, i] == null)
            //            continue;
            //        int tmpj = j;
            //        string str = formArray[j, i];
            //        if (str.Substring(0, 2) == ".r" )
            //        {
            //            if (str.Substring(0, 3) == ".r1")
            //            {
            //                bm = new Bitmap("t6495.png");
            //                files = new Bitmap(bm, 64, 95);
            //                j += 63;
            //            }
            //            else if (str.Substring(0, 3) == ".r2")
            //            {
            //                bm = new Bitmap("t69111.png");
            //                files = new Bitmap(bm, 69, 111);
            //                j += 68;
            //            }
            //            using (Graphics g = Graphics.FromImage(finalImage))
            //            {
            //                g.DrawImage(files, new Point(tmpj, i));
            //            }
            //     }
            //    }

            // trawa
            for (int i = 0; i < 805; i++)
                for (int j = 0; j < 1300; j++)
                {
                    if (formArray[j, i] == null)
                        continue;
                    int tmpj = j;
                    string str = formArray[j, i];
                    //Console.WriteLine(str);
                    if (str.Substring(0, 2) == ".g")
                    {
                        if (str.Substring(0, 3) == ".g1")
                        {
                            bm = new Bitmap("grass1.png");
                            files = new Bitmap(bm, 11, 8);
                            j += 10;
                        }
                    else if (str.Substring(0, 3) == ".g2")
                        {
                            bm = new Bitmap("grass2.png");
                            files = new Bitmap(bm, 10, 8);
                            j += 9;
                        }
                    else if (str.Substring(0, 3) == ".g3")
                        {
                            bm = new Bitmap("grass3.png");
                            files = new Bitmap(bm, 16, 14);
                            j += 15;
                        }
                    else if (str.Substring(0, 3) == ".g4")
                        {
                            bm = new Bitmap("grass4.png");
                            files = new Bitmap(bm, 13, 12);
                            j += 12;
                        }
                    else if (str.Substring(0, 3) == ".g5")
                        {
                            bm = new Bitmap("grass5.png");
                            files = new Bitmap(bm, 10, 13);
                            j += 9;
                        }
                    else if (str.Substring(0, 3) == ".g6")
                        {
                            bm = new Bitmap("grass6.png");
                            files = new Bitmap(bm, 12, 11);
                            j += 11;
                        }
                    else if (str.Substring(0, 3) == ".g7")
                        {
                            bm = new Bitmap("grass7.png");
                            files = new Bitmap(bm, 13, 15);
                            j += 12;
                        }
                    else if (str.Substring(0, 3) == ".g8")
                        {
                            bm = new Bitmap("grass8.png");
                            files = new Bitmap(bm, 15, 13);
                            j += 14;

                        }
                    else if (str.Substring(0, 3) == ".g9")
                        {
                            bm = new Bitmap("grass9.png");
                            files = new Bitmap(bm, 25, 21);
                            j += 24;
                        }

                        using (Graphics g = Graphics.FromImage(finalImage))
                        {
                            //Console.WriteLine(str);
                            g.DrawImage(files, new Point(tmpj, i));
                            //Console.WriteLine(str);
                        }

                        img++;
                    }
                }

            // kamienie
            for (int i = 0; i < 805; i++)
                for (int j = 0; j < 1300; j++)
                {
                    if (formArray[j, i] == null)
                        continue;
                    int tmpj = j;
                    string str = formArray[j, i];
                    if (str.Substring(0, 2) == ".s")
                    {
                        if (str.Substring(0, 3) == ".s1")
                        {
                            bm = new Bitmap("stone1.png");
                            files = new Bitmap(bm, 28, 16);
                            j += 27;
                        }
                        else if (str.Substring(0, 3) == ".s2")
                        {
                            bm = new Bitmap("stone2.png");
                            files = new Bitmap(bm, 24, 16);
                            j += 23;
                        }
                        else if (str.Substring(0, 3) == ".s3")
                        {
                            bm = new Bitmap("stone3.png");
                            files = new Bitmap(bm, 46, 39);
                            j += 45;
                        }

                        using (Graphics g = Graphics.FromImage(finalImage))
                        {
                            //Console.WriteLine(str);
                            g.DrawImage(files, new Point(tmpj, i));
                            //Console.WriteLine(str);
                        }

                        img++;
                    }
                }

            // drzewa
            for (int i = 0; i < 805; i++)
                for (int j = 0; j < 1300; j++)
                {
                    if (formArray[j, i] == null)
                        continue;
                    int tmpj = j;
                    string str = formArray[j, i];
                    if ((str.Substring(0, 2) == ".t"))
                    {
                        if (str.Substring(0, 3) == ".t1")
                        {
                            bm = new Bitmap("tree1.png");
                            files = new Bitmap(bm, 64, 95);
                            j += 63;
                        }
                        else if (str.Substring(0, 3) == ".t2")
                        {
                            bm = new Bitmap("tree2.png");
                            files = new Bitmap(bm, 112, 96);
                            j += 111;
                        }
                        else if (str.Substring(0, 3) == ".t3")
                        {
                            bm = new Bitmap("tree3.png");
                            files = new Bitmap(bm, 69, 111);
                            j += 68;
                        }
                        using (Graphics g = Graphics.FromImage(finalImage))
                        {
                            //Console.WriteLine(str);
                            g.DrawImage(files, new Point(tmpj, i));
                            //.WriteLine(str);
                        }
                        img++;
                    }
                }
            //Console.WriteLine("...");
            return finalImage;
        }



    }
}
