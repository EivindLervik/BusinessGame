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
    private string currentCity;
    private string currentProperty;

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
                    currentProperty = propertyData["name"];
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
                case "createCity":
                    if (o["data"]["success"].AsBool)
                    {
                        createdCity = "Y";
                    }
                    else
                    {
                        createdCity = "N";
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
                case "getCities":
                    cities = new List<string>();
                    foreach(string c in o["data"].AsArray.Values)
                    {
                        cities.Add(c);
                        print(c);
                    }
                    break;
                case "getCity":
                    city = o["data"].AsObject;
                    currentCity = city["name"];
                    print(city.ToString());
                    break;
            }

            messageQueue.RemoveAt(0);
        }
    }

    public bool IsConnected()
    {
        return connected;
    }

    public void SetCurrentCity(string city)
    {
        currentCity = city;
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

    // GET CITIES //
    private List<string> cities;
    public void GetCities()
    {
        JSONObject o = new JSONObject();
        o.Add("type", "get");
        o.Add("target", "cities");
        ws.Send(o.ToString());
    }
    public bool HasCities()
    {
        return cities != null;
    }
    public List<string> ReadCities()
    {
        List<string> c = cities;
        cities = null;
        return c;
    }

    // GET CITY //
    private JSONObject city;
    public void GetCurrentCity()
    {
        GetCity(currentCity);
    }
    public void GetCity(string name)
    {
        JSONObject o = new JSONObject();
        o.Add("type", "get");
        o.Add("target", "city");
        o.Add("name", name);
        ws.Send(o.ToString());
    }
    public bool HasCity()
    {
        return city != null;
    }
    public JSONObject ReadCity()
    {
        JSONObject c = city;
        city = null;
        return c;
    }

    // CREATE CITY //
    private string createdCity = "";
    public void CreateCity(string name)
    {
        JSONObject o = new JSONObject();
        o.Add("type", "add");
        o.Add("target", "city");
        o.Add("name", name);
        ws.Send(o.ToString());
    }
    public bool HasCreatedCity()
    {
        return !createdCity.Equals("");
    }
    public string ReadCreatedCity()
    {
        string c = createdCity;
        createdCity = "";
        return c;
    }
}
