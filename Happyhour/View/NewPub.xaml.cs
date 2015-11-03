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
            string city = Place_TextBox.Text;
            string country = Country_TextBox.Text;

            string openingTimeMa = OpeningTimeMa_TextBox.Text;
            string closingTimeMa = ClosingTimeMa_TextBox.Text;
            string openingTimeDi = OpeningTimeDi_TextBox.Text;
            string closingTimeDi = ClosingTimeDi_TextBox.Text;
            string openingTimeWo = OpeningTimeWo_TextBox.Text;
            string closingTimeWo = ClosingTimeWo_TextBox.Text;
            string openingTimeDo = OpeningTimeDo_TextBox.Text;
            string closingTimeDo = ClosingTimeDo_TextBox.Text;
            string openingTimeVr = OpeningTimeVr_TextBox.Text;
            string closingTimeVr = ClosingTimeVr_TextBox.Text;
            string openingTimeZa = OpeningTimeZa_TextBox.Text;
            string closingTimeZa = ClosingTimeZa_TextBox.Text;
            string openingTimeZo = OpeningTimeZo_TextBox.Text;
            string closingTimeZo = ClosingTimeZo_TextBox.Text;

            string longitude = Longitude_TextBox.Text;
            string latitude = Latitude_TextBox.Text;

            int selectedRatingIndex = Rating_ComboBox.SelectedIndex;
            Object rating = Rating_ComboBox.SelectedItem;

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
            else if (string.IsNullOrEmpty(OpeningTimeMa_TextBox.ToString()))
                ErrorMessage_TextBlock.Text = "Er is geen dag opgegeven.";
            else if (string.IsNullOrEmpty(openingTimeMa) &&
                     string.IsNullOrEmpty(openingTimeDi) &&
                     string.IsNullOrEmpty(openingTimeWo) &&
                     string.IsNullOrEmpty(openingTimeDo) &&
                     string.IsNullOrEmpty(openingTimeVr) &&
                     string.IsNullOrEmpty(openingTimeZa) &&
                     string.IsNullOrEmpty(openingTimeZo))
                ErrorMessage_TextBlock.Text = "Er zijn geen openingstijden opgegeven.";
            else if (string.IsNullOrEmpty(closingTimeMa) &&
                     string.IsNullOrEmpty(closingTimeDi) &&
                     string.IsNullOrEmpty(closingTimeWo) &&
                     string.IsNullOrEmpty(closingTimeDo) &&
                     string.IsNullOrEmpty(closingTimeVr) &&
                     string.IsNullOrEmpty(closingTimeZa) &&
                     string.IsNullOrEmpty(closingTimeZo))
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
            else
            {
                ErrorMessage_TextBlock.Text = "";

                List<bool> happyhourDays = new List<bool>();
                happyhourDays.Add((bool)Happyhour_1.IsChecked);
                happyhourDays.Add((bool)Happyhour_2.IsChecked);
                happyhourDays.Add((bool)Happyhour_3.IsChecked);
                happyhourDays.Add((bool)Happyhour_4.IsChecked);
                happyhourDays.Add((bool)Happyhour_5.IsChecked);
                happyhourDays.Add((bool)Happyhour_6.IsChecked);
                happyhourDays.Add((bool)Happyhour_7.IsChecked);

                List<string> openTimes = new List<string>();
                openTimes.Add(openingTimeMa);
                openTimes.Add(openingTimeDi);
                openTimes.Add(openingTimeWo);
                openTimes.Add(openingTimeDo);
                openTimes.Add(openingTimeVr);
                openTimes.Add(openingTimeZa);
                openTimes.Add(openingTimeZo);

                List<string> closeTimes = new List<string>();
                closeTimes.Add(closingTimeMa);
                closeTimes.Add(closingTimeDi);
                closeTimes.Add(closingTimeWo);
                closeTimes.Add(closingTimeDo);
                closeTimes.Add(closingTimeVr);
                closeTimes.Add(closingTimeZa);
                closeTimes.Add(closingTimeZo);

                LocationData pub = new LocationData(name, street, houseNumber, zipCode, city, country, selectedRatingIndex, openTimes, closeTimes, Convert.ToDouble(longitude), Convert.ToDouble(latitude), happyhourDays);
                LocationHandler.Instance.addPub(pub);

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
