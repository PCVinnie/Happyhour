using Happyhour.Control;
using Happyhour.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Windows.Storage;

namespace Happyhour
{
    class XMLFileHandler
    {

        public void writePubXMLFile(List<LocationData> locations)
        {

            XDocument doc = new XDocument();
            doc.Add(new XElement("pubs"));

            foreach (LocationData l in locations)
            {
                XElement pub = new XElement("pub");
                XElement id = new XElement("id", l.id);
                XElement name = new XElement("name", l.name);
                XElement street = new XElement("street", l.street);
                XElement streetNumber = new XElement("streetNumber", l.streetNumber);
                XElement zipcode = new XElement("zipcode", l.zipcode);
                XElement city = new XElement("city", l.city);
                XElement country = new XElement("country", l.country);
                XElement rating = new XElement("rating", l.rating);
  
                
                XElement opentimes = new XElement("opentimes");
                if (l.openTimes.Count > 0)
                {
                    XElement opentimesMonday = new XElement("Monday", l.openTimes[0]);
                    XElement opentimesTuesday = new XElement("Tuesday", l.openTimes[1]);
                    XElement opentimesWednesday = new XElement("Wednesday", l.openTimes[2]);
                    XElement opentimesThursday = new XElement("Thursday", l.openTimes[3]);
                    XElement opentimesFriday = new XElement("Friday", l.openTimes[4]);
                    XElement opentimesSaterday = new XElement("Saterday", l.openTimes[5]);
                    XElement opentimesSunday = new XElement("Sunday", l.openTimes[6]);

                    opentimes.Add(opentimesMonday);
                    opentimes.Add(opentimesTuesday);
                    opentimes.Add(opentimesWednesday);
                    opentimes.Add(opentimesThursday);
                    opentimes.Add(opentimesFriday);
                    opentimes.Add(opentimesSaterday);
                    opentimes.Add(opentimesSunday);
                }

                XElement closetimes = new XElement("closetimes");
                if (l.closeTimes.Count > 0)
                {
                    XElement closetimesMonday = new XElement("Monday", l.closeTimes[0]);
                    XElement closetimesTuesday = new XElement("Tuesday", l.closeTimes[1]);
                    XElement closetimesWednesday = new XElement("Wednesday", l.closeTimes[2]);
                    XElement closetimesThursday = new XElement("Thursday", l.closeTimes[3]);
                    XElement closetimesFriday = new XElement("Friday", l.closeTimes[4]);
                    XElement closetimesSaterday = new XElement("Saterday", l.closeTimes[5]);
                    XElement closetimesSunday = new XElement("Sunday", l.closeTimes[6]);

                    closetimes.Add(closetimesMonday);
                    closetimes.Add(closetimesTuesday);
                    closetimes.Add(closetimesWednesday);
                    closetimes.Add(closetimesThursday);
                    closetimes.Add(closetimesFriday);
                    closetimes.Add(closetimesSaterday);
                    closetimes.Add(closetimesSunday);
                }
                
                XElement position = new XElement("position");
                XElement longitude = new XElement("longitude", l.longitude);
                XElement latitude = new XElement("latitude", l.latitude);

                XElement happyhourDays = new XElement("happyhourDays");
                if (l.closeTimes.Count > 0)
                {
                    XElement happyhourMonday = new XElement("Monday", l.happyhourDays[0]);
                    XElement happyhourTuesday = new XElement("Tuesday", l.happyhourDays[1]);
                    XElement happyhourWednesday = new XElement("Wednesday", l.happyhourDays[2]);
                    XElement happyhourThursday = new XElement("Thursday", l.happyhourDays[3]);
                    XElement happyhourFriday = new XElement("Friday", l.happyhourDays[4]);
                    XElement happyhourSaterday = new XElement("Saterday", l.happyhourDays[5]);
                    XElement happyhourSunday = new XElement("Sunday", l.happyhourDays[6]);

                    happyhourDays.Add(happyhourMonday);
                    happyhourDays.Add(happyhourTuesday);
                    happyhourDays.Add(happyhourWednesday);
                    happyhourDays.Add(happyhourThursday);
                    happyhourDays.Add(happyhourSaterday);
                    happyhourDays.Add(happyhourSunday);
                }
                
                pub.Add(id);
                pub.Add(name);
                pub.Add(street);
                pub.Add(streetNumber);
                pub.Add(zipcode);
                pub.Add(city);
                pub.Add(country);
                pub.Add(rating);
                pub.Add(opentimes);

                pub.Add(closetimes);
                

                position.Add(longitude);
                position.Add(latitude);
                pub.Add(position);

                pub.Add(happyhourDays);

                doc.Root.Add(pub);
            }

            File.WriteAllText("Assets/XML/PubsInformation.xml", doc.ToString());

            Debug.WriteLine(File.ReadAllText("Assets/XML/PubsInformation.xml"));
        }

        public List<LocationData> readPubXMLFile()
        {
            XElement element;
            List<LocationData> locations = new List<LocationData>();
            LocationData location = null;

            using (XmlReader reader = XmlReader.Create("Assets/XML/PubsInformation.xml"))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToString().ToUpper())
                        {
                            case "PUB":
                                location = new LocationData();
                                break;
                            case "ID":
                                element = XElement.ReadFrom(reader) as XElement;
                                location.id = int.Parse(element.Value);
                                break;
                            case "NAME":
                                element = XElement.ReadFrom(reader) as XElement;
                                location.name = element.Value.ToString();
                                break;
                            case "STREET":
                                element = XElement.ReadFrom(reader) as XElement;
                                location.street = element.Value.ToString();
                                break;
                            case "STREETNUMBER":
                                element = XElement.ReadFrom(reader) as XElement;
                                location.streetNumber = element.Value;
                                break;
                            case "ZIPCODE":
                                element = XElement.ReadFrom(reader) as XElement;
                                location.zipcode = element.Value.ToString();
                                break;
                            case "CITY":
                                element = XElement.ReadFrom(reader) as XElement;
                                location.city = element.Value.ToString();
                                break;
                            case "COUNTRY":
                                element = XElement.ReadFrom(reader) as XElement;
                                location.country = element.Value.ToString();
                                break;
                            case "RATING":
                                element = XElement.ReadFrom(reader) as XElement;
                                int rating;
                                int.TryParse(element.Value.ToString(), out rating);
                                location.rating = rating;
                                break;
                            case "OPENTIMES":
                                readPubTimes(reader, true, location);
                                break;
                            case "CLOSETIMES":
                                readPubTimes(reader, false, location);
                                break;
                            case "LONGITUDE":
                                element = XElement.ReadFrom(reader) as XElement;
                                Double.TryParse(element.Value.ToString(), out location.position.Longitude);
                                break;
                            case "LATITUDE":
                                element = XElement.ReadFrom(reader) as XElement;
                                Double.TryParse(element.Value.ToString(), out location.position.Latitude);
                                break;
                            case "HAPPYHOURDAYS":
                                //element = XElement.ReadFrom(reader) as XElement;
                                readHappyhours(reader, location);
                                break;
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement && reader.Name.ToString().ToUpper() == "PUB")
                        locations.Add(location);
                }
            }
            return locations;
        }

        public void writeRouteXMLFile(List<PubRoute> routes)
        {

            XDocument doc = new XDocument();
            doc.Add(new XElement("routes"));

            foreach (PubRoute p in routes)
            {
                XElement route = new XElement("route");
                XElement name = new XElement("name", p.name);
                route.Add(name);

                foreach (LocationData data in p.pubs)
                {
                    XElement pub = new XElement("pub");
                    XElement id = new XElement("id", data.id);

                    pub.Add(id);
                    route.Add(pub);
                }

                doc.Root.Add(route);
            }

            File.WriteAllText("Assets/XML/PubRoutes.xml", doc.ToString());

            Debug.WriteLine(File.ReadAllText("Assets/XML/PubRoutes.xml"));
        }

        public List<PubRoute> readRouteXMLFile(List<LocationData> pubList)
        {
            if (pubList.Count != 0)
            {
                XElement element;
                List<PubRoute> routes = new List<PubRoute>();
                PubRoute route = null;
                LocationData pubdata = new LocationData();

                using (XmlReader reader = XmlReader.Create("Assets/XML/PubRoutes.xml"))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name.ToString().ToUpper())
                            {
                                case "ROUTE":
                                    route = new PubRoute();
                                    break;
                                case "NAME":
                                    element = XElement.ReadFrom(reader) as XElement;
                                    route.name = element.Value.ToString();
                                    break;
                                case "PUB":
                                    pubdata = new LocationData();
                                    break;
                                case "ID":
                                    element = XElement.ReadFrom(reader) as XElement;
                                    int id = -1;
                                    int.TryParse(element.Value.ToString(), out id);
                                    foreach (LocationData data in pubList)
                                    {
                                        if (data.id == id)
                                        {
                                            pubdata = data;
                                            break;
                                        }
                                    }
                                    route.addPubToList(pubdata);
                                    break;
                            }
                        }
                        else if (reader.NodeType == XmlNodeType.EndElement && reader.Name.ToString().ToUpper() == "ROUTE")
                            routes.Add(route);
                    }
                }
                return routes;
            }
            else
                return null;
        }

        private void readHappyhours(XmlReader reader, LocationData location)
        {
            XElement element;
            bool happyhour;
            while (reader.Read() && reader.NodeType != XmlNodeType.EndElement)
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name.ToString().ToUpper())
                    {
                        case "MONDAY":
                            element = XElement.ReadFrom(reader) as XElement;
                            bool.TryParse(element.Value.ToString(), out happyhour);
                            location.pubdays[0].happyhour = happyhour;
                            break;
                        case "TUESDAY":
                            element = XElement.ReadFrom(reader) as XElement;
                            bool.TryParse(element.Value.ToString(), out happyhour);
                            location.pubdays[1].happyhour = happyhour;
                            break;
                        case "WEDNESDAY":
                            element = XElement.ReadFrom(reader) as XElement;
                            bool.TryParse(element.Value.ToString(), out happyhour);
                            location.pubdays[2].happyhour = happyhour;
                            break;
                        case "THURSDAY":
                            element = XElement.ReadFrom(reader) as XElement;
                            bool.TryParse(element.Value.ToString(), out happyhour);
                            location.pubdays[3].happyhour = happyhour;
                            break;
                        case "FRIDAY":
                            element = XElement.ReadFrom(reader) as XElement;
                            bool.TryParse(element.Value.ToString(), out happyhour);
                            location.pubdays[4].happyhour = happyhour;
                            break;
                        case "SATERDAY":
                            element = XElement.ReadFrom(reader) as XElement;
                            bool.TryParse(element.Value.ToString(), out happyhour);
                            location.pubdays[5].happyhour = happyhour;
                            break;
                        case "SUNDAY":
                            element = XElement.ReadFrom(reader) as XElement;
                            bool.TryParse(element.Value.ToString(), out happyhour);
                            location.pubdays[6].happyhour = happyhour;
                            break;

                    }
                }
            }
        }

        private void readPubTimes(XmlReader reader, Boolean isOpentime, LocationData location)
        {
            ClockTime time = new ClockTime();
            XElement element;
            string typeTime = "CLOSETIMES";
            int day;

            if (isOpentime)
                typeTime = "OPENTIMES";

            while (reader.Read() && reader.NodeType != XmlNodeType.EndElement && reader.Name.ToString().ToUpper() != typeTime)
            {
                if (reader.IsStartElement())
                {
                    time = new ClockTime();
                    switch (reader.Name.ToString().ToUpper())
                    {
                        case "MONDAY":
                            day = 0;
                            element = XElement.ReadFrom(reader) as XElement;
                            location.pubdays[day].isClosed = FillPubHoursAndMinutes(time, element);
                            if (isOpentime)
                                location.pubdays[day].open = time;
                                //location.addOpenTime(time);
                            else
                                //location.addCloseTime(time);
                                location.pubdays[day].close = time;
                            break;
                        case "TUESDAY":
                            day = 1;
                            element = XElement.ReadFrom(reader) as XElement;
                            location.pubdays[day].isClosed = FillPubHoursAndMinutes(time, element);
                            if (isOpentime)
                                location.pubdays[day].open = time;
                            else
                                location.pubdays[day].close = time;
                            break;
                        case "WEDNESDAY":
                            day = 2;
                            element = XElement.ReadFrom(reader) as XElement;
                            location.pubdays[day].isClosed = FillPubHoursAndMinutes(time, element);
                            if (isOpentime)
                                location.pubdays[day].open = time;
                            else
                                location.pubdays[day].close = time;
                            break;
                        case "THURSDAY":
                            day = 3;
                            element = XElement.ReadFrom(reader) as XElement;
                            location.pubdays[day].isClosed = FillPubHoursAndMinutes(time, element);
                            if (isOpentime)
                                location.pubdays[day].open = time;
                            else
                                location.pubdays[day].close = time;
                            break;
                        case "FRIDAY":
                            day = 4;
                            element = XElement.ReadFrom(reader) as XElement;
                            location.pubdays[day].isClosed = FillPubHoursAndMinutes(time, element);
                            if (isOpentime)
                                location.pubdays[day].open = time;
                            else
                                location.pubdays[day].close = time;
                            break;
                        case "SATERDAY":
                            day = 5;
                            element = XElement.ReadFrom(reader) as XElement;
                            location.pubdays[day].isClosed = FillPubHoursAndMinutes(time, element);
                            if (isOpentime)
                                location.pubdays[day].open = time;
                            else
                                location.pubdays[day].close = time;
                            break;
                        case "SUNDAY":
                            day = 6;
                            element = XElement.ReadFrom(reader) as XElement;
                            location.pubdays[day].isClosed = FillPubHoursAndMinutes(time, element);
                            if (isOpentime)
                                location.pubdays[day].open = time;
                            else
                                location.pubdays[day].close = time;
                            break;

                    }
                }
            }
        }

        private bool FillPubHoursAndMinutes(ClockTime time, XElement element)
        {
            bool closed = false;
            string hoursminutes = element.Value.ToString();
            if (hoursminutes == "closed")
            {
                closed = true;
            }
            else
            {
                Int32.TryParse((hoursminutes[0].ToString() + hoursminutes[1].ToString()), out time.hour);
                Int32.TryParse((hoursminutes[2].ToString() + hoursminutes[3].ToString()), out time.minutes);
            }

            return closed;
        }
    }
}
