public class ClockTime
{
    public int hour;
    public int minutes;
    public int day;
    public bool closed;

    public ClockTime()
    {
        hour = 0;
        minutes = 0;
        int day = 0;
        bool closed = false;
    }

    public ClockTime(int hour, int minutes, int day, bool closed)
    {
        this.hour = hour;
        this.minutes = minutes;
        this.day = day;
        this.closed = closed;
    }

    public string getDay()
    {
        switch (day)
        {
            case 0:
                return "Monday";
            case 1:
                return "Tuesday";
            case 2:
                return "Wednesday";
            case 3:
                return "Thursday";
            case 4:
                return "Friday";
            case 5:
                return "Saterday";
            case 6:
                return "Sunday";
        }
        return "";
    }

    public string  getString()
    {
        string hourString;
        string minutesString;

        if (hour < 10)
            hourString = "0" + hour.ToString();
        else
            hourString = hour.ToString();

        if (minutes < 10)
            minutesString = "0" + minutes.ToString();
        else
            minutesString = "0" + minutes.ToString();

        return (hourString + ":" + minutesString);
    }
}