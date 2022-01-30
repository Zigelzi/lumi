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

    string mapName = "Scene_Arena";

    public List<LumiNetworkPlayer> Players { get { return players; } }

    // Avoid overriding built in method OnClientConnect by reversing the naming scheme
    public static event Action ClientOnConnected;
    public static event Action ClientOnDisconnected;

    #region Server
    public override void OnStartServer()
    {
        base.OnStartServer();

        LumiNetworkPlayer.OnServerPlayerDefeat += ServerHandlePlayerDefeat;
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        
        LumiNetworkPlayer.OnServerPlayerDefeat -= ServerHandlePlayerDefeat;
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        LumiNetworkPlayer player = conn.identity.GetComponent<LumiNetworkPlayer>();
        player.SetPlayerName($"Player {players.Count + 1}"); // Initial player count starts from 0
        player.SetPlayerColor();

        players.Add(player);

    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);

        LumiNetworkPlayer disconnectedPlayer = conn.identity.GetComponent<LumiNetworkPlayer>();
        players.Remove(disconnectedPlayer);
        
        ClientOnDisconnected?.Invoke();
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
        ServerChangeScene(mapName);
    }

    #endregion
}
