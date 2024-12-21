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
        private bool RefreshDebug = false;
        private bool LoadingBreak = false;

        public MainWindow()
        {
            this.InitializeComponent();

            ExtendsContentIntoTitleBar = true;

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
            LogRecord.Initialize(); 
            LogRecord.DelayUpdate = true;
            if (!ApplicationData.Current.LocalSettings.Values.ContainsKey("AudioType"))
            {
                ApplicationData.Current.LocalSettings.Values["AudioType"] = "External";
                LogRecord.Add("音频选项被重置，疑似应用被重置");
            }

            if (!ApplicationData.Current.LocalSettings.Values.ContainsKey("IsSettingsVisible"))
            {
                ApplicationData.Current.LocalSettings.Values["IsSettingsVisible"] = "1";
                LogRecord.Add("设置可见性选项被重置，疑似应用被重置");
            }
            else if (ApplicationData.Current.LocalSettings.Values["IsSettingsVisible"].ToString() == "0")
            {
                this.Nav.IsSettingsVisible = false;
            }

            if (!ApplicationData.Current.LocalSettings.Values.ContainsKey("IsRareEnabled"))
            {
                ApplicationData.Current.LocalSettings.Values["IsRareEnabled"] = true;
                LogRecord.Add("Rare名单选项被重置，疑似应用被重置");
            }
            
            LogRecord.DelayUpdate = false;

            if (!ApplicationData.Current.LocalSettings.Values.ContainsKey("IsSuperRareEnabled"))
            {
                ApplicationData.Current.LocalSettings.Values["IsSuperRareEnabled"] = true;
                LogRecord.Add("Super Rare选项被重置，疑似应用被重置");
            }



            if ((!ApplicationData.Current.LocalSettings.Values.ContainsKey("RefreshedVer") &&
                ApplicationData.Current.LocalSettings.Values["RefreshVer"] != null &&
                ApplicationData.Current.LocalSettings.Values.ContainsKey("AudioType") &&
                !ApplicationData.Current.LocalSettings.Values.ContainsKey("IsSettingsVisible")) ||
                RefreshDebug)
            {
                ApplicationData.Current.LocalSettings.Values.Clear();

                ApplicationData.Current.LocalSettings.Values["RefreshVer"] = true;
                LoadingBreak = true;
            }
        }

        private void Nav_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                MainFrame.Navigate(typeof(NewSettings));
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
                    case "Log":
                        MainFrame.Navigate(typeof(LogPage));
                        break;
                }
            }
        }

        private void Window_Activated(object sender, WindowActivatedEventArgs args)
        {
           
        }

        private async void RestartApp(XamlRoot root)
        {
            var dialog = new RestartDialog();
            dialog.XamlRoot = root;
            await dialog.ShowAsync();
        }

        private void Nav_Loaded(object sender, RoutedEventArgs e)
        {
#if DEBUG
            DebugOption.Visibility = Visibility.Visible;
            LogRecord.Add("尝试以Debug标签启动");
#endif
            if (!LoadingBreak) Nav.SelectedItem = Nav.MenuItems.OfType<Microsoft.UI.Xaml.Controls.NavigationViewItem>().First();
            if (LoadingBreak) { RestartApp(this.Content.XamlRoot); }
        }

        private async void SuperSecretButton_Click(object sender, RoutedEventArgs e)
        {
            var CmdPromptDialog = new CommandPrompt();
            CmdPromptDialog.XamlRoot = Nav.XamlRoot;
            LogRecord.Add("进入命令窗口");

            await CmdPromptDialog.ShowAsync();
        }
    }
}