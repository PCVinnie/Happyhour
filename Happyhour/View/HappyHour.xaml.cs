using Happyhour.Control;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Happyhour.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HappyHour : Page
    {
        LocationHandler locationHandler;
        ObservableCollection<LocationData> pubList;

        public HappyHour()
        {
            this.InitializeComponent();
            locationHandler = LocationHandler.Instance;

            pubList = new ObservableCollection<LocationData>(locationHandler.pubList);
            PubsListView.ItemsSource = pubList;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
