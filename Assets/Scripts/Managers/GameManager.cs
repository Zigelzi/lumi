using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    [SerializeField] List<LumiNetworkPlayer> players;

    public static event Action<LumiNetworkPlayer> ServerOnGameOver;
    public static event Action<string> ClientOnGameOver;

    #region Server
    public override void OnStartServer()
    {
        base.OnStartServer();

        LumiNetworkManager networkManager = FindObjectOfType<LumiNetworkManager>();

        players = networkManager.Players;

        Health.ServerOnPlayerDefeat += ServerHandlePlayerDefeat;
    }

    public override void OnStopServer()
    {
        base.OnStopServer();

        Health.ServerOnPlayerDefeat -= ServerHandlePlayerDefeat;
    }

    [Server]
    void ServerHandlePlayerDefeat(LumiNetworkPlayer player)
    {
        if (players.Count <= 1)
        {
            string winnerName = players[0].PlayerName;
            RpcGameOver(winnerName);
            ServerOnGameOver?.Invoke(player);
        }
    }
    #endregion

    #region Client
    [ClientRpc]
    void RpcGameOver(string winnerName)
    {
        ClientOnGameOver?.Invoke(winnerName);
    }
    #endregion
}
