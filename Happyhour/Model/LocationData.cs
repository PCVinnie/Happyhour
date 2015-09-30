﻿using System.Collections.Generic;
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
    public string happyHour { get; set; }

    private List<ClockTime> openTimes;
    private List<ClockTime> closeTimes;
    public double rating { get; set; }

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
        happyHour = "";

        position = new BasicGeoposition();
        rating = 0;
        openTimes = new List<ClockTime>();
        closeTimes = new List<ClockTime>();
    }

    public LocationData(string name, string street, string streetNumber, string zipcode, string city, string country, int rating)
    {
        id = -1;
        this.name = name;
        this.street = street;
        this.streetNumber = streetNumber;
        this.zipcode = zipcode;
        this.city = city;
        this.country = country;
        happyHour = "";

        position = new BasicGeoposition();
        this.rating = rating;
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