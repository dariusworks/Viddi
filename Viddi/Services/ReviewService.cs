﻿using System;
using Windows.ApplicationModel.Store;
using Windows.System;
using Cimbalino.Toolkit.Services;
using Viddi.Core;
using Viddi.ViewModel;
using Viddi.ViewModel.Item;

namespace Viddi.Services
{
    public class ReviewService
    {
        private readonly IApplicationSettingsService _settingsService;
        public static ReviewService Current { get; private set; }

        private int _runCount;
        private bool _alreadyResponded;
        private readonly ReviewViewModel _reviewViewModel;

        public ReviewService(IApplicationSettingsService settingsService)
        {
            _reviewViewModel = new ReviewViewModel();
            _settingsService = settingsService;
            Current = this;

            GetRunCount();
            GetAlreadyResponded();
        }

        public void LaunchReview()
        {
            Launcher.LaunchUriAsync(new Uri("ms-windows-store:reviewapp?appid=" + CurrentApp.AppId));
        }

        public ReviewViewModel ReviewViewModel { get { return _reviewViewModel; } }

        public void Responded()
        {
            _alreadyResponded = true;
            _settingsService.Roaming.Set(Constants.StorageSettings.PhoneAlreadyRespondedSetting, _alreadyResponded);
        }

        public void IncreaseCount()
        {
            _runCount++;
            _settingsService.Local.Set(Constants.StorageSettings.LaunchedCountSetting, _runCount);
        }

        public bool CanShowReviews
        {
            get { return _runCount > 5 && _runCount % 5 == 0 && !_alreadyResponded; }
        }

        private void GetAlreadyResponded()
        {
#if DEBUG
            _alreadyResponded = false;
            return;
#endif
            var responded = _settingsService.Roaming.Get<bool>(Constants.StorageSettings.PhoneAlreadyRespondedSetting);
            _alreadyResponded = responded;
        }

        private void GetRunCount()
        {
            var count = _settingsService.Local.Get<int>(Constants.StorageSettings.LaunchedCountSetting);
            _runCount = count;
        }
    }
}
