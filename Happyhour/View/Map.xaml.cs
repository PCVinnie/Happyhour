using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
using Windows.UI.Xaml.Media;
using System.Collections.Generic;



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
            //geolocator.MovementThreshold = 10;
            routeList = new ObservableCollection<PubRoute>(LocationHandler.Instance.routeList);

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

            SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;

            isPubOpen(routeList[0].pubs[0]);
            getCurrentLocation();
            GeofenceMonitor.Current.GeofenceStateChanged += OnGeofenceStateChanged;

        }

        private void App_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
                return;

            // Navigate back if possible, and if the event has not 
            // already been handled .
            if (rootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }

        private bool isPubOpen(LocationData data)
        {
            bool open = true;
            String day = DateTime.Now.DayOfWeek.ToString();
            PubDay pubday = data.getDay(day);
            if (pubday.isClosed)
                open = false;
            else 
            {
                bool timeopen = pubday.isNowOpen(); 
            }

            return open;
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
                    if (InputMap.MapElements[i] is MapIcon)
                    {
                        MapIcon icon = (MapIcon)InputMap.MapElements[i];
                        if (icon.Title.Equals("U bent hier"))
                        {
                            isInList = true;
                            InputMap.MapElements.Remove(icon);
                        }
                    }
                }

                if(isInList)
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
            Geocircle circle = new Geocircle(pos, 35);
            MonitoredGeofenceStates monitoredStates =
                MonitoredGeofenceStates.Entered |
                MonitoredGeofenceStates.Exited |
                MonitoredGeofenceStates.Removed;
            TimeSpan dwellTime = TimeSpan.FromSeconds(1);
            var geofence = new Windows.Devices.Geolocation.Geofencing.Geofence(location.name, circle, monitoredStates, false, dwellTime);
            GeofenceMonitor.Current.Geofences.Add(geofence);

            drawGeofence(location, 35);
        }

        private void AddMapIcon(Geocoordinate coordinate, string name)
        {
            MapIcon MapIcon1 = new MapIcon();
            MapIcon1.Location = coordinate.Point;
            MapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
            MapIcon1.Title = name;
            InputMap.MapElements.Add(MapIcon1);
        }

        private void drawGeofence(LocationData location, double radius)
        {
            var strokeColor = Colors.DarkBlue;
            strokeColor.A = 100;
            var fillColor = Colors.Blue;
            fillColor.A = 50;

            MapPolygon circlePolygon = new MapPolygon
            {
                FillColor = fillColor,
                StrokeColor = strokeColor,
                StrokeThickness = 3,
                StrokeDashed = true,
                ZIndex = 1,
                Path = new Geopath(GetCirclePoints(location.position, radius))
            };

            InputMap.MapElements.Add(circlePolygon);
        }

        private BasicGeoposition GetAtDistanceBearing(BasicGeoposition point, double distance, double bearing)
        {
            double degreesToRadian = Math.PI / 180.0;
            double radianToDegrees = 180.0 / Math.PI;
            double earthRadius = 6378137.0;

            double latA = point.Latitude * degreesToRadian;
            double lonA = point.Longitude * degreesToRadian;
            double angularDistance = distance / earthRadius;
            double trueCourse = bearing * degreesToRadian;

            double lat = Math.Asin(
                Math.Sin(latA) * Math.Cos(angularDistance) +
                Math.Cos(latA) * Math.Sin(angularDistance) * Math.Cos(trueCourse));

            double dlon = Math.Atan2(
                Math.Sin(trueCourse) * Math.Sin(angularDistance) * Math.Cos(latA),
                Math.Cos(angularDistance) - Math.Sin(latA) * Math.Sin(lat));

            double lon = ((lonA + dlon + Math.PI) % (Math.PI * 2)) - Math.PI;

            BasicGeoposition result = new BasicGeoposition { Latitude = lat * radianToDegrees, Longitude = lon * radianToDegrees };

            return result;
        }

        public IList<BasicGeoposition> GetCirclePoints(BasicGeoposition center, double radius)
        {
            int nrOfPoints = 50;
            double angle = 360.0 / nrOfPoints;
            List<BasicGeoposition> locations = new List<BasicGeoposition>();
            for (int i = 0; i <= nrOfPoints; i++)
            {
                locations.Add(GetAtDistanceBearing(center, radius, angle * i));
            }
            return locations;
        }

        /*
        /   Voor het bepalen van de route en richtingen.
        */
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

                        if (geofencePub != null)
                        {
                            //string happyhourText = "Happyhour: No";
                            String day = DateTime.Now.DayOfWeek.ToString();
                            PubDay pubday = geofencePub.getDay(day);
                           // if(pubday.happyhour)
                                //happyhourText = "Happyhour: Yes";

                            Summary.Text = "";
                            Hyperlink link = new Hyperlink();
                            link.Inlines.Add(new Run()
                            {
                                Text = geofencePub.name,
                            });
                            Summary.Inlines.Add(new LineBreak());

                            if (isPubOpen(geofencePub))
                            {
                                Summary.Inlines.Add(new Run()
                                {
                                    Text = "Open: yes"
                                });
                            }
                            else
                            {
                                Summary.Inlines.Add(new Run()
                                {
                                    Text = "Open: no",
                                });
                            }
                            Summary.Inlines.Add(link);                               
                        }

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
