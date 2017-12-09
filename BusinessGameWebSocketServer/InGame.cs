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

            if (type.Equals("get"))
            {
                switch (target)
                {
                    case "property":
                        JSONObject o = new JSONObject();
                        o.Add("type", "getPropertyData");
                        o.Add("data", Program.database.GetPropertyData(data["city"], data["address"]));

                        Send(o.ToString());
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
                        JSONObject o = new JSONObject();

                        o.Add("type", "createCharacter");
                        o.Add("data", Program.database.CreateCharacter(data["username"], data["password"]));

                        Send(o.ToString());
                        break;
                }
            }
            else if (type.Equals("auth"))
            {
                switch (target)
                {
                    case "login":
                        JSONObject o = new JSONObject();

                        o.Add("type", "login");
                        o.Add("data", Program.database.LogIn(data["username"], data["password"]));

                        Send(o.ToString());
                        break;
                }
            }

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
