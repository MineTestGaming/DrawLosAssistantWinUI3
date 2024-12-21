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
using DrawLosAssistantWinUI3.Models;
using Microsoft.UI.Windowing;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DrawLosAssistantWinUI3
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CompactOverlayPickup : Window
    {
        public CompactOverlayPickup()
        {
            this.InitializeComponent();
        }

        private void CompactOverlayPickup_OnClosed(object sender, WindowEventArgs args)
        {
            if (App.m_window?.AppWindow == null) return;
            if (App.m_window.AppWindow.Presenter is OverlappedPresenter presenter)
            {
                presenter.Maximize();
            }
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
           ExtendsContentIntoTitleBar = true;
        }

        private void Picking_OnClick(object sender, RoutedEventArgs e)
        {
            Result.Text = RandomLogic.CommonRandom();
        }
    }
}