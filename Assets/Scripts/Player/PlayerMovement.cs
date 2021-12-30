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


    #endregion

    #region Client
    void Start()
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
        HandleMovement();
    }

    void HandleMovement()
    {
        if (!hasAuthority) { return; }

        Vector3 moveDirection = new Vector3(movement.ReadValue<Vector2>().x, 0, movement.ReadValue<Vector2>().y);
        Vector3 newPosition = moveDirection * Time.deltaTime * movementSpeed;

        if (moveDirection.magnitude != 0)
        {
            transform.Translate(newPosition);
        }
    }
    #endregion
}
