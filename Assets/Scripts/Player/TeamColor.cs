using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TeamColor : NetworkBehaviour
{
    [SerializeField] GameObject playerModel;
    [SyncVar(hook = nameof(HandlePlayerColorUpdated)) ]
    Color playerColor;

    #region Server
    public override void OnStartServer()
    {
        base.OnStartServer();

        LumiNetworkPlayer player = connectionToClient.identity.GetComponent<LumiNetworkPlayer>();
        player.ServerOnPlayerColorChange += ClientHandleColorChange;
    }

    public override void OnStopServer()
    {
        base.OnStopServer();

        LumiNetworkPlayer player = connectionToClient.identity.GetComponent<LumiNetworkPlayer>();
        player.ServerOnPlayerColorChange -= ClientHandleColorChange;
    }
    #endregion

    #region Client
    void HandlePlayerColorUpdated(Color oldColor, Color newColor)
    {
        if (playerModel == null) { return; }

        foreach (Transform child in playerModel.transform)
        {
            Renderer renderer = child.GetComponent<Renderer>();
            renderer.material.color = newColor;
        }
    }

    void ClientHandleColorChange(Color newColor)
    {
        playerColor = newColor;
    }
    
    #endregion
}
