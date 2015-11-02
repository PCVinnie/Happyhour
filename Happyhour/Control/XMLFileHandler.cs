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
                    XElement opentimesMonday = new XElement("opentimesMonday", l.openTimes[0]);
                    XElement opentimesTuesday = new XElement("opentimesTuesday", l.openTimes[1]);
                    XElement opentimesWednesday = new XElement("opentimesWednesday", l.openTimes[2]);
                    XElement opentimesThursday = new XElement("opentimesThursday", l.openTimes[3]);
                    XElement opentimesFriday = new XElement("opentimesFriday", l.openTimes[4]);
                    XElement opentimesSaterday = new XElement("opentimesSaterday", l.openTimes[5]);
                    XElement opentimesSunday = new XElement("opentimesSunday", l.openTimes[6]);

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
                    XElement closetimesMonday = new XElement("closetimesMonday", l.closeTimes[0]);
                    XElement closetimesTuesday = new XElement("closetimesTuesday", l.closeTimes[1]);
                    XElement closetimesWednesday = new XElement("closetimesWednesday", l.closeTimes[2]);
                    XElement closetimesThursday = new XElement("closetimesThursday", l.closeTimes[3]);
                    XElement closetimesFriday = new XElement("closetimesFriday", l.closeTimes[4]);
                    XElement closetimesSaterday = new XElement("closetimesSaterday", l.closeTimes[5]);
                    XElement closetimesSunday = new XElement("closetimesSunday", l.closeTimes[6]);

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
                    
                    XElement happyhourMonday = new XElement("happyhourMonday", l.happyhourDays[0]);
                    XElement happyhourTuesday = new XElement("happyhourTuesday", l.happyhourDays[1]);
                    XElement happyhourWednesday = new XElement("happyhourWednesday", l.happyhourDays[2]);
                    XElement happyhourThursday = new XElement("happyhourThursday", l.happyhourDays[3]);
                    XElement happyhourFriday = new XElement("happyhourFriday", l.happyhourDays[4]);
                    XElement happyhourSaterday = new XElement("happyhourSaterday", l.happyhourDays[5]);
                    XElement happyhourSunday = new XElement("happyhourSunday", l.happyhourDays[6]);

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
                                element = XElement.ReadFrom(reader) as XElement;
                                //location.addHappyhourDays(Convert.ToBoolean(element.Value.ToString()));
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

        private void readPubTimes(XmlReader reader, Boolean isOpentime, LocationData location)
        {
            ClockTime time = new ClockTime();
            XElement element;
            string typeTime = "CLOSETIMES";

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
                            time.day = 0;
                            element = XElement.ReadFrom(reader) as XElement;
                            FillPubHoursAndMinutes(time, element);
                            if (isOpentime)
                                location.addOpenTime(time);
                            else
                                location.addCloseTime(time);
                            break;
                        case "TUESDAY":
                            time.day = 1;
                            element = XElement.ReadFrom(reader) as XElement;
                            FillPubHoursAndMinutes(time, element);
                            if (isOpentime)
                                location.addOpenTime(time);
                            else
                                location.addCloseTime(time);
                            break;
                        case "WEDNESDAY":
                            time.day = 2;
                            element = XElement.ReadFrom(reader) as XElement;
                            FillPubHoursAndMinutes(time, element);
                            if (isOpentime)
                                location.addOpenTime(time);
                            else
                                location.addCloseTime(time);
                            break;
                        case "THURSDAY":
                            time.day = 3;
                            element = XElement.ReadFrom(reader) as XElement;
                            FillPubHoursAndMinutes(time, element);
                            break;
                        case "FRIDAY":
                            time.day = 4;
                            element = XElement.ReadFrom(reader) as XElement;
                            FillPubHoursAndMinutes(time, element);
                            if (isOpentime)
                                location.addOpenTime(time);
                            else
                                location.addCloseTime(time);
                            break;
                        case "SATERDAY":
                            time.day = 5;
                            element = XElement.ReadFrom(reader) as XElement;
                            FillPubHoursAndMinutes(time, element);
                            if (isOpentime)
                                location.addOpenTime(time);
                            else
                                location.addCloseTime(time);
                            break;
                        case "SUNDAY":
                            time.day = 6;
                            element = XElement.ReadFrom(reader) as XElement;
                            FillPubHoursAndMinutes(time, element);
                            if (isOpentime)
                                location.addOpenTime(time);
                            else
                                location.addCloseTime(time);
                            break;

                    }
                }
            }
        }

        public void readHappyhourDays()
        {

        }

        private void FillPubHoursAndMinutes(ClockTime time, XElement element)
        {
            string hoursminutes = element.Value.ToString();
            if (hoursminutes == "closed")
                time.closed = true;
            else
            {
                Int32.TryParse((hoursminutes[0].ToString() + hoursminutes[1].ToString()), out time.hour);
                Int32.TryParse((hoursminutes[2].ToString() + hoursminutes[3].ToString()), out time.minutes);
            }
        }
    }
}
