using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Happyhour.Control
{

    class LocationHandler
    {
        private static LocationHandler instance;
        public List<LocationData> pubList;
        private XMLFileHandler xmlFileHandler;
        private LocationHandler()
        {
            xmlFileHandler = new XMLFileHandler();
            pubList = xmlFileHandler.readPubXMLFile();
        }

        public static LocationHandler Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new LocationHandler();
                }
                return instance;
            }
        }

        public void addPub(LocationData pub)
        {
            pub.id = pubList[pubList.Count -1].id + 1;
            checkIfListContains(pubList, pub);
            xmlFileHandler.writePubXMLFile(pubList);
        }
        public void setPub(LocationData pub)
        {
            foreach (LocationData p in pubList)
            {
                if(p.id == pub.id)
                {
                    int index = pubList.IndexOf(p);
                    pubList[index] = pub;
                    break;
                }
            }

            xmlFileHandler.writePubXMLFile(pubList);
        }

        public void deletePub(int id)
        {
            pubList.RemoveAt(id);
            xmlFileHandler.writePubXMLFile(pubList);
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


    }
}
