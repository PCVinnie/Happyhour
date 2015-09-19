using System.Collections.Generic;
using Windows.Devices.Geolocation;

public class LocationData
{
    public string name;
    public string street;
    public int streetNumber;
    public string postcode;
    public string place;
    public string country;
    public string happyHour;

    private List<ClockTime> openTimes;
    private List<ClockTime> closeTimes;
    public double rating;

    public BasicGeoposition position;

    public LocationData()
    {
        name = "";
        street = "";
        streetNumber = 0;
        postcode = "0000AA";
        place = "Amsterdam";
        country = "Nederland";
        happyHour = "";

        position = new BasicGeoposition();
        rating = 0;
        openTimes = new List<ClockTime>();
        closeTimes = new List<ClockTime>();
    }

    public string addOpenTime(int day, int hour, int minutes)
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
    }

    public string getOpenTimeOfDay(string day)
    {
        foreach(ClockTime time in openTimes)
        {
            if(day == time.getDay())
            {
                return time.getString();
            }
        }

        return null;
    }

    public string getCloseTimeOfDay(string day)
    {
        foreach (ClockTime time in closeTimes)
        {
            if (day == time.getDay())
            {
                return time.getString();
            }
        }

        return null;
    }
}