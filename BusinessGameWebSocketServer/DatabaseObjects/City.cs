using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessGameWebSocketServer.DatabaseObjects
{
    class City
    {
        public Dictionary<string, Property> properties;

        public City()
        {
            properties = new Dictionary<string, Property>();
        }
    }
}
