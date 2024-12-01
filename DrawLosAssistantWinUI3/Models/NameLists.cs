using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Linq;
using Windows.Gaming.Preview.GamesEnumeration;

namespace DrawLosAssistantWinUI3.Models
{
    public class NameList
    {
        public enum GachaType
        {
            Normal,
            Rare,
            SuperRare
        }

        public static List<string>[] nameList = new List<string>[3];

        public static void InitializeList()
        {
            InitializeNormalList();
            InitializeRareList();
            InitializeSuperRareList();
        }

        public static void InitializeNormalList()
        {
            if (nameList[(int)GachaType.Normal] == null)
            {
                nameList[(int)GachaType.Normal] = new List<string>();
            }
            // Normal List

            nameList[(int)GachaType.Normal].Clear();
            nameList[(int)GachaType.Normal].Add("茅森月歌");
            nameList[(int)GachaType.Normal].Add("和泉由希");
            nameList[(int)GachaType.Normal].Add("逢川惠");
            nameList[(int)GachaType.Normal].Add("東城司");
            nameList[(int)GachaType.Normal].Add("朝倉可憐");
            nameList[(int)GachaType.Normal].Add("國見玉");
            Save("Normal");
        }

        public static void InitializeRareList()
        {
            if (nameList[(int)GachaType.Rare] == null)
            {
                nameList[(int)GachaType.Rare] = new List<string>();
            }
            // Rare List
            nameList[(int)GachaType.Rare].Clear();
            nameList[(int)GachaType.Rare].Add("ペコリム");

            Save("Rare");
        }

        public static void InitializeSuperRareList()
        {
            if (nameList[(int)GachaType.SuperRare] == null)
            {
                nameList[(int)GachaType.SuperRare] = new List<string>();
            }
            // Normal List
            nameList[(int)GachaType.SuperRare].Clear();
            nameList[(int)GachaType.SuperRare].Add("手塚咲");
            nameList[(int)GachaType.SuperRare].Add("七瀨七海");
            nameList[(int)GachaType.SuperRare].Add("淺見真紀子");
            Save("Super Rare");
        }

        public static int Count
        { get { return nameList[(int)GachaType.Normal].Count; } }

        public static string GetName(int index)
        { return nameList[(int)GachaType.Normal][index]; }

        public static void Save(string type)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            switch (type)
            {
                case "All":
                default:
                    localSettings.Values["UnifiedGachaList"] = JsonConvert.SerializeObject(nameList);
                    break;
            }
        }

        public static void Load()
        {
            var localeSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            if (localeSettings.Values.ContainsKey("UnifiedGachaList"))
            {
                nameList = JsonConvert.DeserializeObject<List<string>[]>((string)localeSettings.Values["UnifiedGachaList"]);
            }
            else
            {
                InitializeList();
            }

            // Backward Compatibility
            if (localeSettings.Values.ContainsKey("NameList"))
            {
                nameList[(int)GachaType.Normal] = JsonConvert.DeserializeObject<List<string>>((string)localeSettings.Values["NameList"]);
                localeSettings.Values.Remove("NameList");
                Save("");
            }

            if (localeSettings.Values.ContainsKey("RareList"))
            {
                nameList[(int)GachaType.Rare] = JsonConvert.DeserializeObject<List<string>>((string)localeSettings.Values["RareList"]);
                localeSettings.Values.Remove("RareList");
                Save("");
            }

            if (localeSettings.Values.ContainsKey("SuperRareList"))
            {
                nameList[(int)GachaType.SuperRare] = JsonConvert.DeserializeObject<List<string>>((string)localeSettings.Values["SuperRareList"]);
                localeSettings.Values.Remove("SuperRareList");
                Save("");
            }
            
        }
    }
}