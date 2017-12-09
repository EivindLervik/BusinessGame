using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using WebSocketSharp;
using SimpleJSON;

public class NetworkHandler : MonoBehaviour {

	public string serverURL;

    private WebSocket ws;
    public static NetworkHandler networkHandler;
    private bool connected;

    private List<JSONNode> messageQueue;
    private List<JSONNode> errorQueue;

    private string authenticatedAs;

    void Start()
	{
        if(networkHandler == null)
        {
            networkHandler = this;

            messageQueue = new List<JSONNode>();
            errorQueue = new List<JSONNode>();

            authenticatedAs = null;

            ws = new WebSocket(serverURL);

            ws.OnOpen += (sender, e) =>
            {
                print("WEBSOCKET: Connected - " + e.ToString());
                connected = true;
            };

            ws.OnMessage += (sender, e) =>
            {
                JSONNode data = JSON.Parse(e.Data);
                messageQueue.Add(data);
            };

            ws.OnError += (sender, e) =>
            {
                JSONNode data = JSON.Parse(e.Message);
                errorQueue.Add(data);
            };

            ws.OnClose += (sender, e) =>
            {
                print("WEBSOCKET: Closed - " + e.Reason);
                connected = false;
            };

            ws.Connect();

        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Handle all incoming messages
    private void Update()
    {
        // Is there errors?
        if(errorQueue.Count > 0)
        {
            print("ERROR : " + errorQueue[0].ToString());
            errorQueue.RemoveAt(0);
        }

        // Is there incoming data?
        if(messageQueue.Count > 0)
        {
            JSONObject o = messageQueue[0].AsObject;
            string type = o["type"];

            switch (type)
            {
                case "getPropertyData":
                    propertyData = o["data"].AsObject;
                    break;
                case "createCharacter":
                    if (o["data"]["success"].AsBool)
                    {
                        createdCharacter = "Y";
                    }
                    else
                    {
                        createdCharacter = "N";
                    }
                    break;
                case "login":
                    if (o["data"]["success"].AsBool)
                    {
                        loggedIn = "Y";
                        authenticatedAs = o["data"]["token"].ToString();
                    }
                    else
                    {
                        loggedIn = "N";
                    }
                    break;
            }

            messageQueue.RemoveAt(0);
        }
    }

    public bool IsConnected()
    {
        return connected;
    }









    // PROPERTY DATA //
    private JSONObject propertyData;
    public void GetPropertyData(string city, string address)
    {
        JSONObject o = new JSONObject();
        o.Add("type", "get");
        o.Add("target", "property");
        o.Add("city", city);
        o.Add("address", address);
        ws.Send(o.ToString());
    }
    public bool HasPropertyData()
    {
        return propertyData != null;
    }
    public JSONObject ReadPropertyData()
    {
        JSONObject p = propertyData;
        propertyData = null;
        return p;
    }

    // CREATE CHARACTER //
    private string createdCharacter = "";
    public void CreateCharacter(string username, string password)
    {
        JSONObject o = new JSONObject();
        o.Add("type", "add");
        o.Add("target", "character");
        o.Add("username", username);
        o.Add("password", password);
        ws.Send(o.ToString());
    }
    public bool HasCreatedCharacter()
    {
        return !createdCharacter.Equals("");
    }
    public string ReadCreatedCharacter()
    {
        string c = createdCharacter;
        createdCharacter = "";
        return c;
    }

    // LOG IN //
    private string loggedIn = "";
    public void LogIn(string username, string password)
    {
        JSONObject o = new JSONObject();
        o.Add("type", "auth");
        o.Add("target", "login");
        o.Add("username", username);
        o.Add("password", password);
        ws.Send(o.ToString());
    }
    public bool HasLoggedIn()
    {
        return !loggedIn.Equals("");
    }
    public string ReadLoggedIn()
    {
        string l = loggedIn;
        loggedIn = "";
        return l;
    }
}
