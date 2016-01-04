using Happyhour.Control;
using Happyhour.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Happyhour.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Pub : Page
    {
        LocationHandler locationHandler;
        ObservableCollection<LocationData> pubList;
        public Pub()
        {
            this.InitializeComponent();
            locationHandler = LocationHandler.Instance;
            

            pubList = new ObservableCollection<LocationData>(locationHandler.pubList);
            //PubsListView.ItemsSource = pubList;

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

            SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;

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

        private void PubMenu_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PubMenu));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void autoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion == null)
            {
                String searchtext = args.QueryText.ToUpper();
                PubsListView.SelectedItem = null;
                PubsListView.UpdateLayout();

                foreach (LocationData data in pubList)
                {
                    var container = (SelectorItem)PubsListView.ContainerFromItem(data);
                    if (container != null)
                    {
                        container.IsSelected = false;
                    }
                }

                foreach (LocationData data in pubList)
                {
                    String name = data.name.ToUpper();
                    String city = data.city.ToUpper();

                    if (name.Contains(searchtext))
                    {
                        var container = (SelectorItem)PubsListView.ContainerFromItem(data);
                        if (container != null)
                        {
                            container.IsSelected = true;
                            //PubsListView.SelectedIndex = pubList.IndexOf(data);
                            PubsListView.UpdateLayout();
                            PubsListView.ScrollIntoView(PubsListView.SelectedItem);
                        }
                    }
                    else if(city.Contains(searchtext))
                    {
                        var container = (SelectorItem)PubsListView.ContainerFromItem(data);
                        if (container != null)
                        {
                            container.IsSelected = true;
                            //PubsListView.SelectedIndex = pubList.IndexOf(data);
                            PubsListView.UpdateLayout();
                            PubsListView.ScrollIntoView(data);
                        }
                    }
                }
            }
        }
    }
}
