using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using System;

public class LumiNetworkManager : NetworkManager
{
    [SerializeField] List<LumiNetworkPlayer> players = new List<LumiNetworkPlayer>();
    [SerializeField] GameManager gameManagerPrefab;

    bool isGameInProgress = false;
    string mapName = "Scene_Arena";

    public List<LumiNetworkPlayer> Players { get { return players; } }
    public bool IsGameInProgress { get { return isGameInProgress; } }

    // Avoid overriding built in method OnClientConnect by reversing the naming scheme
    public static event Action ClientOnConnected;
    public static event Action ClientOnDisconnected;

    #region Server
    public override void OnStartServer()
    {
        base.OnStartServer();

        LumiNetworkPlayer.ServerOnPlayerDefeat += ServerHandlePlayerDefeat;
    }

    public override void OnStopServer()
    {
        base.OnStopServer();

        players.Clear();

        isGameInProgress = false;

        LumiNetworkPlayer.ServerOnPlayerDefeat -= ServerHandlePlayerDefeat;
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        LumiNetworkPlayer player = conn.identity.GetComponent<LumiNetworkPlayer>();
        player.SetPlayerName($"Player {players.Count + 1}"); // Initial player count starts from 0
        player.SetPlayerColor();

        players.Add(player);

        if (players.Count < 2)
        {
            player.SetPartyOwner(true);
        }
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        if (!isGameInProgress) { return; }

        conn.Disconnect();
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        

        LumiNetworkPlayer disconnectedPlayer = conn.identity.GetComponent<LumiNetworkPlayer>();
        players.Remove(disconnectedPlayer);
        
        ClientOnDisconnected?.Invoke();

        base.OnServerDisconnect(conn);
    }

    public override void OnServerSceneChanged(string newSceneName)
    {
        base.OnServerSceneChanged(newSceneName);

        if (IsMapScene())
        {
            
            GameManager instantiatedGameManager = Instantiate(gameManagerPrefab);
            NetworkServer.Spawn(instantiatedGameManager.gameObject);
        }
    }

    [Server]
    bool IsMapScene()
    {
        if (SceneManager.GetActiveScene().name.StartsWith(mapName))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    [Server]
    void ServerHandlePlayerDefeat(LumiNetworkPlayer player)
    {
        players.Remove(player);
    }

    public void StartGame()
    {
        if (players.Count >= 2)
        {
            ServerChangeScene(mapName);
            isGameInProgress = true;
        }
        
    }

    #endregion

    #region Client
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        ClientOnConnected?.Invoke();
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        ClientOnDisconnected?.Invoke();
    }

    public override void OnStopClient()
    {
        base.OnStopClient();

        players.Clear();
    }

    #endregion
}
