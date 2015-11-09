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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Happyhour
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
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
            string SID = WebAuthenticationBroker.GetCurrentApplicationCallbackUri().ToString();

            // Get active session
            FBSession sess = FBSession.ActiveSession;
            sess.FBAppId = "438902522960732";
            sess.WinAppId = "3779de4318934fee8f4d5d3a4411481a";

            // Add permissions required by the app
            List<String> permissionList = new List<String>();
            permissionList.Add("public_profile");
            permissionList.Add("user_friends");
            permissionList.Add("user_likes");
            permissionList.Add("user_groups");
            permissionList.Add("user_location");
            permissionList.Add("user_photos");
            permissionList.Add("publish_actions");

            FBPermissions permissions = new FBPermissions(permissionList);

            // Login to Facebook
            FBResult result = await sess.LoginAsync(permissions);

            if (result.Succeeded)
            {

            }
            else
            {

            }

            //Frame.Navigate(typeof(View.Facebook));
        }
    }
}
