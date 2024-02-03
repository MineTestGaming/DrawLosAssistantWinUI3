using DrawLosAssistantWinUI3.Models;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DrawLosAssistantWinUI3.ResultPage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SuperRare : Page
    {
        private DispatcherQueue dispatcherQueue;

        public SuperRare()
        {
            this.InitializeComponent();
            dispatcherQueue = DispatcherQueue.GetForCurrentThread();
            GachaLoading.MediaPlayer.MediaEnded += GachaVideoPlayer_MediaEnded;
            GachaLoading.MediaPlayer.AudioCategory = Windows.Media.Playback.MediaPlayerAudioCategory.Other;

            // Customization
            var localSettings = ApplicationData.Current.LocalSettings;

            string AudioType = localSettings.Values["AudioType"].ToString();
            switch (AudioType)
            {
                case "External":
                    if (localSettings.Values.ContainsKey("AudioPath"))
                    {
                        BGM.MediaPlayer.Source = MediaSource.CreateFromUri(new Uri(localSettings.Values["AudioPath"].ToString()));
                    }
                    GachaLoading.MediaPlayer.Volume = 0;
                    BGM.MediaPlayer.Play();
                    Mute.Visibility = Visibility.Visible;
                    break;

                case "Internal":
                    GachaLoading.MediaPlayer.Volume = 100;
                    BGM.MediaPlayer.AutoPlay = false;
                    BGM.MediaPlayer.Pause();
                    Mute.Visibility = Visibility.Collapsed;
                    break;
            }

            if (localSettings.Values.ContainsKey("SuperRareVideoPath"))
            {
                GachaLoading.MediaPlayer.Source = MediaSource.CreateFromUri(new Uri(localSettings.Values["SuperRareVideoPath"].ToString()));
            }
        }

        private async void GachaVideoPlayer_MediaEnded(Windows.Media.Playback.MediaPlayer sender, object args)
        {
            await Task.Run(() =>
            {
                dispatcherQueue.TryEnqueue(() =>
                {
                    Skip.Visibility = Visibility.Collapsed;
                    ResultDisplay.Visibility = Visibility.Visible;
                    Result.Text = RandomLogic.SuperRareRandom();
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
                    ResultDisplay.Visibility = Visibility.Collapsed;
                    GachaLoading.MediaPlayer.Play();
                    BGM.MediaPlayer.Pause();
                    BGM.MediaPlayer.Position = TimeSpan.Zero;
                    BGM.MediaPlayer.Play();
                    Skip.Visibility = Visibility.Visible;
                    break;

                case "Rare":
                    this.Frame.Navigate(typeof(Rare));
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