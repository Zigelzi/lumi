using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LobbyMenu : MonoBehaviour
{
    UI_MainMenu mainMenu;
    void Start()
    {
        mainMenu = FindObjectOfType<UI_MainMenu>();

        LumiNetworkManager.ClientOnConnected += HandleClientConnected;
        LumiNetworkManager.ClientOnDisconnected += HandleClientDisconnected;
    }

    void OnDestroy()
    {
        LumiNetworkManager.ClientOnConnected -= HandleClientConnected;
        LumiNetworkManager.ClientOnDisconnected -= HandleClientDisconnected;
    }

    void HandleClientConnected()
    {
        mainMenu.LobbyMenu.SetActive(true);
    }

    void HandleClientDisconnected()
    {
        mainMenu.LobbyMenu.SetActive(false);
    }

    public void DisconnectLobby()
    {
        // If player is host
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopHost();
        }
        else
        {
            NetworkManager.singleton.StopClient();
        }
        
    }
}
