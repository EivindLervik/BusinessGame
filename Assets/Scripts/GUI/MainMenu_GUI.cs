using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_GUI : MonoBehaviour {

    public GameObject mainMenu, logIn, createCharacter;

    public InputField newUsername, newPassword;
    public InputField username, password;

    private void Start()
    {
        OpenMainMenu();
    }

    public void OpenMainMenu()
    {
        DisableAllMenues();
        mainMenu.SetActive(true);
    }

    public void OpenLogIn()
    {
        DisableAllMenues();
        logIn.SetActive(true);
    }

    public void OpenCreateCharacter()
    {
        DisableAllMenues();
        createCharacter.SetActive(true);
    }

    public void DisableAllMenues()
    {
        mainMenu.SetActive(false);
        logIn.SetActive(false);
        createCharacter.SetActive(false);
    }

    public void LogIn()
    {
        StartCoroutine("LogIn_C");
    }
    private IEnumerator LogIn_C()
    {
        NetworkHandler.networkHandler.LogIn(username.text, password.text);
        yield return new WaitUntil(() => NetworkHandler.networkHandler.HasLoggedIn());
        print(NetworkHandler.networkHandler.ReadLoggedIn());

    }

    public void CreateCharacter()
    {
        StartCoroutine("CreateCharacter_C");
    }
    private IEnumerator CreateCharacter_C()
    {
        NetworkHandler.networkHandler.CreateCharacter(newUsername.text, newPassword.text);
        yield return new WaitUntil(() => NetworkHandler.networkHandler.HasCreatedCharacter());
        if (NetworkHandler.networkHandler.ReadCreatedCharacter().Equals("Y"))
        {
            OpenLogIn();
        }
        else
        {
            // Creation failed
        }

    }
}
