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
    public sealed partial class NewPub : Page
    {
        public NewPub()
        {
            this.InitializeComponent();
        }

        public void inputData()
        {
            string name = Name_TextBox.Text;
            string street = Street_TextBox.Text;
            string houseNumber = Housenumber_TextBox.Text;
            string zipCode = Zipcode_TextBox.Text;
            string place = Place_TextBox.Text;
            string country = Country_TextBox.Text;
            string openinghours = Openinghours_TextBox.Text;

            if (string.IsNullOrEmpty(name)) {
                ErrorMessage_TextBlock.Text = "Er is geen naam opgegeven.";
            } else if (string.IsNullOrEmpty(street)) {
                ErrorMessage_TextBlock.Text = "Er is geen straat opgegeven.";
            } else if (string.IsNullOrEmpty(houseNumber)) {
                ErrorMessage_TextBlock.Text = "Er is geen huisnummer opgegeven.";
            } else if (string.IsNullOrEmpty(zipCode)) {
                ErrorMessage_TextBlock.Text = "Er is geen postcode opgegeven.";
            } else if (string.IsNullOrEmpty(place)) {
                ErrorMessage_TextBlock.Text = "Er is geen plaats opgegeven.";
            } else if (string.IsNullOrEmpty(country)) {
                ErrorMessage_TextBlock.Text = "Er is geen land opgegeven.";
            } else if (string.IsNullOrEmpty(openinghours)) {
                ErrorMessage_TextBlock.Text = "Er zijn geen openingstijden opgegeven.";
            }
            
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            inputData();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(View.AppSettings));
        }
    }
}
