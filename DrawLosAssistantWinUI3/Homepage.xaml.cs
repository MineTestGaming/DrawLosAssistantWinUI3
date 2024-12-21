using DrawLosAssistantWinUI3.FromWinUI3Gallery;
using DrawLosAssistantWinUI3.Models;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

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
            // Level.Text = RandomLogic.RandomLevel();
            Result.Text = RandomLogic.CommonRandom();
        }

        private void CompactOverlayMode_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new CompactOverlayPickup();
            window.AppWindow.SetPresenter(AppWindowPresenterKind.CompactOverlay);
            window.AppWindow.Show();
            if (App.m_window.AppWindow.Presenter is OverlappedPresenter presenter)
            {
                presenter.Minimize();
            }
        }
    }
}