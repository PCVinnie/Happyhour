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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Happyhour.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Map : Page
    {
        ObservableCollection<PubRoute> routeList;
        public Map()
        {
            this.InitializeComponent();

            routeList = new ObservableCollection<PubRoute>(LocationHandler.Instance.routeList);
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
        }

        /*
        /   Voor het bepalen van de route en richtingen.
        */
        private async void GetRouteAndDirections(LocationData startLoc, LocationData endLoc, bool startIsGPS)
        {
            BasicGeoposition startLocation = startLoc.position;
            Geopoint startPoint = new Geopoint(startLocation);

            BasicGeoposition endLocation = endLoc.position;
            Geopoint endPoint = new Geopoint(endLocation);

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
                viewOfRoute.RouteColor = Colors.Orange;
                viewOfRoute.OutlineColor = Colors.Black;

                InputMap.Routes.Add(viewOfRoute);

                await InputMap.TrySetViewBoundsAsync(routeResult.Route.BoundingBox, null, Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
            }
        }

        private void NewRoute_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewRoute));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void RoutesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InputMap.MapElements.Clear();
            InputMap.Routes.Clear();
            PubRoute selectedRoute = (PubRoute)RoutesListView.SelectedItem;
            Debug.WriteLine(selectedRoute.name);

            for(int index = 0; index < selectedRoute.pubs.Count; index++)
            {
                AddMapIcon(selectedRoute.pubs[index]);
                if (index > 0)
                    GetRouteAndDirections(selectedRoute.pubs[index - 1], selectedRoute.pubs[index], false);
            }
        }
    }
}
