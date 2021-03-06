﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Cimbalino.Toolkit.Services;
using GalaSoft.MvvmLight.Command;
using ScottIsAFool.Windows.Core.Extensions;
using Viddi.Core.Extensions;
using Viddi.Foursquare;
using Viddi.Localisation;
using Viddi.Model;
using Viddi.Views;

namespace Viddi.ViewModel
{
    public class FoursqureViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly INavigationService _navigationService;
        private readonly FoursquareClient _foursquareClient;

        private Geopoint _curentLocation;

        public FoursqureViewModel(ISettingsService settingsService, INavigationService navigationService)
        {
            _settingsService = settingsService;
            _navigationService = navigationService;
            _foursquareClient = new FoursquareClient();
        }

        public async Task GetLocations()
        {
            if (!_settingsService.LocationIsOn)
            {
                LocationText = Resources.LocationTurnLocationOn;
                return;
            }

            LocationText = Resources.LocationFindingYou;

            var position = await GetCurrentLocation();
            if (position == null)
            {
                LocationText = Resources.LocationFailedToFindYou;
                return;
            }

            _curentLocation = position.Point;

            var venues = await _foursquareClient.GetVenuesAsync(_curentLocation.Position.Longitude, _curentLocation.Position.Latitude);
            if (venues.IsNullOrEmpty())
            {
                LocationText = Resources.LocationNothingNearby;
                return;
            }

            Locations = venues;
            SelectedVenue = Locations.FirstOrDefault();
            LocationText = SelectedVenue != null ? SelectedVenue.Name : Resources.LocationAddLocation;
            ShowVenues = true;
        }

        public double? Longitude
        {
            get
            {
                if (_curentLocation != null)
                {
                    return _curentLocation.Position.Longitude;
                }

                return null;
            }
        }

        public double? Latitude
        {
            get
            {
                if (_curentLocation != null)
                {
                    return _curentLocation.Position.Latitude;
                }

                return null;
            }
        }

        public string VenueId
        {
            get { return SelectedVenue != null ? SelectedVenue.Id : null; }
        }

        public string VenueName
        {
            get { return SelectedVenue != null ? SelectedVenue.Name : null; }
        }

        private async Task<Geocoordinate> GetCurrentLocation()
        {
            var locator = new Geolocator
            {
                DesiredAccuracyInMeters = 30,
            };

            try
            {
                var position = await locator.GetGeopositionAsync(TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(10));

                if (position != null)
                {
                    return position.Coordinate;
                }
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        public List<Venue> Locations { get; set; }
        public Venue SelectedVenue { get; set; }

        public bool ShowVenues { get; set; }

        public bool HasVenue
        {
            get { return SelectedVenue != null; }
        }

        public string LocationText { get; set; }

        public RelayCommand<Venue> VenueTappedCommand
        {
            get
            {
                return new RelayCommand<Venue>(venue =>
                {
                    LocationText = venue.Name;
                    SelectedVenue = venue;
                    ShowVenues = false;
                });
            }
        }

        public RelayCommand ClearLocationCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SelectedVenue = null;
                    LocationText = Resources.LocationAddLocation;
                    ShowVenues = false;
                }, () => _settingsService.LocationIsOn);
            }
        }

        public RelayCommand VenueTextTappedCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (!_settingsService.LocationIsOn)
                    {
                        _navigationService.Navigate<SettingsView>();
                        return;
                    }

                    if (Locations.IsNullOrEmpty())
                    {
                        GetLocations();
                        return;
                    }

                    ShowVenues = !ShowVenues;
                });
            }
        }
    }
}
