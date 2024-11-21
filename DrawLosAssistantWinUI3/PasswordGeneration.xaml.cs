using System;
using System.Collections.Generic;
using System.IO;
using Windows.Storage;
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
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DrawLosAssistantWinUI3
{
    public sealed partial class PasswordGeneration : ContentDialog
    {
        private bool ClosingSig = false;

        public PasswordGeneration()
        {
            this.InitializeComponent();
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            

            if (PasswordInput.Password != "" && PasswordInput.Password != null)
            {
                byte[] EncodedPwd = Encoding.UTF8.GetBytes(PasswordInput.Password);
                ApplicationData.Current.LocalSettings.Values["Password"] = BitConverter.ToString(SHA256.Create().ComputeHash(EncodedPwd)); // Compute Password
                PasswordInput.Password = string.Empty;
                ApplicationData.Current.LocalSettings.Values["IsSettingsVisible"] = "0";

                RestartCount.Visibility = Visibility.Visible;
                Tips.Text = "10秒后应用将重启";
                for (int i = 0; i < 10; i++)
                {
                    await Task.Delay(1000);
                    RestartCount.Value++;
                }
                var RestartLog = await CoreApplication.RequestRestartAsync("");

                if (RestartLog == AppRestartFailureReason.Other || RestartLog == AppRestartFailureReason.NotInForeground)
                {
                    Application.Current.Exit();
                }


            }
        }

        private void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            args.Cancel = !ClosingSig;
        }

        private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            ClosingSig = true;
        }
    }
}