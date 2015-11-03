using Happyhour.Model;
using System.Collections.Generic;
using Windows.Devices.Geolocation;

public class LocationData
{
    public int id;
    public string name { get; set; }
    public string street { get; set; }
    public string streetNumber { get; set; }
    public string zipcode { get; set; }
    public string city { get; set; }
    public string country { get; set; }

    public List<ClockTime> openTimes { get; set; }
    public List<ClockTime> closeTimes { get; set; }

    public List<PubDay> pubdays { get; set; }

    public double longitude { get; set; }
    public double latitude { get; set; }
    public int rating { get; set; }
    public List<bool> happyhourDays { get; set; }

    public BasicGeoposition position;

    public LocationData()
    {
        id = -1; 
        name = "";
        street = "";
        streetNumber = "0";
        zipcode = "0000AA";
        city = "Amsterdam";
        country = "Nederland";
        longitude = 0.0;
        latitude = 0.0;
        rating = 0;

        position = new BasicGeoposition();
        openTimes = new List<ClockTime>();
        closeTimes = new List<ClockTime>();
        happyhourDays = new List<bool>();

        pubdays = new List<PubDay>();
        for(int i = 0; i < 7; i++)
        {
            pubdays.Add(new PubDay(i));
        }
    }

    public LocationData(string name, string street, string streetNumber, string zipcode, string city, string country, int rating,
        List<string> openingtime, List<string> closingtime, double longitude, double latitude, List<bool> happyhourDays)
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
        this.happyhourDays = happyhourDays;

        position = new BasicGeoposition();
        openTimes = new List<ClockTime>();
        closeTimes = new List<ClockTime>();

        for (int index = 0; index < openingtime.Count; index++)
            openTimes.Add(new ClockTime(splitStringToInt(openingtime[index])[0], splitStringToInt(openingtime[index])[1], index, true));

        for (int index = 0; index < closingtime.Count; index++)
            closeTimes.Add(new ClockTime(splitStringToInt(closingtime[index])[0], splitStringToInt(closingtime[index])[1], index, true));
    }

    /*public string addOpenTime(int day, int hour, int minutes)
    {
        bool contains = false;
        foreach (ClockTime time in openTimes)
        {
            if(time.day == day)
            {
                contains = true;
            }
        }

        if (!contains)
        {
            openTimes.Add(new ClockTime(hour, minutes, day, false));
            return "Added new open time";
        }
        else
            return "This day already has a open time";
    }

    public string addOpenTime(ClockTime time)
    {
        bool contains = false;
        foreach (ClockTime t in openTimes)
        {
            if (time.day == t.day)
            {
                contains = true;
            }
        }

        if (!contains)
        {
            openTimes.Add(time);
            return "Added new open time";
        }
        else
            return "This day already has a open time";
    }

    public string addCloseTime(int day, int hour, int minutes)
    {
        bool contains = false;
        foreach (ClockTime time in closeTimes)
        {
            if (time.day == day)
            {
                contains = true;
            }
        }

        if (!contains)
        {
            closeTimes.Add(new ClockTime(hour, minutes, day, false));
            return "Added new close time";
        }
        else
            return "This day already has a close time";
    }

    public string addCloseTime(ClockTime time)
    {
        bool contains = false;
        foreach (ClockTime t in closeTimes)
        {
            if (time.day == t.day)
            {
                contains = true;
            }
        }

        if (!contains)
        {
            closeTimes.Add(time);
            return "Added new close time";
        }
        else
            return "This day already has a close time";
    }*/

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
        foreach(PubDay time in pubdays)
        {
            if(day == time.getDay())
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

    public void addHappyhourDays(bool day)
    {
        happyhourDays.Add(day);
    }

    public string getOpeningTime()
    {
        string openingTime = "test";

        foreach (ClockTime op in openTimes)
        {
           openingTime += op.getString() + "\n";
        }

        return openingTime;
    }

    public string getClosingTime()
    {
        string closingTime = "test";

        foreach (ClockTime op in closeTimes)
        {
            closingTime += op.getString() + "\n";
        }

        return closingTime;
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