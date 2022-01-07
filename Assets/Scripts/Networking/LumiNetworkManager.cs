using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class LumiNetworkManager : NetworkManager
{
    [SerializeField] List<LumiNetworkPlayer> players = new List<LumiNetworkPlayer>();
    [SerializeField] GameManager gameManagerPrefab;

    string mapName = "Scene_Arena";

    public List<LumiNetworkPlayer> Players { get { return players; } }
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
        player.SetPlayerName($"Player {players.Count}");

        players.Add(player);
        
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        LumiNetworkPlayer disconnectedPlayer = conn.identity.GetComponent<LumiNetworkPlayer>();
        players.Remove(disconnectedPlayer);

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
