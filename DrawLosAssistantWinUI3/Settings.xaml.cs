using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using DrawLosAssistantWinUI3.Models;
using Windows.Storage;
using Windows.Storage.Provider;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DrawLosAssistantWinUI3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Settings : Page
    {
        public Settings()
        {
            this.InitializeComponent();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            NameList.InitializeList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            NameList.Name.Add(NameList.Count, Input.Text);
            NameList.Save("Normal");
            Input.Text = "";
        }

        private void AddRare_Click(object sender, RoutedEventArgs e)
        {
            NameList.RareList.Add(NameList.Count, Input.Text);
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
                SaveStatus.Title = "����ɹ�";
                SaveStatus.Content = "��ͨ��������ɹ�";
                SaveStatus.IsOpen = true;
                await Task.Delay(3000);
                SaveStatus.IsOpen = false;

            }

        }

        private async void Export_Click(object sender, RoutedEventArgs e)
        {
            var ExportLocationPicker = new Windows.Storage.Pickers.FileSavePicker();
            ExportLocationPicker.SuggestedFileName = "NameList";
            ExportLocationPicker.FileTypeChoices.Add("Json�ļ�", new List<string>() { ".json" });
            var localSettings = ApplicationData.Current.LocalSettings;
            nint hWnd = WinRT.Interop.WindowNative.GetWindowHandle(App.m_window);
            WinRT.Interop.InitializeWithWindow.Initialize(ExportLocationPicker, hWnd);

            StorageFile exportFile = await ExportLocationPicker.PickSaveFileAsync();

            if (exportFile != null)
            {
                CachedFileManager.DeferUpdates(exportFile);

                await FileIO.WriteTextAsync(exportFile, localSettings.Values["NameList"].ToString());
                FileUpdateStatus updateStatus = await CachedFileManager.CompleteUpdatesAsync(exportFile);
                if (updateStatus == FileUpdateStatus.Complete)
                {
                    // �����ɹ�Banner
                    SaveStatus.Severity = InfoBarSeverity.Success;
                    SaveStatus.Title = "����ɹ�";
                    SaveStatus.Content = "��ͨ��������ɹ�";
                    SaveStatus.IsOpen = true;
                    await Task.Delay(3000);
                    SaveStatus.IsOpen = false;
                }
                if (updateStatus == FileUpdateStatus.Failed)
                {
                    // ����ʧ��Banner
                    SaveStatus.Severity = InfoBarSeverity.Error;
                    SaveStatus.Title = "����ʧ��";
                    SaveStatus.Content = "�����Ƿ��ж�ӦȨ�ޣ��ļ��Ƿ�ռ�õ�";
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
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(App.m_window);
            WinRT.Interop.InitializeWithWindow.Initialize(ImportLocationPicker, hWnd);

            StorageFile file = await ImportLocationPicker.PickSingleFileAsync();

            if (file != null)
            {
                string ImportContent = await FileIO.ReadTextAsync(file);
                localSettings.Values["RareList"] = ImportContent;
                SaveStatus.Severity = InfoBarSeverity.Success;
                SaveStatus.Title = "����ɹ�";
                SaveStatus.Content = "R��������ɹ�";
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
            nint hWnd = WinRT.Interop.WindowNative.GetWindowHandle(App.m_window);
            WinRT.Interop.InitializeWithWindow.Initialize(ExportLocationPicker, hWnd);

            StorageFile exportFile = await ExportLocationPicker.PickSaveFileAsync();

            if (exportFile != null)
            {
                CachedFileManager.DeferUpdates(exportFile);

                await FileIO.WriteTextAsync(exportFile, localSettings.Values["RareList"].ToString());
                FileUpdateStatus updateStatus = await CachedFileManager.CompleteUpdatesAsync(exportFile);
                if (updateStatus == FileUpdateStatus.Complete)
                {
                    // �����ɹ�Banner
                    SaveStatus.Severity = InfoBarSeverity.Success;
                    SaveStatus.Title = "����ɹ�";
                    SaveStatus.Content = "R��������ɹ�";
                    SaveStatus.IsOpen = true;
                    await Task.Delay(3000);
                    SaveStatus.IsOpen = false;
                }
                if (updateStatus == FileUpdateStatus.Failed)
                {
                    // ����ʧ��Banner
                    SaveStatus.Severity = InfoBarSeverity.Error;
                    SaveStatus.Title = "����ʧ��";
                    SaveStatus.Content = "�����Ƿ��ж�ӦȨ�ޣ��ļ��Ƿ�ռ�õ�";
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
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(App.m_window);
            WinRT.Interop.InitializeWithWindow.Initialize(ImportLocationPicker, hWnd);

            StorageFile file = await ImportLocationPicker.PickSingleFileAsync();

            if (file != null)
            {
                string ImportContent = await FileIO.ReadTextAsync(file);
                localSettings.Values["SuperRareList"] = ImportContent;
                SaveStatus.Severity = InfoBarSeverity.Success;
                SaveStatus.Title = "����ɹ�";
                SaveStatus.Content = "SR��������ɹ�";
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
            nint hWnd = WinRT.Interop.WindowNative.GetWindowHandle(App.m_window);
            WinRT.Interop.InitializeWithWindow.Initialize(ExportLocationPicker, hWnd);

            StorageFile exportFile = await ExportLocationPicker.PickSaveFileAsync();

            if (exportFile != null)
            {
                CachedFileManager.DeferUpdates(exportFile);

                await FileIO.WriteTextAsync(exportFile, localSettings.Values["SuperRareList"].ToString());
                FileUpdateStatus updateStatus = await CachedFileManager.CompleteUpdatesAsync(exportFile);
                if (updateStatus == FileUpdateStatus.Complete)
                {
                    // �����ɹ�Banner
                    SaveStatus.Severity = InfoBarSeverity.Success;
                    SaveStatus.Title = "����ɹ�";
                    SaveStatus.Content = "SR��������ɹ�";
                    SaveStatus.IsOpen = true;
                    await Task.Delay(3000);
                    SaveStatus.IsOpen = false;
                }
                if (updateStatus == FileUpdateStatus.Failed)
                {
                    // ����ʧ��Banner
                    SaveStatus.Severity = InfoBarSeverity.Error;
                    SaveStatus.Title = "����ʧ��";
                    SaveStatus.Content = "�����Ƿ��ж�ӦȨ�ޣ��ļ��Ƿ�ռ�õ�";
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
    }
}