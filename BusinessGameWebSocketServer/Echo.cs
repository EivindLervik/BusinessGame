using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace BusinessGameWebSocketServer
{
    public class Echo : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Send(e.Data);
        }

        protected override void OnOpen()
        {
            Send("Hei!");
        }
    }
}
