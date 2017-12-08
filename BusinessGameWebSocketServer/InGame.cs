using System;
using WebSocketSharp;
using WebSocketSharp.Server;
using SimpleJSON;
using BusinessGameWebSocketServer.SendObjects;

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
        }

        protected override void OnError(ErrorEventArgs e)
        {
            
        }

        protected override void OnClose(CloseEventArgs e)
        {
            
        }
    }
}
