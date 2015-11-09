using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Happyhour.Model
{
    class PubDay
    {
        public ClockTime open;
        public ClockTime close;
        public ClockTime happyhourFrom;
        public ClockTime happyhourTo;
        int day;
        public bool isClosed;
        public string timeString { get; set; }
        public string happyhourString { get; set; }

        public PubDay(int day)
        {
            this.day = day;
        }

        public PubDay(ClockTime open, ClockTime close, int day, ClockTime happyhourFrom, ClockTime happyhourTo)
        {
            this.open = open;
            this.close = close;
            this.day = day;
            this.happyhourFrom = happyhourFrom;
            this.happyhourTo = happyhourTo;
        }

        public bool isNowOpen()
        {
            TimeSpan start = new TimeSpan(open.hour, open.minutes, 0);
            TimeSpan end = new TimeSpan(close.hour, close.minutes, 0);
            TimeSpan now = DateTime.Now.TimeOfDay;

            if (start < end)
                return start <= now && now <= end;

            return !(end < now && now < start);
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

        public string getDayShort()
        {
            switch (day)
            {
                case 0:
                    return "Mon";
                case 1:
                    return "Tue";
                case 2:
                    return "Wed";
                case 3:
                    return "Thu";
                case 4:
                    return "Fri";
                case 5:
                    return "Sat";
                case 6:
                    return "Sun";
            }
            return "";
        }

        public void setTimeString()
        {
            if (isClosed)
                timeString = getDayShort() + ": Closed";
            else
                timeString = getDayShort() + ": " + open.getString() + " - " + close.getString();
        }

        public void setHappyhourString()
        {
            if (isClosed)
                happyhourString = getDayShort() + ": Closed";
            else
                happyhourString = getDayShort() + ": " + happyhourFrom.getString() + " - " + happyhourTo.getString();
        }
    }
}
