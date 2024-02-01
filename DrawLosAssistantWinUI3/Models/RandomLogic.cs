using System;
using System.Collections.ObjectModel;

namespace DrawLosAssistantWinUI3.Models
{
    public class RandomLogic
    {
        private static ObservableCollection<int> CommonGachaGetIds = new ObservableCollection<int>();
        private static ObservableCollection<int> RGachaGetIds = new ObservableCollection<int>();
        private static ObservableCollection<int> SRGachaGetIds = new ObservableCollection<int>();

        public static string SuperRareRandom()
        {
            NameList.Load();

            Random random = new Random();
            int MaxNum = NameList.SuperRareList.Count - 1;
            int resultNum;
            resultNum = 0;

            for (bool ResultGet = false; ResultGet == false;)  // 防止重复
            {
                resultNum = random.Next(0, MaxNum);
                if (SRGachaGetIds.Count == MaxNum)
                {
                    SRGachaGetIds.Clear();
                    return NameList.SuperRareList[MaxNum];
                }
                if (SRGachaGetIds.Count > MaxNum)
                {
                    SRGachaGetIds.Clear();
                }
                if (!SRGachaGetIds.Contains(resultNum))
                {
                    SRGachaGetIds.Add(resultNum);
                    ResultGet = true;
                }
            }

            return NameList.SuperRareList[resultNum];
        }

        public static string RareRandom()
        {
            NameList.Load();

            Random random = new Random();
            int MaxNum = NameList.RareList.Count - 1;
            int resultNum;
            resultNum = 0;

            for (bool ResultGet = false; ResultGet == false;)  // 防止重复
            {
                resultNum = random.Next(0, MaxNum);
                if (RGachaGetIds.Count == MaxNum)
                {
                    RGachaGetIds.Clear();
                    return NameList.RareList[MaxNum];
                }
                if (RGachaGetIds.Count > MaxNum)
                {
                    RGachaGetIds.Clear();
                }
                if (!RGachaGetIds.Contains(resultNum))
                {
                    RGachaGetIds.Add(resultNum);
                    ResultGet = true;
                }
            }

            return NameList.RareList[resultNum];
        }

        public static string CommonRandom()
        {
            NameList.Load();

            Random random = new Random();
            int MaxNum = NameList.Name.Count - 1;
            int resultNum;
            resultNum = 0;

            for (bool ResultGet = false; ResultGet == false;)  // 防止重复
            {
                resultNum = random.Next(0, MaxNum);
                if (CommonGachaGetIds.Count == MaxNum)
                {
                    CommonGachaGetIds.Clear();
                    return NameList.Name[MaxNum];
                }
                if (CommonGachaGetIds.Count > MaxNum)
                {
                    CommonGachaGetIds.Clear();
                }
                if (!CommonGachaGetIds.Contains(resultNum))
                {
                    CommonGachaGetIds.Add(resultNum);
                    ResultGet = true;
                }
            }

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
            else if (levelNum > 750)
            {
                return "Rare";
            }
            else
            {
                return "Common";
            }
        }
    }
}