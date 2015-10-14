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
    public sealed partial class ChangePub : Page
    {
        LocationData pub;
        public ChangePub()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.pub = (LocationData)e.Parameter;
            fillTextFields();
        }

        private void fillTextFields()
        {
            Name_TextBox.Text = pub.name;
            Street_TextBox.Text = pub.street;
            Housenumber_TextBox.Text = pub.streetNumber;
            Place_TextBox.Text = pub.city;
            Zipcode_TextBox.Text = pub.zipcode;
            Country_TextBox.Text = pub.country;
            Rating_ComboBox.SelectedIndex = pub.rating;
            Longitude_TextBox.Text = pub.longitude.ToString();
            Latitude_TextBox.Text = pub.latitude.ToString();
        }

        public void inputData()
        {
            string name = Name_TextBox.Text;
            string street = Street_TextBox.Text;
            string houseNumber = Housenumber_TextBox.Text;
            string zipCode = Zipcode_TextBox.Text;
            string city = Place_TextBox.Text;
            string country = Country_TextBox.Text;
            string openingTime = OpeningTime_TextBox.Text;
            string closingTime = ClosingTime_TextBox.Text;
            string longitude = Longitude_TextBox.Text;
            string latitude = Latitude_TextBox.Text;

            int selectedRatingIndex = Rating_ComboBox.SelectedIndex;
            Object rating = Rating_ComboBox.SelectedItem;
            int selectedDayIndex = Days_ComboBox.SelectedIndex;
            Object day = Days_ComboBox.SelectedItem;

            if (string.IsNullOrEmpty(name))
                ErrorMessage_TextBlock.Text = "Er is geen naam opgegeven.";
            else if (string.IsNullOrEmpty(street))
                ErrorMessage_TextBlock.Text = "Er is geen straat opgegeven.";
            else if (string.IsNullOrEmpty(houseNumber))
                ErrorMessage_TextBlock.Text = "Er is geen huisnummer opgegeven.";
            else if (string.IsNullOrEmpty(zipCode))
                ErrorMessage_TextBlock.Text = "Er is geen postcode opgegeven.";
            else if (string.IsNullOrEmpty(city))
                ErrorMessage_TextBlock.Text = "Er is geen plaats opgegeven.";
            else if (string.IsNullOrEmpty(country))
                ErrorMessage_TextBlock.Text = "Er is geen land opgegeven.";
            else if (string.IsNullOrEmpty(day.ToString()))
                ErrorMessage_TextBlock.Text = "Er is geen dag opgegeven.";
            else if (string.IsNullOrEmpty(openingTime))
                ErrorMessage_TextBlock.Text = "Er zijn geen openingstijden opgegeven.";
            else if (string.IsNullOrEmpty(closingTime))
                ErrorMessage_TextBlock.Text = "Er zijn geen sluitingstijden opgegeven.";
            else if (string.IsNullOrEmpty(rating.ToString()))
                ErrorMessage_TextBlock.Text = "Er is geen rating opgegeven.";
            else if (string.IsNullOrEmpty(longitude.ToString()))
                ErrorMessage_TextBlock.Text = "Er is geen longitude opgegeven.";
            else if (string.IsNullOrEmpty(latitude.ToString()))
                ErrorMessage_TextBlock.Text = "Er is geen latitude opgegeven.";
            else if (Happyhour_1.IsChecked == false &&
                     Happyhour_2.IsChecked == false &&
                     Happyhour_3.IsChecked == false &&
                     Happyhour_4.IsChecked == false &&
                     Happyhour_5.IsChecked == false &&
                     Happyhour_6.IsChecked == false &&
                     Happyhour_7.IsChecked == false)
                ErrorMessage_TextBlock.Text = "Er is geen dag opgegeven.";
            else {
                ErrorMessage_TextBlock.Text = "";

                LocationData p = new LocationData(name, street, houseNumber, zipCode, city, country, selectedRatingIndex, selectedDayIndex, openingTime, closingTime, Convert.ToDouble(longitude), Convert.ToDouble(latitude));
                p.id = pub.id;
                LocationHandler.Instance.addPub(p);

                Frame.Navigate(typeof(View.PubMenu));
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            inputData();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(View.PubMenu));
        }
    }
}
