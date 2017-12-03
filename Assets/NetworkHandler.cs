using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using WebSocketSharp;

public class NetworkHandler : MonoBehaviour {

	public string serverURL;

    private WebSocket ws;

    void Start()
	{
        ws = new WebSocket(serverURL);

        ws.OnOpen += (sender, e) =>
        {
            print("WEBSOCKET: Connected - " + e.ToString());
            ws.Send("Hei");
        };

        ws.OnMessage += (sender, e) =>
        {
            print("WEBSOCKET: Data - " + e.Data);
        };

        ws.OnError += (sender, e) =>
        {
            print("WEBSOCKET: Error - " + e.Message);
        };

        ws.OnClose += (sender, e) =>
        {
            print("WEBSOCKET: Closed - " + e.Reason);
        };

        ws.Connect();
    }
}
