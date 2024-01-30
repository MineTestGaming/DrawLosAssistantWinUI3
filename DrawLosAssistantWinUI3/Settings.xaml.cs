using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using DrawLosAssistantWinUI3.Models;
using Windows.Storage;
using Windows.Storage.Provider;
using System.Windows.Interop;
using DrawLosAssistantWinUI3.FromWinUI3Gallery;
using Windows.Storage.Pickers;


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

        private void AddSuperRare_Click(object sender, RoutedEventArgs e)
        {
            NameList.SuperRareList.Add(NameList.SuperRareList.Count, InputSR.Text);
            NameList.Save("Super Rare");
            InputSR.Text = "";
        }


        private void Import_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void Export_Click(object sender, RoutedEventArgs e)
        {
            var ExportLocationPicker = new Windows.Storage.Pickers.FileSavePicker();
            ExportLocationPicker.SuggestedFileName = "NameList";
            ExportLocationPicker.FileTypeChoices.Add("Json Files", new List<string>() { ".json" }); 
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
                    // 导出成功Banner
                }
                if (updateStatus == FileUpdateStatus.Failed)
                {
                    // 导出失败Banner
                }
            }

        }

        private void ImportSuperRare_Click(object sender, RoutedEventArgs e)
        {

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
                    // 导出成功Banner
                }
                if (updateStatus == FileUpdateStatus.Failed)
                {
                    // 导出失败Banner
                }
            }

        }
    }
}
