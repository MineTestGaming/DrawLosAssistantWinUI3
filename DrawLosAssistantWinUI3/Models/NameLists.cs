using Newtonsoft.Json;
using System.Collections.Generic;

namespace DrawLosAssistantWinUI3.Models
{
    public class NameList
    {
        public static List<string> Name = new List<string>();
        public static List<string> RareList = new List<string>();
        public static List<string> SuperRareList = new List<string>();

        public static void InitializeList()
        {
            InitializeNormalList();
            InitializeRareList();
            InitializeSuperRareList();
        }

        public static void InitializeNormalList()
        {
            // Normal List
            Name.Clear();
            Name.Add("茅森月歌");
            Name.Add("和泉由希");
            Name.Add("逢川惠");
            Name.Add("東城司");
            Name.Add("朝倉可憐");
            Name.Add("國見玉");
            Save("Normal");
        }

        public static void InitializeRareList()
        {
            // Rare List
            RareList.Clear();
            RareList.Add("ペコリム");

            Save("Rare");
        }

        public static void InitializeSuperRareList()
        {
            // Normal List
            SuperRareList.Clear();
            SuperRareList.Add("手塚咲");
            SuperRareList.Add("七瀨七海");
            SuperRareList.Add("淺見真紀子");
            Save("Super Rare");
        }

        public static int Count
        { get { return Name.Count; } }

        public static string GetName(int index)
        { return Name[index]; }

        public static void Save(string type)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            switch (type)
            {
                case "Normal":
                    localSettings.Values["NameList"] = JsonConvert.SerializeObject(Name);
                    break;

                case "Rare":
                    localSettings.Values["RareList"] = JsonConvert.SerializeObject(RareList);
                    break;

                case "Super Rare":
                    localSettings.Values["SuperRareList"] = JsonConvert.SerializeObject(SuperRareList);
                    break;

                case "All":
                default:
                    localSettings.Values["NameList"] = JsonConvert.SerializeObject(Name);
                    localSettings.Values["RareList"] = JsonConvert.SerializeObject(RareList);
                    localSettings.Values["SuperRareList"] = JsonConvert.SerializeObject(SuperRareList);
                    break;
            }
        }

        public static void Load()
        {
            var localeSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (localeSettings.Values.ContainsKey("NameList"))
            {
                Name = JsonConvert.DeserializeObject<List<string>>((string)localeSettings.Values["NameList"]);
            }
            else
            {
                InitializeNormalList();
            }
            if (localeSettings.Values.ContainsKey("RareList"))
            {
                RareList = JsonConvert.DeserializeObject<List<string>>((string)localeSettings.Values["RareList"]);
            }
            else
            {
                InitializeRareList();
            }
            if (localeSettings.Values.ContainsKey("SuperRareList"))
            {
                SuperRareList = JsonConvert.DeserializeObject<List<string>>((string)localeSettings.Values["SuperRareList"]);
            }
            else
            {
                InitializeSuperRareList();
            }
        }
    }
}