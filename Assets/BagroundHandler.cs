using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class BagroundHandler : MonoBehaviour {

    public List<GameObject> bgTiles;

    private bool loadStart;

	// Use this for initialization
	void Start () {
        loadStart = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!loadStart && NetworkHandler.networkHandler.IsConnected())
        {
            loadStart = true;
            StartCoroutine("LoadBG");
        }
	}

    IEnumerator LoadBG()
    {
        NetworkHandler.networkHandler.GetPropertyData("Manger", "Lervikvegen 98");
        yield return new WaitUntil(() => NetworkHandler.networkHandler.HasPropertyData());
        JSONObject propertyData = NetworkHandler.networkHandler.ReadPropertyData();

        int index = 0;
        foreach(string s in propertyData["bgData"].AsArray.Values)
        {
            int frame = int.Parse(s);

            int x = index % propertyData["sizeX"].AsInt;
            int y = Mathf.FloorToInt(index / propertyData["sizeX"].AsInt);

            GameObject tile = Instantiate(bgTiles[frame]);
            tile.transform.position = 
                new Vector2(x - (propertyData["sizeX"].AsInt / 2) + 0.5f, y - (propertyData["sizeY"].AsInt / 2) + 0.5f);

            index++;
        }
    }
}
