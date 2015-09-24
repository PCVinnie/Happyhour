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
        private const string dataFileName = "PubAppData.txt";
        public XMLFileHandler()
        {
            List<LocationData> test = readPubXMLFile();
            LocationData l = new LocationData();
            l.name = "BierCafe";
            l.city = "Rotterdam";
            
            test = checkIfListContains(test, l);

            LocationData l2 = new LocationData();
            l2.name = "BierCafe";
            l2.city = "Delft";
            test = checkIfListContains(test, l2); ;

            writePubXMLFile(test);

            test = readPubXMLFile();
        }

        private List<LocationData> checkIfListContains(List<LocationData> list, LocationData data)
        {
            Boolean isIn = false;
            foreach (LocationData ld in list)
            {
                if (ld.name == data.name && ld.city != data.city)
                    isIn = false;
                else if (ld.name == data.name)
                {
                    isIn = true;
                    break;
                }
            }
            if (!isIn)
                list.Add(data);

            return list;
        }

        public void writePubXMLFile(List<LocationData> locations)
        {

            //XDocument pubdoc = XDocument.Load("Assets/XML/PubsInformation.xml");
            //List<XElement> pubs = pubdoc.Root.Elements().ToList();

            XDocument doc = new XDocument();
            doc.Add(new XElement("pubs"));

            foreach (LocationData l in locations)
            {
                /*Boolean inIt = true;
                foreach (XElement filepub in pubs)
                {
                    if(filepub.Element("name").Value != l.name || (filepub.Element("name").Value == l.name && filepub.Element("city").Value != l.city))
                    {
                        inIt = false;
                    }
                }
                if(!inIt)
                {*/
                    XElement pub = new XElement("pub");
                    XElement name = new XElement("name", l.name);
                    XElement city = new XElement("city", l.city);
                    pub.Add(name);
                    pub.Add(city);

                    //pubs.Add(pub);
                    doc.Root.Add(pub);
                //}
            }

            //File.WriteAllText("Assets/XML/PubsInformation.xml", doc.ToString());

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
                                Int32.TryParse(element.Value, out location.streetNumber);
                                break;
                            case "POSTCODE":
                                element = XElement.ReadFrom(reader) as XElement;
                                location.postcode = element.Value.ToString();
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
                                Double.TryParse(element.Value.ToString(), out location.rating);
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
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement && reader.Name.ToString().ToUpper() == "PUB")
                        locations.Add(location);
                }
            }
            return locations;
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
