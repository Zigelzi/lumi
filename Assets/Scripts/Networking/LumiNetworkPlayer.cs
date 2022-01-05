using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LumiNetworkPlayer : NetworkBehaviour
{
    Health health;
    string playerName;

    public static event Action<LumiNetworkPlayer> OnServerPlayerDefeat;

    public string PlayerName { get { return playerName; } }

    #region Server
    public override void OnStartServer()
    {
        base.OnStartServer();

        health = GetComponent<Health>();

        health.ServerOnDie += HandleServerOnDie;
    }

    public override void OnStopServer()
    {
        base.OnStopServer();

        health.ServerOnDie -= HandleServerOnDie;
    }

    
    void HandleServerOnDie()
    {
        OnServerPlayerDefeat?.Invoke(this);
    }

    [Server]
    public void SetPlayerName(string newName)
    {
        playerName = newName;
    }
    #endregion
}
