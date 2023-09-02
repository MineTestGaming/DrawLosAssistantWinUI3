using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Printing;

namespace DrawLosAssistantWinUI3.Models
{
    public class RandomLogic
    {
        public static string SuperRareRandom()
        {
            NameList.Load();

            Random random = new Random();
            int MaxNum = NameList.SuperRareList.Count - 1;
            int resultNum = random.Next(0, MaxNum);


            return NameList.SuperRareList[resultNum];
        }

        public static string CommonRandom()
        {
            NameList.Load();

            Random random = new Random();
            int MaxNum = NameList.Name.Count - 1;
            int resultNum = random.Next(0, MaxNum);


            return NameList.Name[resultNum];
        }

        public static string RandomLevel()
        {
            Random random = new Random();

            int levelNum = random.Next(0, 1000);

            if (levelNum > 900)
            {
                return "Super Rare";
            }
           /* else if (levelNum > 850)
            {
                return "Rare";
            } */
            else
            {
                return "Common";
            }

        }
    }
}
