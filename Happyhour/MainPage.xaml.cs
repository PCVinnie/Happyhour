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

using Windows.Security.Authentication.Web;
using Facebook;
using Facebook.Graph;
using Windows.Networking.Connectivity;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Happyhour
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private FacebookHandler fbHandler;
        public MainPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

            fbHandler = FacebookHandler.Instance;

            fbHandler.Logout();

            FacebookLogout.IsEnabled = false;
            Facebook.IsEnabled = true;
        }
        private void Happyhour_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(View.HappyHour));
        }

        private void Pub_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(View.Pub));
        }

        private void Route_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(View.Map));
        }

        private void Credits_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(View.Credits));
        }

        private async void Facebook_Click(object sender, RoutedEventArgs e)
        {
            await fbHandler.Login();

            if(fbHandler.noInternet)
            {
                FacebookUser.Text = "Geen internet";
            }

            if(fbHandler.fbUser != null)
            {
                FacebookUser.Text = fbHandler.fbUser.Name;
                FacebookLogout.IsEnabled = true;
                Facebook.IsEnabled = false;
            }
        }

        private async void FacebookLogout_Click(object sender, RoutedEventArgs e)
        {
            await fbHandler.Logout();
            FacebookUser.Text = "Niet aangemeld";

            FacebookLogout.IsEnabled = false;
            Facebook.IsEnabled = false;
        }

    }
}
