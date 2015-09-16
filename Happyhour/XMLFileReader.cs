using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Happyhour
{
    class XMLFileReader
    {
        public XMLFileReader()
        {
            List<LocationData> test = readXMLFile();
        }

        public List<LocationData> readXMLFile()
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
                                location.place = element.Value.ToString();
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
                                element = XElement.ReadFrom(reader) as XElement;
                                
                                break;
                            case "CLOSETIMES":
                                element = XElement.ReadFrom(reader) as XElement;
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

        private void readTimes(XmlReader reader)
        {

        }
    }
}
