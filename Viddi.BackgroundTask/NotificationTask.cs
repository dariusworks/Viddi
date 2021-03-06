﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.UI.Notifications;
using Cimbalino.Toolkit.Services;
using NotificationsExtensions.BadgeContent;
using NotificationsExtensions.ToastContent;
using ScottIsAFool.Windows.Core.Extensions;
using Viddi.Core;
using Viddi.Core.Extensions;
using Viddi.Core.Services;
using Viddi.Localisation;
using VidMePortable;
using VidMePortable.Model;

namespace Viddi.BackgroundTask
{
    public sealed class NotificationTask : IBackgroundTask
    {
        private IVidMeClient _vidMeClient;
        private readonly IApplicationSettingsService _settingsService = new ApplicationSettingsService();

        private List<Notification> _notifications;
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();

            await CheckForNotifications(true);

            deferral.Complete();
        }

        private void SetAuthentication()
        {
            if (AuthenticationService.Current == null)
            {
                _vidMeClient = new VidMeClient(Utils.UniqueDeviceIdentifier, "WindowsPhone");
                new AuthenticationService(_settingsService, _vidMeClient);
            }

            if (AuthenticationService.Current != null)
            {
                if (!AuthenticationService.Current.ServiceStarted)
                {
                    AuthenticationService.Current.StartService();
                }

                if (_vidMeClient == null || _vidMeClient.AuthenticationInfo == null)
                {
                    _vidMeClient = AuthenticationService.Current.GetAuthenticatedVidMeClient();
                }
            }
        }

        public IAsyncOperation<int> CheckForNotifications(bool showToasts)
        {
            SetAuthentication();
            var task = CheckForNotificationsInternal(showToasts: showToasts);
            var returnTask = task.AsAsyncOperation();
            return returnTask;
        }

        private async Task<int> CheckForNotificationsInternal(bool showToasts = false)
        {
            var unreadCount = 0;
            if (!AuthenticationService.Current.IsLoggedIn)
            {
                UpdateTileCount(0, false);
                return unreadCount;
            }

            try
            {
                var response = await _vidMeClient.GetUnreadNotificationCountAsync();
                unreadCount = response;

                if (showToasts && unreadCount > 0)
                {
                    var notifications = await _vidMeClient.GetNotificationsAsync(unreadCount);
                    if (notifications != null && !notifications.Notifications.IsNullOrEmpty())
                    {
                        if (_notifications == null)
                        {
                            _notifications = notifications.Notifications;
                        }
                    }
                }

                UpdateTileCount(unreadCount, unreadCount > 0 && showToasts);
            }
            catch (Exception ex)
            {

            }

            return unreadCount;
        }

        private List<string> _previousToastIds;
        public void UpdateTileCount(int count, bool showToast)
        {
            if (showToast)
            {
                LoadPreviousToasts();
                ShowToasts();
                SavePreviousToasts();
            }

            if (count == 0)
            {
                ClearPreviousToasts();
                ToastNotificationManager.History.Clear();
            }

            // Update tile
            var badgeContent = new BadgeNumericNotificationContent((uint)count);
            BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(badgeContent.CreateNotification());
        }

        private void ShowToasts()
        {
            var toastManager = ToastNotificationManager.CreateToastNotifier();
            // Show toast
            foreach (var notification in _notifications.Where(x => !x.Read))
            {
                if (_previousToastIds.Contains(notification.NotificationId))
                {
                    continue;
                }

                var toastNotification = ToastContentFactory.CreateToastText02();
                toastNotification.Launch = HandleNotificationType(notification, toastNotification);
                toastNotification.TextBodyWrap.Text = notification.Text;

                var toast = toastNotification.CreateNotification();
                toast.Tag = notification.NotificationId;
                toastManager.Show(toast);

                _previousToastIds.Add(notification.NotificationId);
            }
        }

        private string HandleNotificationType(Notification notification, IToastText02 toastNotification)
        {
            if (notification == null)
            {
                return "Viddi://";
            }

            var type = notification.NotificationType;
            var notificationParameter = "&notificationId=" + notification.NotificationId;

            switch (type)
            {
                case NotificationType.ChannelSubscribed:
                    toastNotification.TextHeading.Text = Resources.NotificationChannelSubscription;

                    var channel = notification.Channel;
                    if (channel != null)
                    {
                        return channel.ToViddiLink() + notificationParameter;
                    }
                    break;
                case NotificationType.UserSubscribed:
                    toastNotification.TextHeading.Text = Resources.NotificationUserSubscription;
                    var user = notification.User;
                    if (user != null)
                    {
                        return user.ToViddiLink() + notificationParameter;
                    }
                    break;
                case NotificationType.CommentReply:
                case NotificationType.VideoComment:
                case NotificationType.VideoUpVoted:
                    if (type == NotificationType.CommentReply) toastNotification.TextHeading.Text = Resources.NotificationCommentReply;
                    else if (type == NotificationType.VideoComment) toastNotification.TextHeading.Text = Resources.NotificationVideoComment;
                    else if (type == NotificationType.VideoUpVoted) toastNotification.TextHeading.Text = Resources.NotificationVideoUpVote;

                    var video = notification.Video;
                    if (video != null)
                    {
                        return video.ToViddiLink() + notificationParameter;
                    }
                    break;
            }

            return "Viddi://";
        }

        private void LoadPreviousToasts()
        {
            var list = _settingsService.Local.GetS<List<string>>(Constants.StorageSettings.PreviousToasts);
            _previousToastIds = list ?? new List<string>();
        }

        private void SavePreviousToasts()
        {
            if (!_previousToastIds.IsNullOrEmpty())
            {
                _settingsService.Local.SetS(Constants.StorageSettings.PreviousToasts, _previousToastIds);
            }
        }

        private void ClearPreviousToasts()
        {
            _settingsService.Local.Remove(Constants.StorageSettings.PreviousToasts);
        }
    }
}
