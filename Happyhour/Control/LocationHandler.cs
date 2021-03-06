﻿using Happyhour.Model;
using System;
using System.Collections.Generic;

namespace Happyhour.Control
{

    class LocationHandler
    {
        private static LocationHandler instance;
        public List<LocationData> pubList;
        public List<PubRoute> routeList;
        private XMLFileHandler xmlFileHandler;
        private LocationHandler()
        {
            xmlFileHandler = new XMLFileHandler();
            pubList = new List<LocationData>();
            LocationData d = new LocationData();
            pubList = xmlFileHandler.readPubXMLFile();
            routeList = xmlFileHandler.readRouteXMLFile(pubList);

            foreach(LocationData location in pubList)
            {
                location.setPubdayTimes();
            }
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

        public void addRoute(PubRoute route)
        {
            routeList.Add(route);
            xmlFileHandler.writeRouteXMLFile(routeList);
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

        public LocationData getLocationdataById(int id)
        {
            foreach(LocationData data in pubList)
            {
                if (data.id == id)
                    return data;
            }
            return null;
        }
    }
}
