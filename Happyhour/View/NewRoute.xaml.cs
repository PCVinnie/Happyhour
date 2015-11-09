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
using Windows.Foundation.Metadata;
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
    public sealed partial class NewRoute : Page
    {
        ObservableCollection<LocationData> pubList;
        public NewRoute()
        {
            this.InitializeComponent();
            pubList = new ObservableCollection<LocationData>();

            fromPub_ComboBox.ItemsSource = LocationHandler.Instance.pubList;
            fromPub_ComboBox.SelectedIndex = 0;

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

        private void AddToList_Click(object sender, RoutedEventArgs e)
        {
            LocationData selectedPub = (LocationData)fromPub_ComboBox.SelectedItem;
            if (!pubList.Contains(selectedPub))
                pubList.Add(selectedPub);
            else
                ErrorMessage_TextBlock.Text = "Lijst bevat deze pub al";
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
           if(string.IsNullOrEmpty(Name_TextBox.Text))
                ErrorMessage_TextBlock.Text = "Er is geen naam opgegeven";
           else if(pubList.Count < 2)
                ErrorMessage_TextBlock.Text = "Er zijn te weinig pubs opgegeven voor een route";
           else
            {
                PubRoute route = new PubRoute(Name_TextBox.Text, pubList.ToList());
                LocationHandler.Instance.addRoute(route);

                Frame.Navigate(typeof(View.Map));
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(View.Map));
        }
    }
}
