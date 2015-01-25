﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Cimbalino.Toolkit.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using ThemeManagerRt;
using Viddy.Extensions;
using Viddy.Messaging;
using Viddy.Services;
using Viddy.Views;
using Viddy.Views.Account;
using Viddy.Views.Account.Manage;
using VidMePortable;
using VidMePortable.Model;
using VidMePortable.Model.Responses;

namespace Viddy.ViewModel.Account
{
    public class AccountViewModel : VideoLoadingViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IVidMeClient _vidMeClient;

        public AccountViewModel(INavigationService navigationService, IVidMeClient vidMeClient, AvatarViewModel avatar)
        {
            Avatar = avatar;
            _navigationService = navigationService;
            _vidMeClient = vidMeClient;

            if (IsInDesignMode)
            {
                IsEmpty = true;
            }
            else
            {
                AuthenticationService.Current.UserSignedIn += CurrentOnUserSignedIn;
            }
        }

        private void CurrentOnUserSignedIn(object sender, EventArgs eventArgs)
        {
            Reset();
        }

        public AvatarViewModel Avatar { get; set; }


        public RelayCommand LogInLogOutCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    if (AuthenticationService.Current.IsLoggedIn)
                    {
                        AuthenticationService.Current.SignOut();
                        await LoadData(true);
                    }
                    else
                    {
                        LaunchAuthentication();
                    }
                });
            }
        }

        public RelayCommand ChangeAvatarCommand
        {
            get
            {
                return new RelayCommand(() => Avatar.ChangeAvatar());
            }
        }

        public RelayCommand<VideoItemViewModel> DeleteCommand
        {
            get
            {
                return new RelayCommand<VideoItemViewModel>(async video =>
                {
                    if (video == null || video.Video == null || !video.CanDelete)
                    {
                        return;
                    }

                    try
                    {
                        if (AuthenticationService.Current.IsLoggedIn)
                        {
                            if (await _vidMeClient.DeleteVideoAsync(video.Video.VideoId))
                            {
                                Videos.Remove(video);
                            }
                        }
                        else
                        {
                            if (await _vidMeClient.DeleteAnonymousVideoAsync(video.Video.VideoId, ""))
                            {
                                Videos.Remove(video);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        
                    }
                });
            }
        }

        public RelayCommand NavigateToSettingsCommand
        {
            get
            {
                return new RelayCommand(() => _navigationService.Navigate<SettingsView>());
            }
        }

        public RelayCommand NavigateToEditProfileCommand
        {
            get { return new RelayCommand(() => _navigationService.Navigate<EditProfileView>()); }
        }

        public RelayCommand NavigateToManageAccountCommand
        {
            get
            {
                return new RelayCommand(() => _navigationService.Navigate<ManageAccountView>());
            }
        }

        public RelayCommand NavigateToRecordCommand
        {
            get
            {
                return new RelayCommand(() => _navigationService.Navigate<VideoRecordView>());
            }
        }

        public RelayCommand NavigateToManualLoginCommand
        {
            get
            {
                return new RelayCommand(() => _navigationService.Navigate<ManualLoginView>());
            }
        }

        public RelayCommand TempCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Messenger.Default.Send(new UserMessage(new User
                    {
                        UserId = "59739",
                        Username = "PunkHack",
                        AvatarUrl = "https://d1wst0behutosd.cloudfront.net/avatars/59739.gif?gv2r1420954820",
                        CoverUrl = "https://d1wst0behutosd.cloudfront.net/channel_covers/59739.jpg?v1r1420500373",
                        FollowerCount = 1200,
                        LikesCount = "92",
                        VideoCount = 532,
                        VideoViews = "71556",
                        VideosScores = 220,
                        Bio = "Some bio information"
                    }));

                    _navigationService.Navigate<ProfileView>();
                });
            }
        }

        private void LaunchAuthentication()
        {
            var url = _vidMeClient.GetAuthUrl(Constants.ClientId, Constants.CallBackUrl, new List<Scope> {Scope.Videos, Scope.VideoUpload, Scope.Channels, Scope.Comments, Scope.Votes, Scope.Account, Scope.AuthManagement, Scope.ClientManagement});

            WebAuthenticationBroker.AuthenticateAndContinue(new Uri(url), new Uri(Constants.CallBackUrl), new ValueSet(), WebAuthenticationOptions.None);
        }

        private async Task CompleteAuthentication(string code)
        {
            try
            {
                var auth = await _vidMeClient.ExchangeCodeForTokenAsync(code, Constants.ClientId, Constants.ClientSecret);
                if (auth != null)
                {
                    AuthenticationService.Current.SetAuthenticationInfo(auth);
                }

                await LoadData(true);
            }
            catch (Exception ex)
            {
                
            }
        }

        public override Task<VideosResponse> GetVideos(int offset)
        {
            return AuthenticationService.Current.IsLoggedIn 
                ? _vidMeClient.GetUserVideosAsync(AuthenticationService.Current.LoggedInUserId) 
                : _vidMeClient.GetAnonymousVideosAsync();
        }

        protected override void WireMessages()
        {
            Messenger.Default.Register<NotificationMessage>(this, async m =>
            {
                if (m.Notification.Equals(Constants.Messages.AuthCodeMsg))
                {
                    var code = (string) m.Sender;
                    await CompleteAuthentication(code);
                }
            });
        }
    }
}