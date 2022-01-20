using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LumiNetworkPlayer : NetworkBehaviour
{
    Health health;
    Color playerColor;
    string playerName;

    public static event Action<LumiNetworkPlayer> OnServerPlayerDefeat;
    public event Action<Color> ServerOnPlayerColorChange;

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
        OnServerPlayerDefeat?.Invoke(this);
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

    [Command]
    public void CmdStartGame()
    {
        LumiNetworkManager networkManager = (LumiNetworkManager)NetworkManager.singleton;

        networkManager.StartGame();
    }
    #endregion
}
