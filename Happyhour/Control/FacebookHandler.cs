using Facebook;
using Facebook.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;

namespace Happyhour.Control
{
    class FacebookHandler
    {
        private static FacebookHandler instance;
        public FBUser fbUser;
        public bool noInternet = false;
        private FBSession sess;

        private FacebookHandler()
        {
            fbUser = null;
            sess = FBSession.ActiveSession;
        }

        public bool isLoggedIn()
        {
            if (fbUser == null)
                return false;
            else
                return true;
        }

        public static FacebookHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FacebookHandler();
                }
                return instance;
            }
        }

        public static bool checkInternetConnection()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            return internet;
        }

        public async Task Login()
        {
            if (checkInternetConnection())
            {
                // Get active session
                sess = FBSession.ActiveSession;
                if (!sess.LoggedIn)
                {
                    sess.FBAppId = "438902522960732";
                    sess.WinAppId = "3779de4318934fee8f4d5d3a4411481a";

                    // Add permissions required by the app
                    List<String> permissionList = new List<String>();
                    permissionList.Add("public_profile");
                    permissionList.Add("user_friends");
                    permissionList.Add("user_likes");
                    //permissionList.Add("user_groups");
                    permissionList.Add("user_location");
                    //permissionList.Add("user_photos");
                    permissionList.Add("publish_actions");

                    FBPermissions permissions = new FBPermissions(permissionList);

                    // Login to Facebook
                    FBResult result = await sess.LoginAsync(permissions);

                    if (result.Succeeded)
                    {
                        fbUser = sess.User;

                        //Read the user information
                        string userName = fbUser.Name;
                        string userFirstname = fbUser.FirstName;
                        string userLastname = fbUser.LastName;

                        string userGender = fbUser.Gender;
                        int userTimezone = fbUser.Timezone;

                        FBProfilePictureData userPicture = fbUser.Picture;
                    }
                    else
                    {
                        fbUser = null;
                    }
                }
                else
                {
                    fbUser = sess.User;
                }
            }
            else
            {
                noInternet = true;
                fbUser = null;
            }
        }

        public async Task Logout()
        {
            FBSession sess = FBSession.ActiveSession;
            await sess.LogoutAsync();
        }

        public async void PostMessage(String text)
        {
            PropertySet postParams = new PropertySet();
            postParams.Add("message", text);
  
            string result = await FBClient.PostTaskAsync("/me/feed", postParams);
        }

    }
}
