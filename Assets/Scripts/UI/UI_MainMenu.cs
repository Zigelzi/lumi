using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class UI_MainMenu : MonoBehaviour
{
    [Header("Menu items")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject lobbyMenuParent;
    [SerializeField] GameObject lobbyMenu;
    [SerializeField] GameObject joinMenu;

    [Header("Navigation")]
    [SerializeField] GameObject currentMenu;
    [SerializeField] GameObject previousMenu;

    string mainMenuName = "Menu_MainMenu";
    string lobbyMenuParentName = "Menu_LobbyParent";
    string lobbyMenuName = "Menu_Lobby";
    string joinMenuName = "Menu_JoinGame";

    public GameObject MainMenu { get { return mainMenu; } }
    public GameObject LobbyMenu { get { return lobbyMenu; } }
    public GameObject JoinMenu { get { return joinMenu; } }

    // Start is called before the first frame update
    void Start()
    {
        mainMenu = transform.Find(mainMenuName).gameObject;
        lobbyMenuParent = transform.Find(lobbyMenuParentName).gameObject;
        lobbyMenu = lobbyMenuParent.transform.Find(lobbyMenuName).gameObject;
        joinMenu = transform.Find(joinMenuName).gameObject;

        mainMenu.SetActive(true);
        currentMenu = mainMenu;
        previousMenu = null;

        lobbyMenu.SetActive(false);
        joinMenu.SetActive(false);
    }

    public void HostGame()
    {
        mainMenu.SetActive(false);

        previousMenu = currentMenu;
        currentMenu = lobbyMenu;

        NetworkManager.singleton.StartHost();
    }

    public void JoinGame()
    {
        mainMenu.SetActive(false);
        joinMenu.SetActive(true);

        previousMenu = currentMenu;
        currentMenu = joinMenu;
    }

    public void GoMainMenu()
    {
        mainMenu.SetActive(true);
        currentMenu.SetActive(false);

        previousMenu = currentMenu;
        currentMenu = mainMenu;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
