﻿namespace Viddy.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VideoPlayerView
    {
        public VideoPlayerView()
        {
            InitializeComponent();
        }

        private void MediaPlayer_OnMediaFailed(object sender, Windows.UI.Xaml.ExceptionRoutedEventArgs e)
        {

        }
    }
}