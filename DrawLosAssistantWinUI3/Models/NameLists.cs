using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawLosAssistantWinUI3.Models
{
    public class NameList
    {
        public static Dictionary<int, string> Name = new Dictionary<int, string>();

        public static void InitializeList()
        {
            Name.Clear();
            Name.Add(Name.Count, "茅森月歌");
            Name.Add(Name.Count, "和泉由希");
            Name.Add(Name.Count, "逢川惠");
            Name.Add(Name.Count, "東城司");
            Name.Add(Name.Count, "朝倉可憐");
            Name.Add(Name.Count, "國見玉");
            Save(Name);
        }

        public static int Count { get { return Name.Count; } }

        public static string GetName(int index) { return Name[index]; }

        public static void Save(Dictionary<int, string> names)
        {
            var localeSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localeSettings.Values["NameList"] = JsonConvert.SerializeObject(names);
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
                InitializeList();
            }
        }
    }

}
