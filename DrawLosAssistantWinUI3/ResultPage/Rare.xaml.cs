using DrawLosAssistantWinUI3.Models;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DrawLosAssistantWinUI3.ResultPage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Rare : Page
    {
        private DispatcherQueue dispatcherQueue;

        public Rare()
        {
            this.InitializeComponent();
            dispatcherQueue = DispatcherQueue.GetForCurrentThread();
            GachaLoading.MediaPlayer.MediaEnded += GachaVideoPlayer_MediaEnded;
            GachaLoading.MediaPlayer.AudioCategory = Windows.Media.Playback.MediaPlayerAudioCategory.Other;
            GachaLoading.MediaPlayer.Volume = 0;
        }

        private async void GachaVideoPlayer_MediaEnded(Windows.Media.Playback.MediaPlayer sender, object args)
        {
            await Task.Run(() =>
            {
                dispatcherQueue.TryEnqueue(() =>
                {
                    Skip.Visibility = Visibility.Collapsed;
                    ResultDisplay.Visibility = Visibility.Visible;
                    Result.Text = RandomLogic.RareRandom();
                });
            });
        }

        private void AnotherTry_Click(object sender, RoutedEventArgs e)
        {
            string Level = RandomLogic.RandomLevel();

            switch (Level)
            {
                case "Common":
                    this.Frame.Navigate(typeof(Common));
                    break;

                case "Super Rare":
                    this.Frame.Navigate(typeof(SuperRare));
                    break;

                case "Rare":
                    ResultDisplay.Visibility = Visibility.Collapsed;
                    GachaLoading.MediaPlayer.Play();
                    BGM.MediaPlayer.Pause();
                    BGM.MediaPlayer.Position = TimeSpan.Zero;
                    BGM.MediaPlayer.Play();
                    Skip.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            BGM.MediaPlayer.Pause();
            BGM.MediaPlayer.Dispose();
            GachaLoading.MediaPlayer.Dispose();
        }

        private void Mute_Click(object sender, RoutedEventArgs e)
        {
            BGM.MediaPlayer.Pause();
        }

        private void Skip_Click(object sender, RoutedEventArgs e)
        {
            GachaLoading.MediaPlayer.Position = TimeSpan.MaxValue;
        }
    }
}