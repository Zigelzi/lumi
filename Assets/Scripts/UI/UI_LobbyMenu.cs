using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LobbyMenu : MonoBehaviour
{
    [SerializeField] Button startGameButton;

    UI_MainMenu mainMenu;

    void Start()
    {
        mainMenu = FindObjectOfType<UI_MainMenu>();

        LumiNetworkManager.ClientOnConnected += HandleClientConnected;
        LumiNetworkManager.ClientOnDisconnected += HandleClientDisconnected;
        
        LumiNetworkPlayer.AuthorityOnPartyOwnerUpdated += HandlePartyOwnerUpdated;
    }

    void OnDestroy()
    {
        LumiNetworkManager.ClientOnConnected -= HandleClientConnected;
        LumiNetworkManager.ClientOnDisconnected -= HandleClientDisconnected;

        LumiNetworkPlayer.AuthorityOnPartyOwnerUpdated -= HandlePartyOwnerUpdated;
    }

    void HandleClientConnected()
    {
        mainMenu.LobbyMenu.SetActive(true);
    }

    void HandleClientDisconnected()
    {
        mainMenu.LobbyMenu.SetActive(false);
    }

    void HandlePartyOwnerUpdated(bool isPartyOwner)
    {
        if (isPartyOwner)
        {
            startGameButton.gameObject.SetActive(true);
        }
        else
        {
            startGameButton.gameObject.SetActive(false);
        }
    }

    public void StartGame()
    {
        LumiNetworkPlayer partyOwner = NetworkClient.connection.identity.GetComponent<LumiNetworkPlayer>();
        partyOwner.CmdStartGame();
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
