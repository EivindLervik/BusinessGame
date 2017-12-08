using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJSON;

namespace BusinessGameWebSocketServer.SendObjects
{
    public class Event
    {

        public EventType eventType;

        public Event()
        {

        }

        public Event(EventType eventType)
        {

        }

        public enum EventType
        {
            Message, NewObject, DeleteObject, GetData
        }

        public string ToJSON()
        {
            JSONObject node = new JSONObject();

            node.Add("eventType", eventType.ToString());

            return node.ToString();
        }

    }
}
