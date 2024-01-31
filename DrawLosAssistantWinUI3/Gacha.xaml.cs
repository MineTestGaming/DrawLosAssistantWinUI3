using DrawLosAssistantWinUI3.Models;
using DrawLosAssistantWinUI3.ResultPage;
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
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DrawLosAssistantWinUI3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Gacha : Page
    {
        public Gacha()
        {
            this.InitializeComponent();
        }

        private void StartGacha_Click(object sender, RoutedEventArgs e)
        {
            string Level = RandomLogic.RandomLevel();

            switch (Level)
            {
                case "Super Rare":
                    this.Frame.Navigate(typeof(SuperRare));
                    break;
                case "Rare":
                    this.Frame.Navigate(typeof(Rare));
                    break;
                case "Common":
                    this.Frame.Navigate(typeof(Common));
                    break;

            }

        }
    }
}
