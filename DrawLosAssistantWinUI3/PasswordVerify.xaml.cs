using DrawLosAssistantWinUI3.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Security.Cryptography;
using System.Text;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DrawLosAssistantWinUI3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PasswordVerify : Page
    {
        public PasswordVerify()
        {
            this.InitializeComponent();
            if (!ApplicationData.Current.LocalSettings.Values.ContainsKey("Password"))
            {
                WrongPwd.Text = "密码未设置";
                WrongPwd.Visibility = Visibility.Visible;
                Password.IsEnabled = false;
                Confirm.IsEnabled = false;
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            var Pwd = ApplicationData.Current.LocalSettings.Values["Password"];
            byte[] EncryptedPwd = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(Password.Password));
            string ConvertedEncPwd = BitConverter.ToString(EncryptedPwd);
            if (ConvertedEncPwd == (string)Pwd)
            {
                PasswordConfirm.Visibility = Visibility.Collapsed;
                EncryptedFrame.Visibility = Visibility.Visible;
                EncryptedFrame.Navigate(typeof(NewSettings));
                LogRecord.Add("密码正确");
            }
            else
            {
                WrongPwd.Visibility = Visibility.Visible;
                Password.Password = String.Empty;
                LogRecord.Add("密码错误");
            }
        }
    }
}