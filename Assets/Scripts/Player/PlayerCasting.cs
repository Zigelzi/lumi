using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class PlayerCasting : NetworkBehaviour
{
    [SerializeField] Transform spellSpawnpoint;
    [SerializeField] GameObject spellPrefab;
    [SerializeField] float castSpeed = 1f;
    [SerializeField] LayerMask groundLayer;

    Camera mainCamera;
    Mouse mouse;

    float previousSpellCastTime;
    PlayerInputActions playerInputActions;
    InputAction casting;

    #region Server
    [Command]
    void CmdCastSpell(Vector3 castPosition)
    {
        Vector3 spellDirection = transform.position - castPosition;

        Quaternion spellRotation = Quaternion.LookRotation(spellDirection);
        
        GameObject spellInstance = Instantiate(spellPrefab, spellSpawnpoint.position, spellRotation);
        NetworkServer.Spawn(spellInstance, connectionToClient);

        previousSpellCastTime = Time.time;
    }

    bool CanCastAgain()
    {
        if (Time.time > (1 / castSpeed) + previousSpellCastTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region Client
    [ClientCallback]
    void Start()
    {
        mainCamera = Camera.main;
        mouse = Mouse.current;
        playerInputActions = new PlayerInputActions();
        casting = playerInputActions.Player.Casting;

        casting.performed += HandleSpellCast;
        casting.Enable();

    }

    [ClientCallback]
    void OnDestroy()
    {
        casting.performed -= HandleSpellCast;
        casting.Disable();
    }

    void HandleSpellCast(InputAction.CallbackContext ctx)
    {
        if (!hasAuthority) { return; }

        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(mouse.position.ReadValue());

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer) && CanCastAgain())
        {
            CmdCastSpell(hit.point);
        }
        
    }
    #endregion
}
