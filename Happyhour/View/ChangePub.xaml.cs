using Happyhour.Control;
using Happyhour.Model;
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

            OpeningTimeMa_TextBox.Text = pub.pubdays[0].isClosed ? "Closed" : pub.pubdays[0].open.getTimeForGui();
            ClosingTimeMa_TextBox.Text = pub.pubdays[0].isClosed ? "Closed" : pub.pubdays[0].close.getTimeForGui();
            OpeningTimeDi_TextBox.Text = pub.pubdays[1].isClosed ? "Closed" : pub.pubdays[1].open.getTimeForGui();
            ClosingTimeDi_TextBox.Text = pub.pubdays[1].isClosed ? "Closed" : pub.pubdays[1].close.getTimeForGui();
            OpeningTimeWo_TextBox.Text = pub.pubdays[2].isClosed ? "Closed" : pub.pubdays[2].open.getTimeForGui();
            ClosingTimeWo_TextBox.Text = pub.pubdays[2].isClosed ? "Closed" : pub.pubdays[2].close.getTimeForGui();
            OpeningTimeDo_TextBox.Text = pub.pubdays[3].isClosed ? "Closed" : pub.pubdays[3].open.getTimeForGui();
            ClosingTimeDo_TextBox.Text = pub.pubdays[3].isClosed ? "Closed" : pub.pubdays[3].close.getTimeForGui();
            OpeningTimeVr_TextBox.Text = pub.pubdays[4].isClosed ? "Closed" : pub.pubdays[4].open.getTimeForGui();
            ClosingTimeVr_TextBox.Text = pub.pubdays[4].isClosed ? "Closed" : pub.pubdays[4].close.getTimeForGui();
            OpeningTimeZa_TextBox.Text = pub.pubdays[5].isClosed ? "Closed" : pub.pubdays[5].open.getTimeForGui();
            ClosingTimeZa_TextBox.Text = pub.pubdays[5].isClosed ? "Closed" : pub.pubdays[5].close.getTimeForGui();
            OpeningTimeZo_TextBox.Text = pub.pubdays[6].isClosed ? "Closed" : pub.pubdays[6].open.getTimeForGui();
            ClosingTimeZo_TextBox.Text = pub.pubdays[6].isClosed ? "Closed" : pub.pubdays[6].close.getTimeForGui();

            Rating_ComboBox.SelectedIndex = pub.rating;
            Longitude_TextBox.Text = pub.position.Longitude.ToString();
            Latitude_TextBox.Text = pub.position.Latitude.ToString();
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

            string happyhourFromMa = HappyhourFromMa_TextBox.Text;
            string happyhourToMa = HappyhourFromMa_TextBox.Text;
            string happyhourFromDi = HappyhourFromMa_TextBox.Text;
            string happyhourToDi = HappyhourFromMa_TextBox.Text;
            string happyhourFromWo = HappyhourFromMa_TextBox.Text;
            string happyhourToWo = HappyhourFromMa_TextBox.Text;
            string happyhourFromDo = HappyhourFromMa_TextBox.Text;
            string happyhourToDo = HappyhourFromMa_TextBox.Text;
            string happyhourFromVr = HappyhourFromMa_TextBox.Text;
            string happyhourToVr = HappyhourFromMa_TextBox.Text;
            string happyhourFromZa = HappyhourFromMa_TextBox.Text;
            string happyhourToZa = HappyhourFromMa_TextBox.Text;
            string happyhourFromZo = HappyhourFromMa_TextBox.Text;
            string happyhourToZo = HappyhourFromMa_TextBox.Text;

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
            else if (string.IsNullOrEmpty(happyhourFromMa) &&
                     string.IsNullOrEmpty(happyhourFromDi) &&
                     string.IsNullOrEmpty(happyhourFromWo) &&
                     string.IsNullOrEmpty(happyhourFromDo) &&
                     string.IsNullOrEmpty(happyhourFromVr) &&
                     string.IsNullOrEmpty(happyhourFromZa) &&
                     string.IsNullOrEmpty(happyhourFromZo))
                ErrorMessage_TextBlock.Text = "Er is geen tijd opgegeven.";
            else if (string.IsNullOrEmpty(happyhourToMa) &&
                     string.IsNullOrEmpty(happyhourToDi) &&
                     string.IsNullOrEmpty(happyhourToWo) &&
                     string.IsNullOrEmpty(happyhourToDo) &&
                     string.IsNullOrEmpty(happyhourToVr) &&
                     string.IsNullOrEmpty(happyhourToZa) &&
                     string.IsNullOrEmpty(happyhourToZo))
                ErrorMessage_TextBlock.Text = "Er is geen tijd opgegeven.";
            else {
                ErrorMessage_TextBlock.Text = "";

                List<string> happyhourFrom = new List<string>();
                happyhourFrom.Add(happyhourFromMa);
                happyhourFrom.Add(happyhourFromDi);
                happyhourFrom.Add(happyhourFromWo);
                happyhourFrom.Add(happyhourFromDo);
                happyhourFrom.Add(happyhourFromVr);
                happyhourFrom.Add(happyhourFromZa);
                happyhourFrom.Add(happyhourFromZo);

                List<string> happyhourTo = new List<string>();
                happyhourTo.Add(happyhourToMa);
                happyhourTo.Add(happyhourToDi);
                happyhourTo.Add(happyhourToWo);
                happyhourTo.Add(happyhourToDo);
                happyhourTo.Add(happyhourToVr);
                happyhourTo.Add(happyhourToZa);
                happyhourTo.Add(happyhourToZo);


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

                LocationData p = new LocationData(name, street, houseNumber, zipCode, city, country, selectedRatingIndex, Convert.ToDouble(longitude), Convert.ToDouble(latitude), happyhourFrom, happyhourTo);
                p.id = pub.id;

                fillTimes(p, openTimes, closeTimes);
                LocationHandler.Instance.setPub(p);

                Frame.Navigate(typeof(View.PubMenu));
            }
        }

        private void fillTimes(LocationData data, List<string> openTimes, List<string> closeTimes)
        {

            for(int i = 0; i < 7; i++)
            {
                data.pubdays[i].open = new ClockTime();
                if (openTimes[i].ToUpper() == "CLOSED")
                    data.pubdays[i].isClosed = true;
                else
                {
                    data.pubdays[i].open.hour = Int32.Parse((openTimes[i])[0] + "" +(openTimes[i])[1]);
                    data.pubdays[i].open.minutes = Int32.Parse((openTimes[i])[3] + "" + (openTimes[i])[4]);

                    if (data.pubdays[i].open.hour < 10)
                        data.pubdays[i].open.stringhour = "0" + data.pubdays[i].open.hour;
                    else
                        data.pubdays[i].open.stringhour = data.pubdays[i].open.hour.ToString();

                    if (data.pubdays[i].open.minutes < 10)
                        data.pubdays[i].open.stringminutes = "0" + data.pubdays[i].open.minutes;
                    else
                        data.pubdays[i].open.stringminutes = data.pubdays[i].open.minutes.ToString();
                }





                data.pubdays[i].close = new ClockTime();
                if (closeTimes[i].ToUpper() == "CLOSED")
                    data.pubdays[i].isClosed = true;
                else
                {
                    data.pubdays[i].close.hour = Int32.Parse((closeTimes[i])[0] + "" + (closeTimes[i])[1]);
                    data.pubdays[i].close.minutes = Int32.Parse((closeTimes[i])[3] + "" + (closeTimes[i])[4]);

                    if (data.pubdays[i].close.hour < 10)
                        data.pubdays[i].close.stringhour = "0" + data.pubdays[i].close.hour;
                    else
                        data.pubdays[i].close.stringhour = data.pubdays[i].close.hour.ToString();

                    if (data.pubdays[i].close.minutes < 10)
                        data.pubdays[i].close.stringminutes = "0" + data.pubdays[i].close.minutes;
                    else
                        data.pubdays[i].close.stringminutes = data.pubdays[i].close.minutes.ToString();
                }
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
