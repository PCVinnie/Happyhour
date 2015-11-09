using Happyhour.Control;
using System;
using System.Collections.Generic;
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
    public sealed partial class PubMenu : Page
    {
        public PubMenu()
        {
            this.InitializeComponent();
            pubList.ItemsSource = LocationHandler.Instance.pubList;
            pubList.SelectedIndex = 0;

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

        private void NewPub_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(View.NewPub));
        }

        private void ChangePub_Click(object sender, RoutedEventArgs e)
        {
            LocationData chosenPub = (LocationData)pubList.SelectedItem;
            Frame.Navigate(typeof(View.ChangePub), chosenPub);
        }
     
        private void RemovePub_Click(object sender, RoutedEventArgs e)
        {
            LocationData chosenPub = (LocationData)pubList.SelectedItem;
            LocationHandler.Instance.deletePub(chosenPub.id);

            pubList.ItemsSource = LocationHandler.Instance.pubList;
            pubList.SelectedIndex = 0;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pub));
        }
    }
}

