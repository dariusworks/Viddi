﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Cimbalino.Toolkit.Extensions;
using GalaSoft.MvvmLight.Messaging;
using ScottIsAFool.Windows.Core.Extensions;
using ScottIsAFool.Windows.Core.ViewModel;
using Viddi.Core;
using Viddi.Services;
using Viddi.ViewModel.Item;
using VidMePortable.Model.Responses;

namespace Viddi.ViewModel
{
    public class VideoLoadingViewModel : LoadingItemsViewModel<IListType>
    {
        public virtual Task<VideosResponse> GetVideos(int offset)
        {
            return Task.FromResult(new VideosResponse());
        }

        protected override async Task LoadData(bool isRefresh, bool add = false, int offset = 0)
        {
            if (ItemsLoaded && !isRefresh && !add)
            {
                return;
            }

            HasErrors = false;

            try
            {
                if (!add)
                {
                    SetProgressBar("Getting videos...");
                }

                IsLoadingMore = add;
                var response = await GetVideos(offset);
                if (response != null && response.Videos != null)
                {
                    if (Items == null || !add)
                    {
                        Items = new ObservableCollection<IListType>();
                    }

                    var allVideos = response.Videos.Select(x => new VideoItemViewModel(x, this)).ToList();

                    IEnumerable<IListType> videoList;

                    if (IncludeReviewsInFeed() && !allVideos.IsNullOrEmpty())
                    {
                        videoList = allVideos.AddEveryOften(10, 2, ReviewService.Current.ReviewViewModel);
                    }
                    else
                    {
                        videoList = allVideos;
                    }

                    //allVideos.AddEveryOften(10, 2, new AdViewModel(), 5, true);

                    Items.AddRange(videoList);
                }

                CanLoadMore = response != null && response.Page != null && Items.Count + 1 < response.Page.Total;
                ItemsLoaded = true;
            }
            catch (Exception ex)
            {
                Log.DebugException(string.Format("{0}.LoadData()", GetType()), ex);
                HasErrors = true;
            }

            IsLoadingMore = false;
            SetProgressBar();
        }

        protected virtual bool IncludeReviewsInFeed()
        {
#if DEBUG
            return true;
#else
            return ReviewService.Current.CanShowReviews;
#endif
        }

        protected override void WireMessages()
        {
            Messenger.Default.Register<NotificationMessage>(this, m =>
            {
                if (m.Notification.Equals(Constants.Messages.HideReviewsMsg))
                {
                    if (Items == null)
                    {
                        return;
                    }

                    var reviews = Items.Where(x => x is ReviewViewModel).ToList();
                    if (reviews == null) return;
                    foreach (var item in reviews)
                    {
                        Items.Remove(item);
                    }
                }
            });

            base.WireMessages();
        }
    }
}
