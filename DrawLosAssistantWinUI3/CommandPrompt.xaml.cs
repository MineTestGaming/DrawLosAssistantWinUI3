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
using DrawLosAssistantWinUI3.FromWinUI3Gallery;
using Windows.ApplicationModel.Core;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DrawLosAssistantWinUI3
{
    public sealed partial class CommandPrompt : ContentDialog
    {
        public CommandPrompt()
        {
            this.InitializeComponent();
        }

        private bool ClosingSign = false;

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            switch (CommandInput.Text)
            {
                case "Password":
                    var SettingsWindow = WindowHelperUtillity.WindowHelper.CreateWindow();
                    SettingsWindow.Content = new PasswordVerify();
                    SettingsWindow.Activate();
                    ClosingSign = true;
                    break;
                default:
                    ErrorMsg.Text = "输入的指令不存在，请检查后输入";
                    ErrorMsg.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            if (!ClosingSign)
            {
                args.Cancel = true;
            }
        }

        private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            ClosingSign = true;
        }
    }
}