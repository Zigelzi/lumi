using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] [Range(0, 50f)] float movementSpeed = 20f;

    PlayerInputActions playerInputActions;
    InputAction movement;

    #region Server
    [Command]
    void CmdMove(Vector3 direction)
    {
        if (!hasAuthority) { return; }

        Vector3 movementAmount = direction * Time.deltaTime * movementSpeed;

        transform.Translate(movementAmount);
    }

    #endregion

    #region Client
    public override void OnStartAuthority()
    {
        playerInputActions = new PlayerInputActions();
        movement = playerInputActions.Player.Movement;
        movement.Enable();
    }

    
    [ClientCallback]
    void OnDestroy()
    {
        movement.Disable();    
    }

    [ClientCallback]
    void Update()
    {
        if (!hasAuthority) { return; }

        Vector3 moveDirection = new Vector3(movement.ReadValue<Vector2>().x, 0, movement.ReadValue<Vector2>().y);
        CmdMove(moveDirection);
    }
    #endregion
}
