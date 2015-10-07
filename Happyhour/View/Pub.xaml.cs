using Happyhour.Control;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            PubsListView.ItemsSource = pubList;
            
        }

        private void PubMenu_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PubMenu));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
