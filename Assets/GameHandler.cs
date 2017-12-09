using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

    public static NetworkHandler networkHandler;
    public static SceneHandler sceneHandler;

    private static GameHandler gameHandler;

    private void Start()
    {
        networkHandler = GetComponentInChildren<NetworkHandler>();
        sceneHandler = GetComponentInChildren<SceneHandler>();

        if(gameHandler == null)
        {
            DontDestroyOnLoad(gameObject);
            gameHandler = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
