using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;

namespace DrawLosAssistantWinUI3.Models
{
    internal class LogRecord
    {
        public class Event
        {
            public DateTime time { get; set; }
            public string EventData { get; set; }
        }

        public static List<Event> LogEvent = new List<Event>();

        public static bool DelayUpdate = false;

        public static async void Initialize()
        {
            if (await ApplicationData.Current.LocalFolder.TryGetItemAsync("Log.json")== null)
            {
                
                await ApplicationData.Current.LocalFolder.CreateFileAsync("Log.json");
                LogEvent.Add(new Event { time = DateTime.Now, EventData = "第一次启动/日志文件被删除" });
            }
            else
            {
                StorageFile LogFile = await ApplicationData.Current.LocalFolder.GetFileAsync("Log.json");
                string Data = await FileIO.ReadTextAsync(LogFile);

                LogEvent = JsonConvert.DeserializeObject<List<Event>>(Data);
            }
        }

        public static void Add(string @event)
        {
            DateTime currTime = new DateTime();
            currTime = DateTime.Now;
            LogEvent.Add(new Event { time = currTime, EventData = @event });
            if (!DelayUpdate) Save();
            
        }

        private static async void Save()
        {
            StorageFile LogFile = await ApplicationData.Current.LocalFolder.GetFileAsync("Log.json");

            CachedFileManager.DeferUpdates(LogFile);
            await FileIO.WriteTextAsync(LogFile, JsonConvert.SerializeObject(LogEvent));
            await CachedFileManager.CompleteUpdatesAsync(LogFile);
        }

        public static string ExportDev()
        {
            return JsonConvert.SerializeObject(LogEvent, Formatting.Indented);
        }

        public static string Export()
        {
            List<string> dataList = new List<string>();
            foreach (Event e in LogEvent)
            {
                dataList.Add(e.time.ToString() + " " + e.EventData);
            }
            return string.Join(Environment.NewLine, dataList.ToArray());
        }

        public static async void Reset()
        {
            LogEvent.Clear();
            StorageFile LogFile = await ApplicationData.Current.LocalFolder.GetFileAsync("Log.json");
            CachedFileManager.DeferUpdates(LogFile);
            await LogFile.DeleteAsync();
            await CachedFileManager.CompleteUpdatesAsync(LogFile);
            Initialize();
            Add("日志通过软件内部重置");
        }
    }
}