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

    float previousSpellCastTime;
    PlayerInputActions playerInputActions;
    InputAction casting;

    #region Server
    [Command]
    void CmdCastSpell()
    {
        GameObject spellInstance = Instantiate(spellPrefab, spellSpawnpoint.position, Quaternion.identity);
        NetworkServer.Spawn(spellInstance, connectionToClient);
        Rigidbody spellRb = spellInstance.GetComponentInChildren<Rigidbody>();
        spellRb.velocity = transform.forward * 20f;

        previousSpellCastTime = Time.time;
    }
    #endregion

    #region Client
    [ClientCallback]

    void Start()
    {
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
        CmdCastSpell();
    }
    #endregion
}
