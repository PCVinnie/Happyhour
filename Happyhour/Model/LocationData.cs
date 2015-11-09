using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Happyhour.Model
{
    class LocationData
    {
        public int id;
        public string name { get; set; }
        public string street { get; set; }
        public string streetNumber { get; set; }
        public string zipcode { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string time { get; set; }

        public List<PubDay> pubdays { get; set; }

        public double longitude { get; set; }
        public double latitude { get; set; }
        public int rating { get; set; }
        public string happyhour { get; set; }

        public BasicGeoposition position;

        string[] days = { "Ma", "Di", "Wo", "Do", "Vr", "Za", "Zo" };

        public LocationData()
        {
            id = -1;
            name = "";
            street = "";
            streetNumber = "0";
            zipcode = "0000AA";
            city = "Amsterdam";
            country = "Nederland";
            //longitude = 0.0;
            //latitude = 0.0;
            rating = 0;

            position = new BasicGeoposition();

            pubdays = new List<PubDay>();
            for (int i = 0; i < 7; i++)
            {
                pubdays.Add(new PubDay(i));
            }

            /* List<string> time = new List<string>();
                for (int i = 0; i < 7; i++)
                    time.Add("00:00");*/
            //getOpenAndClosingTime(time, time, time, time);
        }

        public LocationData(string name, string street, string streetNumber, string zipcode, string city, string country, int rating,
                double longitude, double latitude, List<string> openTimes, List<string> closeTimes, List<string> happyhourFrom, List<string> happyhourTo)
        {
            id = -1;
            this.name = name;
            this.street = street;
            this.streetNumber = streetNumber;
            this.zipcode = zipcode;
            this.city = city;
            this.country = country;
            this.rating = rating;
            this.longitude = longitude;
            this.latitude = latitude;

            position = new BasicGeoposition();
            position.Longitude = longitude;
            position.Latitude = latitude;

            pubdays = new List<PubDay>();
            for (int i = 0; i < 7; i++)
            {
                pubdays.Add(new PubDay(i));
            }

            //getOpenAndClosingTime(openTimes, closeTimes, happyhourFrom, happyhourTo);
        }

        /*
        public void getOpenAndClosingTime(List<string> openTimes, List<string> closeTimes, List<string> happyhourFrom, List<string> happyhourTo)
        {
            XMLFileHandler xmlFileHandler = new XMLFileHandler();
            for (int i = 0; i < openTimes.Count; i++)
                time += days[i] + " " + openTimes[i] + " - " + closeTimes[i] + "\n";

            for (int i = 0; i < happyhourFrom.Count; i++)
                happyhour += days[i] + " " + xmlFileHandler.readPubXMLFile()[i].pubdays[i].happyhourFrom + " - " + pubdays[i].happyhourTo.hour + "\n";
        }
        */

        public void setPubdayTimes()
        {
            foreach (PubDay day in pubdays)
            {
                day.setTimeString();
                day.setHappyhourString();
            }
        }

        public PubDay getDay(string dayString)
        {
            PubDay pubday = null;
            foreach (PubDay day in pubdays)
            {
                if (day.getDay() == dayString)
                    pubday = day;
            }

            return pubday;
        }

        public string getOpenTimeOfDay(string day)
        {
            foreach (PubDay time in pubdays)
            {
                if (day == time.getDay())
                {
                    return time.open.getString();
                }
            }

            return null;
        }

        public string getCloseTimeOfDay(string day)
        {
            foreach (PubDay time in pubdays)
            {
                if (day == time.getDay())
                {
                    return time.close.getString();
                } 
            }

            return null;
        }

        public int[] splitStringToInt(string value)
        {
            int[] time = new int[2];
            string[] output = value.Split(':');

            for (int i = 0; i < output.Length; i++)
            {
                time[i] = System.Convert.ToInt32(output[i]);
            }

            return time;
        }
    }
}
