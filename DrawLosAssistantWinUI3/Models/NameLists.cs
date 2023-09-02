using Newtonsoft.Json;
using System.Collections.Generic;

namespace DrawLosAssistantWinUI3.Models
{
    public class NameList
    {
        public static Dictionary<int, string> Name = new Dictionary<int, string>();
        //  public static Dictionary<int, string> RareList = new Dictionary<int, string>();
        public static Dictionary<int, string> SuperRareList = new Dictionary<int, string>();

        public static void InitializeList()
        {
            InitializeNormalList();
            // InitializeRareList();
            InitializeSuperRareList();
        }


        public static void InitializeNormalList()
        {
            // Normal List
            Name.Clear();
            Name.Add(Name.Count, "茅森月歌");
            Name.Add(Name.Count, "和泉由希");
            Name.Add(Name.Count, "逢川惠");
            Name.Add(Name.Count, "東城司");
            Name.Add(Name.Count, "朝倉可憐");
            Name.Add(Name.Count, "國見玉");
            Save("Normal");
        }

        /*  public static void InitializeRareList()
          {
              // Rare List
              RareList.Clear();
              RareList.Add(RareList.Count, "ペコリム");

              Save("Rare");
          } */

        public static void InitializeSuperRareList()
        {
            // Normal List
            SuperRareList.Clear();
            SuperRareList.Add(SuperRareList.Count, "手塚咲");
            SuperRareList.Add(SuperRareList.Count, "七瀨七海");
            SuperRareList.Add(SuperRareList.Count, "淺見真紀子");
            Save("Super Rare");
        }



        public static int Count { get { return Name.Count; } }

        public static string GetName(int index) { return Name[index]; }

        public static void Save(string type)
        {
            var localeSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            switch (type)
            {
                default:
                    localeSettings.Values["NameList"] = JsonConvert.SerializeObject(Name);
                    // localeSettings.Values["RareList"] = JsonConvert.SerializeObject(RareList);
                    localeSettings.Values["SuperRareList"] = JsonConvert.SerializeObject(SuperRareList);
                    break;
                case "Normal":
                    localeSettings.Values["NameList"] = JsonConvert.SerializeObject(Name);
                    break;
                /*  case "Rare":
                 *  localeSettings.Values["RareList"] = JsonConvert.SerializeObject(RareList);
                 *  break; 
                 */
                case "Super Rare":
                    localeSettings.Values["SuperRareList"] = JsonConvert.SerializeObject(SuperRareList);
                    break;
                case "All":
                    localeSettings.Values["NameList"] = JsonConvert.SerializeObject(Name);
                    // localeSettings.Values["RareList"] = JsonConvert.SerializeObject(RareList);
                    localeSettings.Values["SuperRareList"] = JsonConvert.SerializeObject(SuperRareList);
                    break;
            }
        }

        public static void Load()
        {
            var localeSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (localeSettings.Values.ContainsKey("NameList"))
            {
                Name = JsonConvert.DeserializeObject<Dictionary<int, string>>((string)localeSettings.Values["NameList"]);
            }
            else
            {
                InitializeNormalList();
            }
            /* if (localeSettings.Values.ContainsKey("RareList"))
             {
                 Name = JsonConvert.DeserializeObject<Dictionary<int, string>>((string)localeSettings.Values["RareList"]);
             }
             else
             {
                 InitializeRareList();
             } */
            if (localeSettings.Values.ContainsKey("SuperRareList"))
            {
                SuperRareList = JsonConvert.DeserializeObject<Dictionary<int, string>>((string)localeSettings.Values["SuperRareList"]);
            }
            else
            {
                InitializeSuperRareList();
            }
        }


    }

}
