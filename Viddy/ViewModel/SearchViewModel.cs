﻿using System.Threading.Tasks;
using Cimbalino.Toolkit.Services;
using GalaSoft.MvvmLight.Command;
using VidMePortable;
using VidMePortable.Model.Responses;

namespace Viddy.ViewModel
{
    public class SearchViewModel : VideoLoadingViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IVidMeClient _vidMeClient;

        public SearchViewModel(INavigationService navigationService, IVidMeClient vidMeClient)
        {
            _navigationService = navigationService;
            _vidMeClient = vidMeClient;
        }

        public string SearchText { get; set; }
        public bool IncludeNsfw { get; set; }

        public bool CanSearch
        {
            get { return !ProgressIsVisible && !string.IsNullOrEmpty(SearchText); }
        }

        public RelayCommand SearchCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    if (!CanSearch) return;

                    await LoadData(true);
                });
            }
        }

        public override Task<VideosResponse> GetVideos(int offset)
        {
            return _vidMeClient.SearchVideosAsync(SearchText, offset, includeNsfw: IncludeNsfw);
        }

        public override void UpdateProperties()
        {
            RaisePropertyChanged(() => CanSearch);
        }
    }
}
