public class ClockTime
{
    public int hour;
    public int minutes;

    public ClockTime()
    {
        hour = 0;
        minutes = 0;
    }

    public ClockTime(int hour, int minutes, int day, bool closed)
    {
        this.hour = hour;
        this.minutes = minutes;
    }

    public string getString()
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