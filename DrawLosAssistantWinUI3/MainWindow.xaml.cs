using DrawLosAssistantWinUI3.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DrawLosAssistantWinUI3
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private bool LoadingBreak = false;

        public MainWindow()
        {
            this.InitializeComponent();

            if ((!ApplicationData.Current.LocalSettings.Values.ContainsKey("RefreshedVer") && ApplicationData.Current.LocalSettings.Values.ContainsKey("AudioType") && !ApplicationData.Current.LocalSettings.Values.ContainsKey("IsSettingsVisible")) || ApplicationData.Current.LocalSettings.Values["RefreshVer"].Equals(false))
            {
                ApplicationData.Current.LocalSettings.Values.Clear();

                ApplicationData.Current.LocalSettings.Values["RefreshVer"] = true;
                LoadingBreak = true;
            }
            if (!LoadingBreak)
            {
                MainFrame.Navigate(typeof(Homepage));
                NameList.Load();
            }
            else
            {
                MainFrame.Navigate(typeof(BlankPage2));
            }

            // 初始化部分设置
            if (!ApplicationData.Current.LocalSettings.Values.ContainsKey("AudioType"))
            {
                ApplicationData.Current.LocalSettings.Values["AudioType"] = "External";
            }

            if (!ApplicationData.Current.LocalSettings.Values.ContainsKey("IsSettingsVisible"))
            {
                ApplicationData.Current.LocalSettings.Values["IsSettingsVisible"] = "1";
            }
            else if (ApplicationData.Current.LocalSettings.Values["IsSettingsVisible"].ToString() == "0")
            {
                this.Nav.IsSettingsVisible = false;
            }

            if (!ApplicationData.Current.LocalSettings.Values.ContainsKey("IsRareEnabled"))
            {
                ApplicationData.Current.LocalSettings.Values["IsRareEnabled"] = true;
            }
            if (!ApplicationData.Current.LocalSettings.Values.ContainsKey("IsSuperRareEnabled"))
            {
                ApplicationData.Current.LocalSettings.Values["IsSuperRareEnabled"] = true;
            }
        }

        private void Nav_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                MainFrame.Navigate(typeof(Settings));
            }
            else
            {
                switch (args.InvokedItemContainer.Tag.ToString())
                {
                    case "Home":
                        MainFrame.Navigate(typeof(Homepage));
                        break;

                    case "Debug":
                        MainFrame.Navigate(typeof(BlankPage1));
                        break;

                    case "Gacha":
                        MainFrame.Navigate(typeof(Gacha));
                        break;
                }
            }
        }

        private void Window_Activated(object sender, WindowActivatedEventArgs args)
        {
#if DEBUG
            DebugOption.Visibility = Visibility.Visible;
#endif

            if (!LoadingBreak) Nav.SelectedItem = Nav.MenuItems.OfType<Microsoft.UI.Xaml.Controls.NavigationViewItem>().First();
        }

        private async void RestartApp(XamlRoot root)
        {
            var dialog = new RestartDialog();
            dialog.XamlRoot = root;
            await dialog.ShowAsync();
        }

        private void Nav_Loaded(object sender, RoutedEventArgs e)
        {
            if (LoadingBreak) { RestartApp(this.Content.XamlRoot); }
        }
    }
}