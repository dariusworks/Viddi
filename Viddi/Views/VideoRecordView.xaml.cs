﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Cimbalino.Toolkit.Services;
using GalaSoft.MvvmLight.Ioc;
using ScottIsAFool.Windows.Core.Services;
using Viddi.Services;
using Viddi.ViewModel;

namespace Viddi.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VideoRecordView
    {
        private IDisplayRequestService _displayRequest;
        private bool _isRecording;
        private readonly DisplayInformation _display;
        private readonly DispatcherTimer _recordingTimer;
        private readonly ICameraInfoService _cameraInfoService;
        private TimeSpan _recordedDuration;

        protected override ApplicationViewBoundsMode Mode
        {
            get { return ApplicationViewBoundsMode.UseVisible; }
        }

        public static readonly DependencyProperty FlashOnProperty = DependencyProperty.Register(
            "FlashOn", typeof(bool), typeof(VideoRecordView), new PropertyMetadata(default(bool)));

        public bool FlashOn
        {
            get { return (bool)GetValue(FlashOnProperty); }
            set { SetValue(FlashOnProperty, value); }
        }

        public static readonly DependencyProperty IsFrontFacingProperty = DependencyProperty.Register(
            "IsFrontFacing", typeof(bool), typeof(VideoRecordView), new PropertyMetadata(default(bool)));

        public bool IsFrontFacing
        {
            get { return (bool)GetValue(IsFrontFacingProperty); }
            set { SetValue(IsFrontFacingProperty, value); }
        }

        public VideoRecordView()
        {
            InitializeComponent();

            _display = DisplayInformation.GetForCurrentView();
            _display.OrientationChanged += DisplayOnOrientationChanged;

            FlashViewbox.DataContext = this;
            FrontFacingViewbox.DataContext = this;

            _recordingTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _recordingTimer.Tick += RecordingTimerOnTick;

            _cameraInfoService = SimpleIoc.Default.GetInstance<ICameraInfoService>();
            _displayRequest = SimpleIoc.Default.GetInstance<IDisplayRequestService>();

            Window.Current.Activated += CurrentOnActivated;
        }

        private void CurrentOnActivated(object sender, WindowActivatedEventArgs e)
        {
            var i = 1;
        }

        private void RecordingTimerOnTick(object sender, object o)
        {
            if (_recordedDuration == TimeSpan.MinValue)
            {
                _recordedDuration = TimeSpan.FromSeconds(0);
            }

            _recordedDuration = _recordedDuration.Add(TimeSpan.FromSeconds(1));
            var timeString = string.Format("{0:00}:{1:00}", _recordedDuration.Minutes, _recordedDuration.Seconds);
            RecordedLengthText.Text = timeString;
        }

        private void DisplayOnOrientationChanged(DisplayInformation sender, object args)
        {
            SetRotation(_display.CurrentOrientation);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            _fromPageNavigation = true;

            base.OnNavigatedTo(e);

            await StartPreview();

            Window.Current.VisibilityChanged += CurrentOnVisibilityChanged;
        }

        private bool _fromPageNavigation;

        public static async void CurrentOnVisibilityChanged(object sender, VisibilityChangedEventArgs e)
        {
            var window = sender as Window;
            if (window != null)
            {
                var frame = window.Content as Frame;
                if (frame != null)
                {
                    var page = frame.Content as VideoRecordView;
                    if (page != null)
                    {
                        await page.VisibilityChanged(e);
                    }
                }
            }
        }

        private async Task VisibilityChanged(VisibilityChangedEventArgs e)
        {
            if (e.Visible)
            {
                if (!_fromPageNavigation)
                {
                    await StartPreview();
                }
            }
            else
            {
                _fromPageNavigation = false;
                StopVideo();
            }
        }

        private async Task StartPreview()
        {
            try
            {
                if (!_cameraInfoService.IsInitialised)
                {
                    var cameraType = IsFrontFacing ? CameraType.FrontFacing : CameraType.Primary;
                    await _cameraInfoService.StartService(cameraType);
                }
            }
            catch
            {
            }

            if (!_cameraInfoService.IsInitialised)
            {
                // TODO: Display error
                return;
            }

            await _cameraInfoService.StartPreview(PrePreviewTask());
        }

        private async Task PrePreviewTask()
        {
            var mediaCapture = _cameraInfoService.MediaCapture;

            var previewResolutions = mediaCapture.VideoDeviceController.GetAvailableMediaStreamProperties(MediaStreamType.VideoPreview).Cast<VideoEncodingProperties>().ToList();
            var recordingResolutions = mediaCapture.VideoDeviceController.GetAvailableMediaStreamProperties(MediaStreamType.VideoRecord).Cast<VideoEncodingProperties>().ToList();

            var highestPreviewRes = previewResolutions.FirstOrDefault(y => y.Height <= 720);
            var highestRecordingRes = recordingResolutions.FirstOrDefault(y => y.Height <= 720);

            await mediaCapture.VideoDeviceController.SetMediaStreamPropertiesAsync(MediaStreamType.VideoPreview, highestPreviewRes);
            await mediaCapture.VideoDeviceController.SetMediaStreamPropertiesAsync(MediaStreamType.VideoRecord, highestRecordingRes);

            CaptureElement.Source = mediaCapture;
            SetRotation(_display.CurrentOrientation);
        }

        private async void SetRotation(DisplayOrientations orientation)
        {
            var mediaCapture = _cameraInfoService.MediaCapture;
            var rotation = VideoRotation.Clockwise90Degrees;
            if (orientation != _display.NativeOrientation || IsFrontFacing)
            {
                switch (orientation)
                {
                    case DisplayOrientations.Landscape:
                        rotation = IsFrontFacing ? VideoRotation.None : VideoRotation.Clockwise270Degrees;
                        break;
                    case DisplayOrientations.LandscapeFlipped:
                        rotation = VideoRotation.Clockwise180Degrees;
                        break;
                    default:
                        rotation = IsFrontFacing ? VideoRotation.Clockwise270Degrees : VideoRotation.Clockwise180Degrees;
                        break;
                }
            }

            if (_cameraInfoService.IsInitialised && !string.IsNullOrEmpty(mediaCapture.MediaCaptureSettings.VideoDeviceId) && !string.IsNullOrEmpty(mediaCapture.MediaCaptureSettings.AudioDeviceId))
            {
                //rotate the video feed according to the sensor
                mediaCapture.SetPreviewRotation(rotation);
                //mediaCapture.SetRecordRotation(rotation);

                //hook into MediaCapture events
                mediaCapture.RecordLimitationExceeded += MediaCaptureOnRecordLimitationExceeded;
                mediaCapture.Failed += MediaCaptureOnFailed;

                //device initialized successfully
            }
            else
            {
                //no cam found
            }
        }

        private void MediaCaptureOnFailed(MediaCapture sender, MediaCaptureFailedEventArgs errorEventArgs)
        {
            var i = 1;
        }

        private void MediaCaptureOnRecordLimitationExceeded(MediaCapture sender)
        {
            var i = 1;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            Window.Current.VisibilityChanged -= CurrentOnVisibilityChanged;
            StopVideo();
        }

        private void StopVideo()
        {
            if (_displayRequest != null && _isRecording)
            {
                _displayRequest.Release();
                _displayRequest = null;
            }

            if (!_cameraInfoService.IsInitialised)
            {
                return;
            }

            try
            {
                if (_isRecording)
                {
                    _cameraInfoService.MediaCapture.StopRecordAsync();
                }

                CaptureElement.Source = null;

                _cameraInfoService.DisposeMediaCapture();
            }
            catch
            {
            }
        }

        private void FlashButton_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            FlashOn = !FlashOn;
            TurnFlashOnOrOff(FlashOn);
        }

        private async void FrontFacingButton_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            IsFrontFacing = !IsFrontFacing;
            await _cameraInfoService.DisposeMediaCapture();
            await StartPreview();
        }

        private void TurnFlashOnOrOff(bool turnFlashOn)
        {
            if (!_cameraInfoService.IsInitialised)
            {
                return;
            }

            var mediaCapture = _cameraInfoService.MediaCapture;

            var torch = mediaCapture.VideoDeviceController.TorchControl;
            torch.Enabled = turnFlashOn;
        }

        private int ConvertVideoRotationToMFRotation(VideoRotation rotation)
        {
            int MFVideoRotation = 0; // MFVideoRotationFormat::MFVideoRotationFormat_0 in Mfapi.h
            switch (rotation)
            {
                case VideoRotation.Clockwise90Degrees:
                    MFVideoRotation = 90; // MFVideoRotationFormat::MFVideoRotationFormat_90;
                    break;
                case VideoRotation.Clockwise180Degrees:
                    MFVideoRotation = 180; // MFVideoRotationFormat::MFVideoRotationFormat_180;
                    break;
                case VideoRotation.Clockwise270Degrees:
                    MFVideoRotation = 270; // MFVideoRotationFormat::MFVideoRotationFormat_270;
                    break;
            }

            return MFVideoRotation;
        }

        private async void PinButton_OnClick(object sender, RoutedEventArgs e)
        {
            await SaveTileImage(RecordTile);
        }

        private string _fileName;
        private async void RecordButtonChanged(object sender, RoutedEventArgs e)
        {
            var mediaCapture = _cameraInfoService.MediaCapture;
            if (!_isRecording)
            {
                RecordedLengthText.Text = "00:00";
                RecordedLengthText.Visibility = Visibility.Visible;

                _isRecording = true;

                _displayRequest.Request();

                _fileName = string.Format("Viddi-{0}.mp4", DateTime.Now.ToString("yyyy-M-dd-HH-mm-ss"));
                var folder = ApplicationData.Current.LocalCacheFolder;
                var file = await folder.CreateFileAsync(_fileName, CreationCollisionOption.ReplaceExisting);

                var profile = MediaEncodingProfile.CreateMp4(VideoEncodingQuality.Auto);

                var mfVideoRotationGuid = new Guid("C380465D-2271-428C-9B83-ECEA3B4A85C1"); // MF_MT_VIDEO_ROTATION in Mfapi.h
                var rotation = _cameraInfoService.MediaCapture.GetPreviewRotation();
                int mfVideoRotation = ConvertVideoRotationToMFRotation(rotation);

                profile.Video.Properties.Add(mfVideoRotationGuid, PropertyValue.CreateInt32(mfVideoRotation));

                mediaCapture.StartRecordToStorageFileAsync(profile, file);
                _recordingTimer.Start();
            }
            else
            {
                _recordingTimer.Stop();
                _isRecording = false;
                _displayRequest.Release();
                _recordedDuration = TimeSpan.MinValue;

                await mediaCapture.StopRecordAsync();

                if (string.IsNullOrEmpty(_fileName))
                {
                    return;
                }

                var folder = ApplicationData.Current.LocalCacheFolder;
                try
                {
                    var file = await folder.GetFileAsync(_fileName);
                    if (file != null)
                    {
                        var cameraRoll = KnownFolders.CameraRoll;
                        await file.MoveAsync(cameraRoll);

                        var movedFile = await cameraRoll.GetFileAsync(_fileName);

                        var vm = DataContext as VideoRecordViewModel;
                        if (vm != null)
                        {
                            vm.FinishedRecording(movedFile);
                            RecordedLengthText.Visibility = Visibility.Collapsed;
                        }
                    }
                }
                catch (FileNotFoundException)
                {

                }
            }
        }
    }
}