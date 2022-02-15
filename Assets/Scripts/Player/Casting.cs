using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class Casting : NetworkBehaviour
{
    [SerializeField] Transform spellSpawnpoint;
    [SerializeField] List<Spell> spells;
    [SerializeField] Spell selectedSpell;
    [SerializeField] GameObject spellPrefab;
    [SerializeField] float castSpeed = 1f;
    [SerializeField] LayerMask groundLayer;

    Camera mainCamera;
    Mouse mouse;

    float previousSpellCastTime;
    PlayerInputActions playerInputActions;
    InputAction castPrimarySpell;
    InputAction castSecondarySpell;
    InputAction castThirdSpell;

    #region Server
    [Command]
    void CmdCastSpell(Vector3 castPosition)
    {
        if (CanCastAgain())
        {
            Vector3 spellDirection = transform.position - castPosition;

            Quaternion spellRotation = Quaternion.LookRotation(spellDirection);

            GameObject spellInstance = Instantiate(spellPrefab, spellSpawnpoint.position, spellRotation);
            NetworkServer.Spawn(spellInstance, connectionToClient);

            previousSpellCastTime = Time.time;
        }  
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
        castPrimarySpell = playerInputActions.Player.CastPrimarySpell;
        castSecondarySpell = playerInputActions.Player.CastSecondarySpell;
        castThirdSpell = playerInputActions.Player.CastThirdSpell;

        castPrimarySpell.performed += HandleSpellCast;
        castSecondarySpell.performed += HandleSpellCast;
        castThirdSpell.performed += HandleSpellCast;

        castPrimarySpell.Enable();
        castSecondarySpell.Enable();
        castThirdSpell.Enable();

        GameManager.ClientOnGameOver += ClientHandleGameOver;

    }

    [ClientCallback]
    void OnDestroy()
    {
        castPrimarySpell.performed -= HandleSpellCast;
        castSecondarySpell.performed -= HandleSpellCast;
        castThirdSpell.performed -= HandleSpellCast;

        castPrimarySpell.Disable();
        castSecondarySpell.Disable();
        castThirdSpell.Disable();
    }

    void HandleSpellCast(InputAction.CallbackContext ctx)
    {
        if (!hasAuthority) { return; }

        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(mouse.position.ReadValue());

        Debug.Log(ctx.action);

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            CmdCastSpell(hit.point);
        }
        
    }

    void ClientHandleGameOver(string winnerName)
    {
        castPrimarySpell.Disable();
        castSecondarySpell.Disable();
        castThirdSpell.Disable();
    }
    #endregion
}
