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
                    return "C:\\Users\\User\\Source\\Repos\\byret\\Farmio\\images\\1.png";
                    break;
                case 2:
                    return "C:\\Users\\User\\Source\\Repos\\byret\\Farmio\\images\\2.png";
                    break;
                case3:
                    return "C:\\Users\\User\\Source\\Repos\\byret\\Farmio\\images\\3.png";
                    break;
                case4:
                    return "C:\\Users\\User\\Source\\Repos\\byret\\Farmio\\images\\4.png";
                    break;
                case5:
                    return "C:\\Users\\User\\Source\\Repos\\byret\\Farmio\\images\\5.png";
                    break;
                case6:
                    return "C:\\Users\\User\\Source\\Repos\\byret\\Farmio\\images\\6.png";
                    break;
                case7:
                    return "C:\\Users\\User\\Source\\Repos\\byret\\Farmio\\images\\7.png";
                    break;
                default:
                    return "C:\\Users\\User\\Source\\Repos\\byret\\Farmio\\images\\8.png";
                    break;
            }
        }

        public static string MusicRandomize()
        {
            int caseSwitch = RandomNumber(1, 8);

            switch (caseSwitch)
            {
                case 1:
                    return "C:\\Users\\User\\Source\\Repos\\byret\\Farmio\\music\\purrple-cat-green-tea.wav";
                    break;
                case 2:
                    return "C:\\Users\\User\\Source\\Repos\\byret\\Farmio\\music\\soimanislander-behind-the-barn.wav";
                    break;
                case 3:
                    return "C:\\Users\\User\\Source\\Repos\\byret\\Farmio\\music\\soimanislander-along-the-fjord.wav";
                    break;
                case 4:
                    return "C:\\Users\\User\\Source\\Repos\\byret\\Farmio\\music\\purrple-cat-bloom.wav";
                    break;
                case 5:
                    return "C:\\Users\\User\\Source\\Repos\\byret\\Farmio\\music\\purrple-cat-spring-showers.wav"; 
                    break;
                case 6:
                    return "C:\\Users\\User\\Source\\Repos\\byret\\Farmio\\music\\vlad-gluschenko-springtime.wav";  // хз, наверное слишком резкий
                    break;
                default:
                    return "C:\\Users\\User\\Source\\Repos\\byret\\Farmio\\music\\vlad-gluschenko-its-been-a-while.wav";   // тоже резковат, но для стартового окна
                                                                                                                          // вроде норм хд
                    break;
                    //case5:
                    //    return "C:\\Users\\User\\Source\\Repos\\byret\\Farmio\\images\\5.png";
                    //    break;
                    //case6:
                    //    return "C:\\Users\\User\\Source\\Repos\\byret\\Farmio\\images\\6.png";
                    //    break;
                    //case7:
                    //    return "C:\\Users\\User\\Source\\Repos\\byret\\Farmio\\images\\7.png";
                    //    break;
                    //default:
                    //return "C:\\Users\\User\\Source\\Repos\\byret\\Farmio\\images\\8.png";
                    //break;
            }
        }
    }
}
