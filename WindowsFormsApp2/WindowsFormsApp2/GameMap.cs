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
        string str;
        Random random = new Random();
        public void ArrayGen()
        { for (int i = 0; i < 500; i++)
            {
                
                //random.Next(1, max);
                int caseSwitch = random.Next(1, 5);
                //Console.WriteLine(caseSwitch);

                switch (caseSwitch)
                {
                    case 1:
                        str = "g1";
                        break;
                    case 2:
                        str = "g2";
                        break;
                    case 3:
                        str = "g3";
                        break;
                    case 4:
                        str = "g4";
                        break;
                    case 5:
                        str = "g5";
                        break;
                    //case 6:
                    //    str = "g6";
                    //    break;
                    //case 7:
                    //    str = "g7";
                    //    break;
                    //case 8:
                    //    str = "g8";
                    //    break;
                    //case 9:
                    //    str = "g9";
                    //    break;
                    //case 10:
                    //    str = "g10";
                    //    break;
                    //case 11:
                    //    str = "g11";
                    //    break;
                    //case 12:
                    //    str = "g12";
                    //    break;
                    //case 13:
                    //    str = "g13";
                    //    break;
                    //case 14:
                    //    str = "g14";
                    //    break;
                    //case 15:
                    //    str = "g15";
                    //    break;
                    default:
                        str = "g";
                        break;
                }
                Console.WriteLine(str);
                     array[i] = str;
                }
             }
           
    




        public Image MapGen()
        {
            int x = 0, y = 0, img = 0;

            Bitmap[] bm;
            bm = new Bitmap[501];

            Image[] files;
            files = new Image[501];

            List<Bitmap> images = new List<Bitmap>();
            Bitmap bitmap;

            Bitmap finalImage = new Bitmap(1500, 1000);

            foreach (string itm in array)
            {

                // create the images!
                if (itm == "g")
                {
                    bm[img] = new Bitmap("grass1.png");
                    files[img] = new Bitmap(bm[img], 11, 8);
                }

                if (itm == "g1")
                {
                    bm[img] = new Bitmap("grass2.png");
                    files[img] = new Bitmap(bm[img], 10, 8);
                }

                if (itm == "g2")
                {
                    bm[img] = new Bitmap("grass3.png");
                    files[img] = new Bitmap(bm[img], 16, 14);
                }

                if (itm == "g3")
                {
                    bm[img] = new Bitmap("grass4.png");
                    files[img] = new Bitmap(bm[img], 13, 12);
                }

                if (itm == "g4")
                {
                    bm[img] = new Bitmap("grass5.png");
                    files[img] = new Bitmap(bm[img], 10, 13);
                }

                if (itm == "g5")
                {
                    bm[img] = new Bitmap("grass6.png");
                    files[img] = new Bitmap(bm[img], 12, 11);
                }

                //if (itm == "g6")
                //{
                //    bm[img] = new Bitmap("grass6.png");
                //    files[img] = new Bitmap(bm[img], 50, 50);
                //}

                //if (itm == "g7")
                //{
                //    bm[img] = new Bitmap("grass7.png");
                //    files[img] = new Bitmap(bm[img], 50, 50);
                //}

                //if (itm == "g8")
                //{
                //    bm[img] = new Bitmap("grass8.png");
                //    files[img] = new Bitmap(bm[img], 50, 50);
                //}

                //if (itm == "g9")
                //{
                //    bm[img] = new Bitmap("grass9.png");
                //    files[img] = new Bitmap(bm[img], 50, 50);
                //}

                //if (itm == "g10")
                //{
                //    bm[img] = new Bitmap("grass10.png");
                //    files[img] = new Bitmap(bm[img], 50, 50);
                //}

                //if (itm == "g11")
                //{
                //    bm[img] = new Bitmap("grass11.png");
                //    files[img] = new Bitmap(bm[img], 50, 50);
                //}

                //if (itm == "g12")
                //{
                //    bm[img] = new Bitmap("grass12.png");
                //    files[img] = new Bitmap(bm[img], 50, 50);
                //}

                //if (itm == "g13")
                //{
                //    bm[img] = new Bitmap("grass13.png");
                //    files[img] = new Bitmap(bm[img], 50, 50);
                //}

                //if (itm == "g14")
                //{
                //    bm[img] = new Bitmap("grass14.png");
                //    files[img] = new Bitmap(bm[img], 50, 50);
                //}

                //if (itm == "g15")
                //{
                //    bm[img] = new Bitmap("grass15.png");
                //    files[img] = new Bitmap(bm[img], 50, 50);
                //}

                // now we add the image to the graphics engine
                using (Graphics g = Graphics.FromImage(finalImage))
                {
                    g.DrawImage(files[img], new Point(x, y));
                }
               // int caseSwitch = random.Next(0, 1500);
                img++;


                int xr = random.Next(50, 150);
                

                x += xr;

                if (img % 50 == 0)
                {
                    x = 0;
                    int yr = random.Next(50, 150);
                    y += yr; // 12 because that's how big the image is
                }
            }

            return finalImage;


        }
    }
}
