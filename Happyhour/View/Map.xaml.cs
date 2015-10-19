using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// BING MAPS
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Documents;
using Windows.UI;
using Happyhour.Control;
using Happyhour.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Windows.UI.Core;
using Windows.Devices.Geolocation.Geofencing;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Happyhour.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Map : Page
    {
        ObservableCollection<PubRoute> routeList;
        Geocoordinate currentLocation;
        Geolocator geolocator;
        Geoposition pos;

        PubRoute selectedRoute;
        LocationData geofencePub;
        int visitedPubs = 0;
        int counter = 0;
        public Map()
        {
            this.InitializeComponent();
            GeofenceMonitor.Current.Geofences.Clear();
            geolocator = new Geolocator();
            geolocator.MovementThreshold = 10;
            routeList = new ObservableCollection<PubRoute>(LocationHandler.Instance.routeList);
            getCurrentLocation();
            GeofenceMonitor.Current.GeofenceStateChanged += OnGeofenceStateChanged;
        }

        async private void PositionChanged(Geolocator sender, PositionChangedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                currentLocation = e.Position.Coordinate;
                bool isInList = false;
                //Summary.Text = "Updating," + counter;

                counter++;

                for (int i = 0; i < InputMap.MapElements.Count; i++)
                {
                    MapIcon icon = (MapIcon)InputMap.MapElements[i];
                    if(icon.Title.Equals("U bent hier"))
                    {
                        isInList = true;
                        icon.Location = currentLocation.Point;
                    }
                }

                if(!isInList)
                    AddMapIcon(currentLocation, "U bent hier");

                if(selectedRoute != null)
                {
                    //getRouteWithCurrentLocation(currentLocation.Point, selectedRoute.pubs[0]);
                }
            });
        }

        /*
        /   Voor het toevoegen van icon's aan de map.
        */
        private void AddMapIcon(LocationData location)
        {
            MapIcon MapIcon1 = new MapIcon();
            MapIcon1.Location = new Geopoint(location.position);
            MapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
            MapIcon1.Title = location.name;
            InputMap.MapElements.Add(MapIcon1);

            // Geofence
            BasicGeoposition pos = new BasicGeoposition();
            pos.Latitude = location.position.Latitude;
            pos.Longitude = location.position.Longitude;
            Geocircle circle = new Geocircle(pos, 20);
            MonitoredGeofenceStates monitoredStates =
                MonitoredGeofenceStates.Entered |
                MonitoredGeofenceStates.Exited |
                MonitoredGeofenceStates.Removed;
            TimeSpan dwellTime = TimeSpan.FromSeconds(1);
            var geofence = new Windows.Devices.Geolocation.Geofencing.Geofence(location.name, circle, monitoredStates, false, dwellTime);
            GeofenceMonitor.Current.Geofences.Add(geofence);
        }

        private void AddMapIcon(Geocoordinate coordinate, string name)
        {
            MapIcon MapIcon1 = new MapIcon();
            MapIcon1.Location = coordinate.Point;
            MapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
            MapIcon1.Title = name;
            InputMap.MapElements.Add(MapIcon1);
        }

        private void AddMapIcon()
        {
            MapIcon MapIcon1 = new MapIcon();
            MapIcon1.Location = new Geopoint(new BasicGeoposition()
            {
                Latitude = 47.620,
                Longitude = -122.349
            });
            MapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
            MapIcon1.Title = "Space Needle";
            InputMap.MapElements.Add(MapIcon1);
        }

        /*
        /   Voor het bepalen van de route en richtingen.
        */
        private async void GetRouteAndDirections()
        {
            // Start at Microsoft in Redmond, Washington.
            BasicGeoposition startLocation = new BasicGeoposition();
            startLocation.Latitude = 47.643;
            startLocation.Longitude = -122.131;
            Geopoint startPoint = new Geopoint(startLocation);

            // End at the city of Seattle, Washington.
            BasicGeoposition endLocation = new BasicGeoposition();
            endLocation.Latitude = 47.604;
            endLocation.Longitude = -122.329;
            Geopoint endPoint = new Geopoint(endLocation);

            // Get the route between the points.
            MapRouteFinderResult routeResult =
                await MapRouteFinder.GetWalkingRouteAsync(
                startPoint,
                endPoint);

            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                Summary.Inlines.Add(new Run()
                {
                    Text = "Totale geschatte tijd in minuten: " + routeResult.Route.EstimatedDuration.TotalMinutes.ToString()
                });
                Summary.Inlines.Add(new LineBreak());
                Summary.Inlines.Add(new Run()
                {
                    Text = "Totale lengte in kilometers: "
                        + (routeResult.Route.LengthInMeters / 1000).ToString()
                });
            } else {
                Summary.Text = "Er is een probleem opgetreden: " + routeResult.Status.ToString();
            }

            // Tekent de route op de map.
            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.Orange;
                viewOfRoute.OutlineColor = Colors.Black;
                
                InputMap.Routes.Add(viewOfRoute);

                await InputMap.TrySetViewBoundsAsync(routeResult.Route.BoundingBox, null, Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
            }
        }

        private async void getRouteWithCurrentLocation(Geopoint startLoc, LocationData endLoc)
        {
            BasicGeoposition endLocation = endLoc.position;
            Geopoint endPoint = new Geopoint(endLocation);

            GetRouteAndDirections(startLoc, endPoint, true, Colors.Red);
        }

        private async void getRouteWithPubs(LocationData startLoc, LocationData endLoc)
        {
            BasicGeoposition startLocation = startLoc.position;
            Geopoint startPoint = new Geopoint(startLocation);

            BasicGeoposition endLocation = endLoc.position;
            Geopoint endPoint = new Geopoint(endLocation);

            GetRouteAndDirections(startPoint, endPoint, false, Colors.Orange);
        }

        private async void GetRouteAndDirections(Geopoint startPoint, Geopoint endPoint, bool startIsGPS, Color color)
        {
            // Get the route between the points.
            MapRouteFinderResult routeResult =
                await MapRouteFinder.GetDrivingRouteAsync(
                startPoint,
                endPoint,
                MapRouteOptimization.Time,
                MapRouteRestrictions.None);

            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                if (startIsGPS)
                {
                    Summary.Text = "";
                    Summary.Inlines.Add(new Run()
                    {
                        Text = "Totale geschatte tijd in minuten: " + routeResult.Route.EstimatedDuration.TotalMinutes.ToString()
                    });
                    Summary.Inlines.Add(new LineBreak());
                    Summary.Inlines.Add(new Run()
                    {
                        Text = "Totale lengte in kilometers: "
                            + (routeResult.Route.LengthInMeters / 1000).ToString()
                    });
                }
            }
            else
            {
                Summary.Text = "Er is een probleem opgetreden: " + routeResult.Status.ToString();
            }

            // Tekent de route op de map.
            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = color;
                viewOfRoute.OutlineColor = Colors.Black;

                InputMap.Routes.Add(viewOfRoute);

                await InputMap.TrySetViewBoundsAsync(routeResult.Route.BoundingBox, null, Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
            }
        }

        private async void getCurrentLocation()
        {

            var accessStatus = await Geolocator.RequestAccessAsync();

            switch(accessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                    Summary.Text = "Locatie bepalen, check internet verbinding";
                    pos = await geolocator.GetGeopositionAsync();
                    currentLocation = pos.Coordinate;
                    AddMapIcon(currentLocation, "U bent hier");
                    Summary.Text = "Locatie bekend, kies een route";
                    geolocator.PositionChanged += PositionChanged;
                    break;
                case GeolocationAccessStatus.Denied:
                    //Summary.Text = "Denied";
                    //LocationDisabledMessage.Visibility = Visibility.Visible;
                    Summary.Text = "";
                    Hyperlink link = new Hyperlink();
                    link.Inlines.Add(new Run()
                    {
                        Text = "settings voor Happyhour",
                        Foreground = new SolidColorBrush(Colors.White)
                    });
                    link.NavigateUri = new Uri("ms-settings:privacy-location");

                    Summary.Inlines.Add(new Run()
                    {
                        Text = "Huidige locatie niet bekend"
                    });
                    Summary.Inlines.Add(new LineBreak());
                    Summary.Inlines.Add(new Run()
                    {
                        Text = "Check de locatie "
                    });
                    Summary.Inlines.Add(link);
                    break;
                case GeolocationAccessStatus.Unspecified:
                    //Summary.Text = "Er is een probleem opgetreden";
                    break;
            }
        }

        private void DrawGeofences()
        {
            Windows.UI.Xaml.Shapes.Ellipse fence = new Windows.UI.Xaml.Shapes.Ellipse();

            //MapControl.Children.Add(fence);
            //MapControl.SetLocation(fence, point);
            //MapControl.SetNormalizedAnchorPoint(fence, new Point(0.5, 0.5));
        }

        /*
    private void drawGeofence(Geocoordinate coor)
    {
        var circle = new MapPolygon
        {
            FillColor = Colors.Red

        };
    }
    */

        public async void OnGeofenceStateChanged(GeofenceMonitor sender, object e)
        {
            var reports = sender.ReadReports();

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                foreach (GeofenceStateChangeReport report in reports)
                {
                    GeofenceState state = report.NewState;

                    Geofence geofence = report.Geofence;

                    if (state == GeofenceState.Removed)
                    {
                        // remove the geofence from the geofences collection
                        GeofenceMonitor.Current.Geofences.Remove(geofence);
                    }
                    else if (state == GeofenceState.Entered)
                    {
                        // Your app takes action based on the entered event

                        // NOTE: You might want to write your app to take particular
                        // action based on whether the app has internet connectivity.
                        foreach(LocationData data in selectedRoute.pubs)
                        {
                            if (data.name.Equals(geofence.Id))
                                geofencePub = data;
                        }

                        if(geofencePub != null)
                            Summary.Text = geofencePub.name + ", rating: " + geofencePub.rating;

                    }
                    else if (state == GeofenceState.Exited)
                    {
                        // Your app takes action based on the exited event

                        // NOTE: You might want to write your app to take particular
                        // action based on whether the app has internet connectivity.
                        visitedPubs = 1;
                        if(visitedPubs < selectedRoute.pubs.Count)
                            getRouteWithCurrentLocation(currentLocation.Point, selectedRoute.pubs[visitedPubs]);
                    }
                }
            });
        }

        private void NewRoute_Click(object sender, RoutedEventArgs e)
        {
            GeofenceMonitor.Current.Geofences.Clear();
            GeofenceMonitor.Current.GeofenceStateChanged -= OnGeofenceStateChanged;
            geolocator.PositionChanged -= PositionChanged;
            Frame.Navigate(typeof(NewRoute));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            GeofenceMonitor.Current.Geofences.Clear();
            GeofenceMonitor.Current.GeofenceStateChanged -= OnGeofenceStateChanged;
            geolocator.PositionChanged -= PositionChanged;
            Frame.Navigate(typeof(MainPage));
        }

        private void RoutesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (currentLocation != null)
            {
                InputMap.MapElements.Clear();
                InputMap.Routes.Clear();
                GeofenceMonitor.Current.Geofences.Clear();

                visitedPubs = 0;
                selectedRoute = (PubRoute)RoutesListView.SelectedItem;
                Debug.WriteLine(selectedRoute.name);
                Summary.Text = "Route is aan het inladen....";

                for(int index = 0; index < selectedRoute.pubs.Count; index++)
                {
                    AddMapIcon(selectedRoute.pubs[index]);
                    if (index > 0)
                        getRouteWithPubs(selectedRoute.pubs[index - 1], selectedRoute.pubs[index]);
                }

            
                AddMapIcon(currentLocation, "U bent hier");
                getRouteWithCurrentLocation(currentLocation.Point, selectedRoute.pubs[0]);
            }
            else
            {
                Summary.Text = "";
                Hyperlink link = new Hyperlink();
                link.Inlines.Add(new Run()
                {
                    Text = "settings voor Happyhour",
                    Foreground = new SolidColorBrush(Colors.White)
                });
                link.NavigateUri = new Uri("ms-settings:privacy-location");

                Summary.Inlines.Add(new Run()
                {
                    Text = "Huidige locatie niet bekend"
                });
                Summary.Inlines.Add(new LineBreak());
                Summary.Inlines.Add(new Run()
                {
                    Text = "Check de locatie "
                });
                Summary.Inlines.Add(link);

                RoutesListView.SelectedIndex = -1;
                getCurrentLocation();
            }
        }
    }
}
