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
using DrawLosAssistantWinUI3.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DrawLosAssistantWinUI3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Homepage : Page
    {
        public Homepage()
        {
            this.InitializeComponent();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            NameList.Load();

            Random random = new Random();
            int MaxNum = NameList.Count - 1;
            int resultNum = random.Next(0, MaxNum);
            int levelNum = random.Next(0, 1000);


            if (levelNum > 900)
            {
                Level.Text = "Super Rare";

            }
            else if (levelNum > 850)
            {
                Level.Text = "Rare";
            }
            else
            {
                Level.Text = "Common";
            }


            Result.Text = NameList.GetName(resultNum);

        }
    }
}
