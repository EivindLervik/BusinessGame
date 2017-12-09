using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour {

    private void Start()
    {
        
    }

    public void GotoCity()
    {
        SceneManager.LoadSceneAsync("City");
    }

}
