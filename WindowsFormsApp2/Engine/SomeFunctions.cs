using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class SomeFunctions
    {
        static public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public static string MainPictureRandomize()
        {
            int caseSwitch = RandomNumber(1, 8);

            switch (caseSwitch)
            {
                case 1:
                    return "_1";
                    break;
                case 2:
                    return "_2";
                    break;
                case3:
                    return "_3";
                    break;
                case4:
                    return "_4";
                    break;
                case5:
                    return "_5";
                    break;
                case6:
                    return "_6";
                    break;
                case7:
                    return "_7";
                    break;
                default:
                    return "_8";
                    break;
            }
        }

        public static string MusicRandomize()
        {
            int caseSwitch = RandomNumber(1, 8);

            switch (caseSwitch)
            {
                case 1:
                    return "purrple_cat_green_tea";
                    break;
                case 2:
                    return "purrple_cat_spring_showers";
                    break;
                case 3:
                    return "purrple_cat_bloom";
                    break;
                case 4:
                    return "soimanislander_along_the_fjord";
                    break;
                case 5:
                    return "soimanislander_behind_the_barn"; 
                    break;
                case 6:
                    return "vlad_gluschenko_springtime";
                    break;
                default:
                    return "vlad_gluschenko_its_been_a_while";
                    break;

            }
        }
    }
}
