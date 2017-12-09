using System;
using WebSocketSharp;
using WebSocketSharp.Server;
using SimpleJSON;
using BusinessGameWebSocketServer.SendObjects;
using System.Collections.Generic;

namespace BusinessGameWebSocketServer
{
    class InGame : WebSocketBehavior
    {
        protected override void OnOpen()
        {
            Console.WriteLine("User connected");
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            JSONNode data = JSON.Parse(e.Data);
            string type = data["type"];
            string target = data["target"];

            JSONObject o = new JSONObject();

            if (type.Equals("get"))
            {
                switch (target)
                {
                    case "property":
                        o.Add("type", "getPropertyData");
                        o.Add("data", Program.database.GetPropertyData(data["city"], data["address"]));
                        break;

                    case "cities":
                        o.Add("type", "getCities");
                        o.Add("data", Program.database.GetCities());
                        break;

                    case "city":
                        o.Add("type", "getCity");
                        o.Add("data", Program.database.GetCity(data["name"]));
                        break;
                }
            }
            else if (type.Equals("set"))
            {
                
            }
            else if (type.Equals("add"))
            {
                switch (target)
                {
                    case "character":
                        o.Add("type", "createCharacter");
                        o.Add("data", Program.database.CreateCharacter(data["username"], data["password"]));
                        break;

                    case "city":
                        o.Add("type", "createCity");
                        o.Add("data", Program.database.CreateCity(data["name"]));
                        break;
                }
            }
            else if (type.Equals("auth"))
            {
                switch (target)
                {
                    case "login":
                        o.Add("type", "login");
                        o.Add("data", Program.database.LogIn(data["username"], data["password"]));
                        break;
                }
            }

            Send(o.ToString());

            Program.SaveDatabase();
        }

        protected override void OnError(ErrorEventArgs e)
        {
            
        }

        protected override void OnClose(CloseEventArgs e)
        {
            
        }
    }
}
