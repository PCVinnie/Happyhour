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

            string happyhourFromMa = HappyhourFromMa_TextBox.Text;
            string happyhourToMa = HappyhourToMa_TextBox.Text;
            string happyhourFromDi = HappyhourFromDi_TextBox.Text;
            string happyhourToDi = HappyhourToDi_TextBox.Text;
            string happyhourFromWo = HappyhourFromWo_TextBox.Text;
            string happyhourToWo = HappyhourToWo_TextBox.Text;
            string happyhourFromDo = HappyhourFromDo_TextBox.Text;
            string happyhourToDo = HappyhourToDo_TextBox.Text;
            string happyhourFromVr = HappyhourFromVr_TextBox.Text;
            string happyhourToVr = HappyhourToVr_TextBox.Text;
            string happyhourFromZa = HappyhourFromZa_TextBox.Text;
            string happyhourToZa = HappyhourToZa_TextBox.Text;
            string happyhourFromZo = HappyhourFromZo_TextBox.Text;
            string happyhourToZo = HappyhourToZo_TextBox.Text;

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
            else
            {
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


                LocationData pub = new LocationData(name, street, houseNumber, zipCode, city, country, selectedRatingIndex, Convert.ToDouble(longitude), Convert.ToDouble(latitude), openTimes, closeTimes, happyhourFrom, happyhourTo);
                fillTimes(pub, openTimes, closeTimes);
                fillHappyTimes(pub, happyhourFrom, happyhourTo);
                LocationHandler.Instance.addPub(pub);

                Frame.Navigate(typeof(View.PubMenu));
            }
        }

        private void fillTimes(LocationData data, List<string> openTimes, List<string> closeTimes)
        {

            for (int i = 0; i < 7; i++)
            {
                data.pubdays[i].open = new ClockTime();
                if (openTimes[i].ToUpper() == "CLOSED")
                    data.pubdays[i].isClosed = true;
                else
                {
                    data.pubdays[i].open.hour = Int32.Parse((openTimes[i])[0] + "" + (openTimes[i])[1]);
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

        private void fillHappyTimes(LocationData data, List<string> fromTimes, List<string> toTimes)
        {

            for (int i = 0; i < 7; i++)
            {
                data.pubdays[i].happyhourFrom = new ClockTime();
                if (fromTimes[i].ToUpper() == "CLOSED")
                    data.pubdays[i].isClosed = true;
                else
                {
                    data.pubdays[i].happyhourFrom.hour = Int32.Parse((fromTimes[i])[0] + "" + (fromTimes[i])[1]);
                    data.pubdays[i].happyhourFrom.minutes = Int32.Parse((fromTimes[i])[3] + "" + (fromTimes[i])[4]);

                    if (data.pubdays[i].happyhourFrom.hour < 10)
                        data.pubdays[i].happyhourFrom.stringhour = "0" + data.pubdays[i].happyhourFrom.hour;
                    else
                        data.pubdays[i].happyhourFrom.stringhour = data.pubdays[i].happyhourFrom.hour.ToString();

                    if (data.pubdays[i].happyhourFrom.minutes < 10)
                        data.pubdays[i].happyhourFrom.stringminutes = "0" + data.pubdays[i].happyhourFrom.minutes;
                    else
                        data.pubdays[i].happyhourFrom.stringminutes = data.pubdays[i].happyhourFrom.minutes.ToString();
                }





                data.pubdays[i].happyhourTo = new ClockTime();
                if (toTimes[i].ToUpper() == "CLOSED")
                    data.pubdays[i].isClosed = true;
                else
                {
                    data.pubdays[i].happyhourTo.hour = Int32.Parse((toTimes[i])[0] + "" + (toTimes[i])[1]);
                    data.pubdays[i].happyhourTo.minutes = Int32.Parse((toTimes[i])[3] + "" + (toTimes[i])[4]);

                    if (data.pubdays[i].happyhourTo.hour < 10)
                        data.pubdays[i].happyhourTo.stringhour = "0" + data.pubdays[i].happyhourTo.hour;
                    else
                        data.pubdays[i].happyhourTo.stringhour = data.pubdays[i].happyhourTo.hour.ToString();

                    if (data.pubdays[i].happyhourTo.minutes < 10)
                        data.pubdays[i].happyhourTo.stringminutes = "0" + data.pubdays[i].happyhourTo.minutes;
                    else
                        data.pubdays[i].happyhourTo.stringminutes = data.pubdays[i].happyhourTo.minutes.ToString();
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
