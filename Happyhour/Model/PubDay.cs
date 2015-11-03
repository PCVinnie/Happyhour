using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Happyhour.Model
{
    public class PubDay
    {
        public ClockTime open;
        public ClockTime close;
        public ClockTime happyhourFrom;
        public ClockTime happyhourTo;
        int day;

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
    }
}
