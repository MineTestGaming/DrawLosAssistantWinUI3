using DrawLosAssistantWinUI3.ResultPage;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DrawLosAssistantWinUI3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        public BlankPage1()
        {
            this.InitializeComponent();
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("Password"))
            {
                DisplayPassword.IsEnabled = true;
            }
        }

        private void CommonResult_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Common));
        }

        private void RareResult_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Rare));
        }

        private void SuperRareResult_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SuperRare));
        }

        private void NewSetting_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NewSettings));
        }

        private void DisplayPassword_Click(object sender, RoutedEventArgs e)
        {
            PasswordDisplay.Text = "密码: " + ApplicationData.Current.LocalSettings.Values["Password"];
        }

        private void ShowSettings_Click(object sender, RoutedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values["IsSettingsVisible"] = "1";
        }

        private void ClearPassword_Click(object sender, RoutedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values.Remove("Password");
        }
    }
}