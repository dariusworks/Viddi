﻿using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Navigation;
using Cimbalino.Toolkit.Services;
using GalaSoft.MvvmLight.Ioc;
using ScottIsAFool.WindowsPhone.Logging;
using ThemeManagerRt;
using Viddy.Common;

namespace Viddy.Views
{
    public class BasePage : ThemeEnabledPage
    {
        protected readonly ILog Logger;
        private readonly INavigationService _navigationService;

        protected virtual ApplicationViewBoundsMode Mode
        {
            get { return ApplicationViewBoundsMode.UseVisible; }
        }

        public BasePage()
        {
            NavigationCacheMode = NavigationCacheMode.Required;
            Logger = new WinLogger(GetType().FullName);
            _navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
        }

        protected virtual void OnBackKeyPressed(object sender, NavigationServiceBackKeyPressedEventArgs e)
        {
            
        }

        protected void SetFullScreen(ApplicationViewBoundsMode mode)
        {
            ApplicationView.GetForCurrentView().SetDesiredBoundsMode(mode);
        }
        
        protected virtual void InitialiseOnBack()
        {
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (_navigationService != null)
            {
                _navigationService.BackKeyPressed += OnBackKeyPressed;
            }

            Logger.Info("Navigated to {0}", GetType().FullName);

            SetFullScreen(Mode);

            if (e.NavigationMode == NavigationMode.Back)
            {
                InitialiseOnBack();
            }
            else
            {
                var parameters = e.Parameter as NavigationParameters;
                if (parameters != null && parameters.ClearBackstack)
                {
                    Logger.Info("Clearing backstack");
                    Frame.SetNavigationState("1,0");
                }
            }

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (_navigationService != null)
            {
                _navigationService.BackKeyPressed -= OnBackKeyPressed;                
            }

            //Logger.Info("Navigated from {0}", GetType().FullName);
            base.OnNavigatedFrom(e);
        }
    }
}
