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
        int day;
        public bool isClosed;
        public Boolean happyhour;

        public PubDay(int day)
        {
            this.day = day;
        }

        public PubDay(ClockTime open, ClockTime close, int day, Boolean happyhour)
        {
            this.open = open;
            this.close = close;
            this.day = day;
            this.happyhour = happyhour;
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
