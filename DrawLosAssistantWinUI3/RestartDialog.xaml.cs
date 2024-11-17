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
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DrawLosAssistantWinUI3
{
    public sealed partial class RestartDialog : ContentDialog
    {
        public RestartDialog()
        {
            this.InitializeComponent();
        }

        private async void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(1000);
                Progress.Value += 1;
            }

            AppRestartFailureReason reason = await CoreApplication.RequestRestartAsync("");

            if (reason == AppRestartFailureReason.Other || reason == AppRestartFailureReason.NotInForeground)
            {
                this.Title = "重启失败，3秒后将关闭";
                Progress.Value = 0;
                Progress.Maximum = 3;
                for (int i = 0; i < 3; i++)
                {
                    await Task.Delay(1000);
                    Progress.Value += 1;
                }
                Application.Current.Exit();
            }
        }
    }
}
