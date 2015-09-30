using Happyhour.Control;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Happyhour.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PubMenu : Page
    {
        public PubMenu()
        {
            this.InitializeComponent();
            pubList.ItemsSource = LocationHandler.Instance.pubList;
            pubList.SelectedIndex = 0;
        }

        private void NewPub_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(View.NewPub));
        }

        private void ChangePub_Click(object sender, RoutedEventArgs e)
        {
            LocationData chosenPub = (LocationData)pubList.SelectedItem;
            Frame.Navigate(typeof(View.ChangePub), chosenPub);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pub));
        }
    }
}

