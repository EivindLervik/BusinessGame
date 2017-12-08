using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessGameWebSocketServer.DatabaseObjects;
using SimpleJSON;

namespace BusinessGameWebSocketServer
{
    class Database
    {

        public Dictionary<string, JSONObject> cities;

        public Database(bool dummy)
        {
            cities = new Dictionary<string, JSONObject>();
            if (dummy)
            {
                JSONObject c = new JSONObject();
                JSONObject p = new JSONObject();

                p["owner"] = "Eivind";
                p["address"] = "Lervikvegen 98";
                p["sizeX"] = 30;
                p["sizeY"] = 10;
                p["bgData"] = new JSONArray();
                for (int i=0; i<(p["sizeX"] * p["sizeY"]); i++)
                {
                    p["bgData"].Add(0);
                }

                c["name"] = "Manger";
                c["properties"].Add(p["address"], p);

                cities.Add(c["name"], c);
            }
        }

        public JSONObject GetPropertyData(string city, string property)
        {
            return cities[city]["properties"][property].AsObject;
        }
    }
}
