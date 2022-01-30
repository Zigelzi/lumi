using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LumiNetworkPlayer : NetworkBehaviour
{
    [SerializeField]
    [SyncVar(hook = nameof(AuthorityPartyOwnerUpdated)) ]
    bool isPartyOwner = false;

    Health health;
    Color playerColor;
    string playerName;

    public static event Action<LumiNetworkPlayer> ServerOnPlayerDefeat;
    public event Action<Color> ServerOnPlayerColorChange;
    public static event Action<bool> AuthorityOnPartyOwnerUpdated;

    public bool IsPartyOwner { get { return IsPartyOwner; } }
    public string PlayerName { get { return playerName; } }
    public Color PlayerColor { get { return playerColor; } }

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
        ServerOnPlayerDefeat?.Invoke(this);
    }

    [Server]
    public void SetPlayerName(string newName)
    {
        playerName = newName;
    }

    [Server]
    public void SetPlayerColor()
    {
        float red = UnityEngine.Random.Range(0, 1f);
        float green = UnityEngine.Random.Range(0, 1f);
        float blue = UnityEngine.Random.Range(0, 1f);

        Color newColor = new Color(red, green, blue);

        playerColor = newColor;
        ServerOnPlayerColorChange.Invoke(playerColor);
    }

    [Server]
    public void SetPartyOwner(bool newState)
    {
        isPartyOwner = newState;
    }

    [Command]
    public void CmdStartGame()
    {
        LumiNetworkManager networkManager = (LumiNetworkManager)NetworkManager.singleton;

        if (isPartyOwner)
        {
            networkManager.StartGame();
        }
        
    }
    #endregion

    #region Client
    public override void OnStartClient()
    {
        base.OnStartClient();

        // Prevent players being added to players list twice for host
        if (NetworkServer.active) { return; }

        LumiNetworkManager manager = (LumiNetworkManager)NetworkManager.singleton;
        manager.Players.Add(this);
    }

    public override void OnStopClient()
    {
        base.OnStopClient();

        // Prevent player being removed second time when player is host
        if (!isClientOnly) { return; }

        LumiNetworkManager manager = (LumiNetworkManager)NetworkManager.singleton;
        manager.Players.Remove(this);
    }

    void AuthorityPartyOwnerUpdated(bool oldState, bool newState)
    {
        if (!hasAuthority) { return; }

        AuthorityOnPartyOwnerUpdated?.Invoke(newState);
    }

    #endregion
}
