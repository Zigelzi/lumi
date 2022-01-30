using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TeamColor : NetworkBehaviour
{
    [SerializeField] GameObject targetObject;
    [SyncVar(hook = nameof(HandlePlayerColorUpdated)) ]
    Color playerColor;

    #region Server
    public override void OnStartServer()
    {
        base.OnStartServer();

        LumiNetworkPlayer player = connectionToClient.identity.GetComponent<LumiNetworkPlayer>();
        playerColor = player.PlayerColor;
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
        if (targetObject == null) { return; }

        if (targetObject.transform.childCount == 0)
        {
            SetSingleObjectColor(newColor);
        }
        else
        {
            SetMultipleObjectColor(newColor);
        }
        
    }

    void SetSingleObjectColor(Color newColor)
    {
        Renderer renderer = targetObject.transform.GetComponent<Renderer>();
        renderer.material.color = newColor;
    }

    void SetMultipleObjectColor(Color newColor)
    {
        foreach (Transform child in targetObject.transform)
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
