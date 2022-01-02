using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class PlayerCasting : NetworkBehaviour
{
    [SerializeField] Transform spellSpawnpoint;
    [SerializeField] GameObject spellPrefab;
    [SerializeField] float spellCastInterval = 1f;
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

        Debug.Log($"Player is at: {transform.position} and spell is casted at {castPosition}");
        Debug.Log($"Spell Direction: {spellDirection}");
        Quaternion spellRotation = Quaternion.LookRotation(spellDirection);
        
        GameObject spellInstance = Instantiate(spellPrefab, spellSpawnpoint.position, spellRotation);
        NetworkServer.Spawn(spellInstance, connectionToClient);

        Spell castedSpell = spellInstance.GetComponent<Spell>();
        castedSpell.LaunchSpell(-spellDirection.normalized);

        previousSpellCastTime = Time.time;
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

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            CmdCastSpell(hit.point);
        }
        
    }
    #endregion
}
