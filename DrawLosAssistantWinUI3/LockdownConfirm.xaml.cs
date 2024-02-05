using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
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
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DrawLosAssistantWinUI3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LockdownConfirm : ContentDialog
    {
        public LockdownConfirm()
        {
            this.InitializeComponent();
            this.IsPrimaryButtonEnabled = false;
        }

        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["IsSettingsVisible"] = "0";
            App.Current.Exit();
        }

        private async void ConfirmTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ConfirmTextBox.Text == "我确认要锁定名单并隐藏设置")
            {
                ConfirmTextBox.IsEnabled = false;
                int i = 5;
                while(i != 0)
                {
                    PrimaryButtonText = "确定 (" + i + ")";
                    await Task.Delay(1000);
                    i--;
                }
                
                PrimaryButtonText = "确定";
                IsPrimaryButtonEnabled = true;
            }
        }
    }
}
