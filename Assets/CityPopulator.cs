using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class CityPopulator : MonoBehaviour {

    public List<GameObject> bgTiles;
    public List<GameObject> objects;

    private bool loadStart;

    // Use this for initialization
    void Start()
    {
        loadStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!loadStart && NetworkHandler.networkHandler.IsConnected())
        {
            loadStart = true;
            StartCoroutine("LoadBG");
        }
    }

    IEnumerator LoadBG()
    {
        GameHandler.networkHandler.GetCurrentCity();
        yield return new WaitUntil(() => GameHandler.networkHandler.HasCity());
        JSONObject cityData = GameHandler.networkHandler.ReadCity();

        int index = 0;
        foreach (string s in cityData["bgData"].AsArray.Values)
        {
            int frame = int.Parse(s);

            int x = index % cityData["sizeX"].AsInt;
            int y = Mathf.FloorToInt(index / cityData["sizeX"].AsInt);

            GameObject tile = Instantiate(bgTiles[frame]);
            tile.transform.position =
                new Vector2(x - (cityData["sizeX"].AsInt / 2) + 0.5f, y - (cityData["sizeY"].AsInt / 2) + 0.5f);

            index++;
        }

        index = 0;
        foreach (string s in cityData["objectData"].AsArray.Values)
        {
            int frame = int.Parse(s);

            int x = index % cityData["sizeX"].AsInt;
            int y = Mathf.FloorToInt(index / cityData["sizeX"].AsInt);

            if(frame > 0)
            {
                GameObject tile = Instantiate(objects[frame - 1]);
                tile.transform.position =
                    new Vector2(x - (cityData["sizeX"].AsInt / 2) + 0.5f, y - (cityData["sizeY"].AsInt / 2) + 0.5f);
            }
            
            index++;
        }
    }

}
