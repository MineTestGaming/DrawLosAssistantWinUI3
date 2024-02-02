using DrawLosAssistantWinUI3.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Linq;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DrawLosAssistantWinUI3
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            MainFrame.Navigate(typeof(Homepage));
            NameList.Load();

            // 初始化部分设置
            if (!ApplicationData.Current.LocalSettings.Values.ContainsKey("AudioType"))
            {
                ApplicationData.Current.LocalSettings.Values["AudioType"] = "External";
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

        public static void ResetedMsg()
        {
        }

        private void Window_Activated(object sender, WindowActivatedEventArgs args)
        {
            Nav.SelectedItem = Nav.MenuItems.OfType<Microsoft.UI.Xaml.Controls.NavigationViewItem>().First();
        }
    }
}