using DrawLosAssistantWinUI3.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Provider;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DrawLosAssistantWinUI3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Settings : Page
    {
        private nint hWnd = WinRT.Interop.WindowNative.GetWindowHandle(App.m_window);

        public Settings()
        {
            this.InitializeComponent();
            string Audiotype = ApplicationData.Current.LocalSettings.Values["AudioType"].ToString();
            if (Audiotype == "External")
            {
                AudioType.IsOn = true;
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values.Clear();
            App.Current.Exit();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            NameList.Name.Add(NameList.Count, Input.Text);
            NameList.Save("Normal");
            Input.Text = "";
        }

        private void AddRare_Click(object sender, RoutedEventArgs e)
        {
            NameList.RareList.Add(NameList.RareList.Count, Input.Text);
            NameList.Save("Rare");
            InputR.Text = "";
        }

        private void AddSuperRare_Click(object sender, RoutedEventArgs e)
        {
            NameList.SuperRareList.Add(NameList.SuperRareList.Count, InputSR.Text);
            NameList.Save("Super Rare");
            InputSR.Text = "";
        }

        private async void Import_Click(object sender, RoutedEventArgs e)
        {
            var ImportLocationPicker = new Windows.Storage.Pickers.FileOpenPicker();
            ImportLocationPicker.FileTypeFilter.Add(".json");

            var localSettings = ApplicationData.Current.LocalSettings;
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(App.m_window);
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
                    SaveStatus.IsOpen = false;
                }
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

        private async void ShowDevInfo(string devInfo)
        {
            SaveStatus.Severity = InfoBarSeverity.Informational;
            SaveStatus.Title = "Debug Info";
            SaveStatus.Content = devInfo;
            SaveStatus.IsOpen = true;
            await Task.Delay(3000);
            SaveStatus.IsOpen = false;
        }

        private void AudioType_Toggled(object sender, RoutedEventArgs e)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            if (AudioType.IsOn)
            {
                localSettings.Values["AudioType"] = "External";
            }
            else
            {
                localSettings.Values["AudioType"] = "Internal";
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
    }
}