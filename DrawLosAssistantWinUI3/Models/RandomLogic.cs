using System;
using System.Collections.ObjectModel;
using static DrawLosAssistantWinUI3.Models.NameList;

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
            int MaxNum = NameList.nameList[(int)GachaType.SuperRare].Count - 1;
            int resultNum;
            resultNum = 0;

            for (bool ResultGet = false; ResultGet == false;)  // 防止重复
            {
                resultNum = random.Next(0, MaxNum);
                if (SRGachaGetIds.Count == MaxNum)
                {
                    SRGachaGetIds.Clear();
                    return NameList.nameList[(int)GachaType.SuperRare][MaxNum];
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
            LogRecord.Add("本次抽中Super Rare名单中的" + NameList.nameList[(int)GachaType.SuperRare][resultNum]);
            return NameList.nameList[(int)GachaType.SuperRare][resultNum];
        }

        public static string RareRandom()
        {
            NameList.Load();

            Random random = new Random();
            int MaxNum = NameList.nameList[(int)GachaType.Rare].Count - 1;
            int resultNum;
            resultNum = 0;

            for (bool ResultGet = false; ResultGet == false;)  // 防止重复
            {
                resultNum = random.Next(0, MaxNum);
                if (RGachaGetIds.Count == MaxNum)
                {
                    RGachaGetIds.Clear();
                    return NameList.nameList[(int)GachaType.Rare][MaxNum];
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
            LogRecord.Add("本次抽中Rare名单中的" + NameList.nameList[(int)GachaType.Rare][resultNum]);
            return NameList.nameList[(int)GachaType.Rare][resultNum];
        }

        public static string CommonRandom()
        {
            NameList.Load();

            Random random = new Random();
            int MaxNum = NameList.nameList[(int)GachaType.Normal].Count - 1;
            int resultNum;
            resultNum = 0;

            for (bool ResultGet = false; ResultGet == false;)  // 防止重复
            {
                resultNum = random.Next(0, MaxNum);
                if (CommonGachaGetIds.Count == MaxNum)
                {
                    CommonGachaGetIds.Clear();
                    return NameList.nameList[(int)GachaType.Normal][MaxNum];
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
            LogRecord.Add("本次抽中普通名单中的" + nameList[(int)GachaType.Normal][resultNum]);
            return NameList.nameList[(int)GachaType.Normal][resultNum];
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