using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using CommunityToolkit;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using DrawLosAssistantWinUI3.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Windows.Storage.Provider;
using Windows.Storage;
using Windows.ApplicationModel.Core;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DrawLosAssistantWinUI3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewSettings : Page
    {
        private nint hWnd = WinRT.Interop.WindowNative.GetWindowHandle(App.m_window);

        public NewSettings()
        {
            this.InitializeComponent();
            string Audiotype = ApplicationData.Current.LocalSettings.Values["AudioType"].ToString();

            if (ApplicationData.Current.LocalSettings.Values["IsRareEnabled"].ToString() == "True")
            {
                RareToggle.IsOn = true;
            }
            else
            {
                AddListCardR.IsEnabled = RareToggle.IsOn;
                ImportRtxtCard.IsEnabled = false;
                ImportRCard.IsEnabled = false;
                ExportRCard.IsEnabled = false;
                RareBGA.IsEnabled = RareToggle.IsOn;
            }

            if (ApplicationData.Current.LocalSettings.Values["IsSuperRareEnabled"].ToString() == "True")
            {
                SuperRareToggle.IsOn = true;
            }
            else
            {
                AddListCardSR.IsEnabled = false;
                ImportSRtxtCard.IsEnabled = false;
                ImportSRCard.IsEnabled = false;
                ExportSRCard.IsEnabled = false;
                SuperRareBGA.IsEnabled = SuperRareToggle.IsOn;
            }
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("Password"))
            {
                DelPwdCard.IsEnabled = true;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            NameList.Name.Add(Input.Text);
            NameList.Save("Normal");
            Input.Text = "";
        }

        private void AddRare_Click(object sender, RoutedEventArgs e)
        {
            NameList.RareList.Add(InputR.Text);
            NameList.Save("Rare");
            InputR.Text = "";
        }

        private void AddSuperRare_Click(object sender, RoutedEventArgs e)
        {
            NameList.SuperRareList.Add(InputSR.Text);
            NameList.Save("Super Rare");
            InputSR.Text = "";
        }

        private async void AudioLocation_Click(object sender, RoutedEventArgs e)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            var filePicker = new Windows.Storage.Pickers.FileOpenPicker();
            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hWnd);
            filePicker.FileTypeFilter.Add("*");
            StorageFile storageFile = await filePicker.PickSingleFileAsync();

            if (storageFile != null)
            {
                localSettings.Values["AudioPath"] = storageFile.Path;
            }
        }

        //private void AudioType_Toggled(object sender, RoutedEventArgs e)
        //{
        //    var localSettings = ApplicationData.Current.LocalSettings;
        //    if (AudioType.IsOn)
        //    {
        //        localSettings.Values["AudioType"] = "External";
        //        CustomizeAudio.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        localSettings.Values["AudioType"] = "Internal";
        //        CustomizeAudio.Visibility = Visibility.Collapsed;
        //    }
        //}

        private async void Clear_Click(object sender, RoutedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values.Clear();
            SaveStatus.Severity = InfoBarSeverity.Warning;
            SaveStatus.Title = "应用已重置";
            SaveStatus.Content = "将在1s后关闭";

            SaveStatus.IsOpen = true;
            await Task.Delay(1000);
            App.Current.Exit();
        }

        private async void Export_Click(object sender, RoutedEventArgs e)
        {
            var ExportLocationPicker = new Windows.Storage.Pickers.FileSavePicker();
            ExportLocationPicker.SuggestedFileName = "NameList";
            ExportLocationPicker.FileTypeChoices.Add("Json文件", new List<string>() { ".json" });
            var localSettings = ApplicationData.Current.LocalSettings;
            WinRT.Interop.InitializeWithWindow.Initialize(ExportLocationPicker, hWnd);

            StorageFile exportFile = await ExportLocationPicker.PickSaveFileAsync();

            if (exportFile != null)
            {
                CachedFileManager.DeferUpdates(exportFile);

                await FileIO.WriteTextAsync(exportFile, localSettings.Values["NameList"].ToString());
                FileUpdateStatus updateStatus = await CachedFileManager.CompleteUpdatesAsync(exportFile);
                if (updateStatus == FileUpdateStatus.Complete)
                {
                    // 导出成功Banner
                    SaveStatus.Severity = InfoBarSeverity.Success;
                    SaveStatus.Title = "保存成功";
                    SaveStatus.Content = "普通名单保存成功";
                    SaveStatus.IsOpen = true;

                    await Task.Delay(3000);
                    SaveStatus.IsOpen = false;
                }
                if (updateStatus == FileUpdateStatus.Failed)
                {
                    // 导出失败Banner
                    SaveStatus.Severity = InfoBarSeverity.Error;
                    SaveStatus.Title = "保存失败";
                    SaveStatus.Content = "请检查是否有对应权限，文件是否被占用等";

                    SaveStatus.IsOpen = true;
                    await Task.Delay(3000);

                    SaveStatus.IsOpen = true;
                }
            }
        }

        private async void ExportR_Click(object sender, RoutedEventArgs e)
        {
            var ExportLocationPicker = new Windows.Storage.Pickers.FileSavePicker();
            ExportLocationPicker.SuggestedFileName = "RareNameList";
            ExportLocationPicker.FileTypeChoices.Add("Json Files", new List<string>() { ".json" });
            var localSettings = ApplicationData.Current.LocalSettings;
            WinRT.Interop.InitializeWithWindow.Initialize(ExportLocationPicker, hWnd);

            StorageFile exportFile = await ExportLocationPicker.PickSaveFileAsync();

            if (exportFile != null)
            {
                CachedFileManager.DeferUpdates(exportFile);

                await FileIO.WriteTextAsync(exportFile, localSettings.Values["RareList"].ToString());
                FileUpdateStatus updateStatus = await CachedFileManager.CompleteUpdatesAsync(exportFile);
                if (updateStatus == FileUpdateStatus.Complete)
                {
                    // 导出成功Banner
                    SaveStatus.Severity = InfoBarSeverity.Success;
                    SaveStatus.Title = "保存成功";
                    SaveStatus.Content = "R名单保存成功";

                    SaveStatus.IsOpen = true;
                    await Task.Delay(3000);

                    SaveStatus.IsOpen = false;
                }
                if (updateStatus == FileUpdateStatus.Failed)
                {
                    // 导出失败Banner
                    SaveStatus.Severity = InfoBarSeverity.Error;
                    SaveStatus.Title = "保存失败";
                    SaveStatus.Content = "请检查是否有对应权限，文件是否被占用等";

                    SaveStatus.IsOpen = true;
                    await Task.Delay(3000);

                    SaveStatus.IsOpen = false;
                }
            }
        }

        private async void ExportSR_Click(object sender, RoutedEventArgs e)
        {
            var ExportLocationPicker = new Windows.Storage.Pickers.FileSavePicker();
            ExportLocationPicker.SuggestedFileName = "SuperRareNameList";
            ExportLocationPicker.FileTypeChoices.Add("Json Files", new List<string>() { ".json" });
            var localSettings = ApplicationData.Current.LocalSettings;
            WinRT.Interop.InitializeWithWindow.Initialize(ExportLocationPicker, hWnd);

            StorageFile exportFile = await ExportLocationPicker.PickSaveFileAsync();

            if (exportFile != null)
            {
                CachedFileManager.DeferUpdates(exportFile);

                await FileIO.WriteTextAsync(exportFile, localSettings.Values["SuperRareList"].ToString());
                FileUpdateStatus updateStatus = await CachedFileManager.CompleteUpdatesAsync(exportFile);
                if (updateStatus == FileUpdateStatus.Complete)
                {
                    // 导出成功Banner
                    SaveStatus.Severity = InfoBarSeverity.Success;
                    SaveStatus.Title = "保存成功";
                    SaveStatus.Content = "SR名单保存成功";

                    SaveStatus.IsOpen = true;
                    await Task.Delay(3000);

                    SaveStatus.IsOpen = false;
                }
                if (updateStatus == FileUpdateStatus.Failed)
                {
                    // 导出失败Banner
                    SaveStatus.Severity = InfoBarSeverity.Error;
                    SaveStatus.Title = "保存失败";
                    SaveStatus.Content = "请检查是否有对应权限，文件是否被占用等";

                    SaveStatus.IsOpen = true;
                    await Task.Delay(3000);

                    SaveStatus.IsOpen = false;
                }
            }
        }

        private async void Import_Click(object sender, RoutedEventArgs e)
        {
            var ImportLocationPicker = new Windows.Storage.Pickers.FileOpenPicker();
            ImportLocationPicker.FileTypeFilter.Add(".json");

            var localSettings = ApplicationData.Current.LocalSettings;
            WinRT.Interop.InitializeWithWindow.Initialize(ImportLocationPicker, hWnd);

            StorageFile file = await ImportLocationPicker.PickSingleFileAsync();

            if (file != null)
            {
                string ImportContent = await FileIO.ReadTextAsync(file);
                localSettings.Values["NameList"] = ImportContent;
                SaveStatus.Severity = InfoBarSeverity.Success;
                SaveStatus.Title = "导入成功";
                SaveStatus.Content = "普通名单导入成功";

                SaveStatus.IsOpen = true;
                await Task.Delay(3000);

                SaveStatus.IsOpen = false;
            }
        }

        private async void ImportFromTxt_Click(object sender, RoutedEventArgs e)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            var filePicker = new Windows.Storage.Pickers.FileOpenPicker();
            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hWnd);
            filePicker.FileTypeFilter.Add(".txt");
            StorageFile txt = await filePicker.PickSingleFileAsync();
            Dictionary<int, string> imports = new Dictionary<int, string>();
            int currentLine = 0;

            if (txt != null)
            {
                StreamReader reader = new StreamReader(txt.Path);
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    imports.Add(currentLine, line);
                    currentLine++;
                }
                reader.Close();
                localSettings.Values["NameList"] = JsonConvert.SerializeObject(imports);
                SaveStatus.Severity = InfoBarSeverity.Success;
                SaveStatus.Title = "导入成功";
                SaveStatus.Content = "普通名单导入成功";

                SaveStatus.IsOpen = true;
                await Task.Delay(3000);

                SaveStatus.IsOpen = false;
            }
        }

        private async void ImportRare_Click(object sender, RoutedEventArgs e)
        {
            var ImportLocationPicker = new Windows.Storage.Pickers.FileOpenPicker();
            ImportLocationPicker.FileTypeFilter.Add(".json");
            var localSettings = ApplicationData.Current.LocalSettings;
            WinRT.Interop.InitializeWithWindow.Initialize(ImportLocationPicker, hWnd);

            StorageFile file = await ImportLocationPicker.PickSingleFileAsync();

            if (file != null)
            {
                string ImportContent = await FileIO.ReadTextAsync(file);
                localSettings.Values["RareList"] = ImportContent;
                SaveStatus.Severity = InfoBarSeverity.Success;
                SaveStatus.Title = "导入成功";
                SaveStatus.Content = "Rare名单导入成功";

                SaveStatus.IsOpen = true;
                await Task.Delay(3000);

                SaveStatus.IsOpen = false;
            }
        }

        private async void ImportSuperRare_Click(object sender, RoutedEventArgs e)
        {
            var ImportLocationPicker = new Windows.Storage.Pickers.FileOpenPicker();
            ImportLocationPicker.FileTypeFilter.Add(".json");
            var localSettings = ApplicationData.Current.LocalSettings;

            WinRT.Interop.InitializeWithWindow.Initialize(ImportLocationPicker, hWnd);

            StorageFile file = await ImportLocationPicker.PickSingleFileAsync();

            if (file != null)
            {
                string ImportContent = await FileIO.ReadTextAsync(file);
                localSettings.Values["SuperRareList"] = ImportContent;
                SaveStatus.Severity = InfoBarSeverity.Success;
                SaveStatus.Title = "导入成功";
                SaveStatus.Content = "SR名单导入成功";

                SaveStatus.IsOpen = true;
                await Task.Delay(3000);

                SaveStatus.IsOpen = false;
            }
        }

        private async void NormalVideoSel_Click(object sender, RoutedEventArgs e)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            var FileSelector = new Windows.Storage.Pickers.FileOpenPicker();
            WinRT.Interop.InitializeWithWindow.Initialize(FileSelector, hWnd);
            FileSelector.FileTypeFilter.Add("*");

            StorageFile storageFile = await FileSelector.PickSingleFileAsync();

            if (storageFile != null)
            {
                localSettings.Values["NormalVideoPath"] = storageFile.Path;
            }
        }

        private async void RareImportFromTxt_Click(object sender, RoutedEventArgs e)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            var filePicker = new Windows.Storage.Pickers.FileOpenPicker();
            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hWnd);
            filePicker.FileTypeFilter.Add(".txt");
            StorageFile txt = await filePicker.PickSingleFileAsync();
            Dictionary<int, string> imports = new Dictionary<int, string>();
            int currentLine = 0;

            if (txt != null)
            {
                StreamReader reader = new StreamReader(txt.Path);
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    imports.Add(currentLine, line);
                    currentLine++;
                }
                reader.Close();
                localSettings.Values["RareList"] = JsonConvert.SerializeObject(imports);
                SaveStatus.Title = "导入成功";
                SaveStatus.Content = "Rare名单导入成功";

                SaveStatus.IsOpen = true;
                await Task.Delay(3000);

                SaveStatus.IsOpen = false;
            }
        }

        private async void RareVideoSel_Click(object sender, RoutedEventArgs e)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            var FileSelector = new Windows.Storage.Pickers.FileOpenPicker();
            WinRT.Interop.InitializeWithWindow.Initialize(FileSelector, hWnd);
            FileSelector.FileTypeFilter.Add("*");
            StorageFile storageFile = await FileSelector.PickSingleFileAsync();

            if (storageFile != null)
            {
                localSettings.Values["RareVideoPath"] = storageFile.Path;
            }
        }

        private async void ShowDevInfo(string devInfo)
        {
            SaveStatus.Severity = InfoBarSeverity.Informational;
            SaveStatus.Title = "Debug Info";
            SaveStatus.Content = devInfo;

            SaveStatus.IsOpen = true;
            await Task.Delay(3000);

            SaveStatus.IsOpen = false;
        }

        private async void SRVideoSel_Click(object sender, RoutedEventArgs e)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            var FileSelector = new Windows.Storage.Pickers.FileOpenPicker();
            WinRT.Interop.InitializeWithWindow.Initialize(FileSelector, hWnd);
            FileSelector.FileTypeFilter.Add("*");
            StorageFile storageFile = await FileSelector.PickSingleFileAsync();

            if (storageFile != null)
            {
                localSettings.Values["SuperRareVideoPath"] = storageFile.Path;
            }
        }

        private async void SuperRareImportFromTxt_Click(object sender, RoutedEventArgs e)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            var filePicker = new Windows.Storage.Pickers.FileOpenPicker();
            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hWnd);
            filePicker.FileTypeFilter.Add(".txt");
            StorageFile txt = await filePicker.PickSingleFileAsync();
            Dictionary<int, string> imports = new Dictionary<int, string>();
            int currentLine = 0;

            if (txt != null)
            {
                StreamReader reader = new StreamReader(txt.Path);
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    imports.Add(currentLine, line);
                    currentLine++;
                }
                reader.Close();
                localSettings.Values["SuperRareList"] = JsonConvert.SerializeObject(imports);
                SaveStatus.Title = "导入成功";
                SaveStatus.Content = "SuperRare名单导入成功";

                SaveStatus.IsOpen = true;
                await Task.Delay(3000);

                SaveStatus.IsOpen = false;
            }
        }

        private async void Lockdown_Click(object sender, RoutedEventArgs e)
        {
            LockdownConfirm confirm = new LockdownConfirm();
            confirm.XamlRoot = this.XamlRoot;
            await confirm.ShowAsync();
        }

        private void RareToggle_Toggled(object sender, RoutedEventArgs e)
        {
            AddListCardR.IsEnabled = RareToggle.IsOn;
            ImportRtxtCard.IsEnabled = RareToggle.IsOn;
            ImportRCard.IsEnabled = RareToggle.IsOn;
            ExportRCard.IsEnabled = RareToggle.IsOn;
            RareBGA.IsEnabled = RareToggle.IsOn;

        }

        private void SuperRareToggle_Toggled(object sender, RoutedEventArgs e)
        {
            AddListCardSR.IsEnabled = SuperRareToggle.IsOn;
            ImportSRtxtCard.IsEnabled = SuperRareToggle.IsOn;
            ImportSRCard.IsEnabled = SuperRareToggle.IsOn;
            ExportSRCard.IsEnabled = SuperRareToggle.IsOn;
            SuperRareBGA.IsEnabled = SuperRareToggle.IsOn;

            ApplicationData.Current.LocalSettings.Values["IsSuperRareEnabled"] = SuperRareToggle.IsOn;
        }

        private void AudioTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var LocalSettings = ApplicationData.Current.LocalSettings;
            switch (AudioTypeComboBox.SelectedItem)
            {
                case "视频内嵌音频":
                    LocalSettings.Values["AudioType"] = "External";
                    AudioLocationCard.IsEnabled = false;
                    break;
                case "外置音频":
                    LocalSettings.Values["AudioType"] = "Internal";
                    AudioLocationCard.IsEnabled = true;
                    break;
                default:
                    LocalSettings.Values["AudioType"] = "External";
                    AudioTypeComboBox.SelectedItem = "外置音频";
                    AudioLocationCard.IsEnabled = true;
                    break;
            }

        }

        private void SettingsCard_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void AudioTypeCard_Loaded(object sender, RoutedEventArgs e)
        {
            var LocalSettings = ApplicationData.Current.LocalSettings;

            switch (LocalSettings.Values["AudioType"])
            {
                case "External":
                    AudioTypeComboBox.SelectedItem = "外置音频";
                    AudioLocationCard.IsEnabled = true;
                    break;
                case "Internal":
                    AudioTypeComboBox.SelectedItem = "视频内嵌音频";
                    AudioLocationCard.IsEnabled = false;
                    break;
                default:
                    LocalSettings.Values["AudioType"] = "External";
                    AudioTypeComboBox.SelectedItem = "外置音频";
                    AudioLocationCard.IsEnabled = true;
                    break;
            }
        }

        private async void SetPassword_Click(object sender, RoutedEventArgs e)
        {
            PasswordGeneration pwdWnd = new PasswordGeneration();
            pwdWnd.XamlRoot = this.XamlRoot;

            await pwdWnd.ShowAsync();

        }

        private async void DeletePwd_Click(object sender, RoutedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values.Remove("Password");
            ApplicationData.Current.LocalSettings.Values["IsSettingsVisible"] = "1";
            SaveStatus.Content = "应用将在3秒后重启";
            SaveStatus.IsOpen = true;
            await Task.Delay(3000);
            await CoreApplication.RequestRestartAsync("");
            Application.Current.Exit();
        }
    }
}