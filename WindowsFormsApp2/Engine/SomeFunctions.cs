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
            int caseSwitch = RandomNumber(7, 8);

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

       /* public static string MusicRandomize()
        {
            int caseSwitch = RandomNumber(1, 8);

            switch (caseSwitch)
            {
                case 1:
                    return "purrple-cat-green-tea.wav";
                    break;
                case 2:
                    return "purrple-cat-spring-showers.wav";
                    break;
                case 3:
                    return "purrple-cat-bloom.wav";
                    break;
                case 4:
                    return "soimanislander-along-the-fjord.wav";
                    break;
                case 5:
                    return "soimanislander-behind-the-barn.wav"; 
                    break;
                case 6:
                    return "vlad-gluschenko-springtime.wav";
                    break;
                default:
                    return "vlad-gluschenko-its-been-a-while.wav";
                    break;

            }
        }
       */



    }
}
