using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TeamColor : NetworkBehaviour
{
    [SerializeField] GameObject targetObject;
    
    [SerializeField]
    [SyncVar(hook = nameof(HandlePlayerColorUpdated)) ]
    Color playerColor = new Color();

    #region Server
    public override void OnStartServer()
    {
        base.OnStartServer();

        LumiNetworkPlayer player = connectionToClient.identity.GetComponent<LumiNetworkPlayer>();
        playerColor = player.PlayerColor;
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
    
    #endregion
}
