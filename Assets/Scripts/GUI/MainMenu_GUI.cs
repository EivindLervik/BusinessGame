using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_GUI : MonoBehaviour {

    public GameObject mainMenu, logIn, createCharacter, chooseCity, createCity;

    public InputField newUsername, newPassword;
    public InputField username, password;

    public Dropdown cityChooser;
    public InputField cityName;

    private List<string> loadedCities;

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

    public void OpenChooseCity()
    {
        DisableAllMenues();
        chooseCity.SetActive(true);
        cityChooser.ClearOptions();
        cityChooser.AddOptions(loadedCities);
    }

    public void OpenCreateCity()
    {
        DisableAllMenues();
        createCity.SetActive(true);
    }

    public void DisableAllMenues()
    {
        mainMenu.SetActive(false);
        logIn.SetActive(false);
        createCharacter.SetActive(false);
        chooseCity.SetActive(false);
        createCity.SetActive(false);
    }

    public void LogIn()
    {
        StartCoroutine("LogIn_C");
    }
    private IEnumerator LogIn_C()
    {
        GameHandler.networkHandler.LogIn(username.text, password.text);
        yield return new WaitUntil(() => GameHandler.networkHandler.HasLoggedIn());

        if (GameHandler.networkHandler.ReadLoggedIn().Equals("Y"))
        {
            GameHandler.networkHandler.GetCities();
            yield return new WaitUntil(() => GameHandler.networkHandler.HasCities());
            loadedCities = GameHandler.networkHandler.ReadCities();
            OpenChooseCity();
        }

    }

    public void CreateCharacter()
    {
        StartCoroutine("CreateCharacter_C");
    }
    private IEnumerator CreateCharacter_C()
    {
        GameHandler.networkHandler.CreateCharacter(newUsername.text, newPassword.text);
        yield return new WaitUntil(() => GameHandler.networkHandler.HasCreatedCharacter());
        if (GameHandler.networkHandler.ReadCreatedCharacter().Equals("Y"))
        {
            OpenLogIn();
        }
        else
        {
            // Creation failed
        }

    }

    public void ChooseCity()
    {
        GameHandler.networkHandler.SetCurrentCity(cityChooser.options[cityChooser.value].text);
        GameHandler.sceneHandler.GotoCity();
    }

    public void CreateCity()
    {
        StartCoroutine("CreateCity_C");
    }
    private IEnumerator CreateCity_C()
    {
        GameHandler.networkHandler.CreateCity(cityName.text);
        yield return new WaitUntil(() => GameHandler.networkHandler.HasCreatedCity());
        if (GameHandler.networkHandler.ReadCreatedCity().Equals("Y"))
        {
            GameHandler.networkHandler.GetCities();
            yield return new WaitUntil(() => GameHandler.networkHandler.HasCities());
            loadedCities = GameHandler.networkHandler.ReadCities();
            OpenChooseCity();
        }
        else
        {
            // Creation failed
        }

    }
}
