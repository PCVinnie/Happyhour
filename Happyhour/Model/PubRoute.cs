using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Happyhour.Model
{
    class PubRoute
    {
        public List<LocationData> pubs { get; }
        public string name { get; set; }

        public PubRoute()
        {
            pubs = new List<LocationData>();
            name = "";
        }

        public PubRoute(string name, List<LocationData> pubs)
        {
            this.name = name;
            this.pubs = pubs;
        }

        public void addPubToList(LocationData data)
        {
            pubs.Add(data);
        }
    }
}
