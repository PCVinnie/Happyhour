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
            XMLFileHandler reader = new XMLFileHandler();
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

        private void AppSettings_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
