using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class Movement : NetworkBehaviour
{
    [SerializeField] [Range(0, 50f)] float movementSpeed = 20f;
    [SerializeField] [Range(0, 50f)] float maxAcceleration = 20f;

    PlayerInputActions playerInputActions;
    InputAction movement;
    Rigidbody playerRb;

    #region Server


    #endregion

    #region Client
    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerRb = GetComponent<Rigidbody>();

        movement = playerInputActions.Player.Movement;
        movement.Enable();

        GameManager.ClientOnGameOver += ClientHandleGameOver;
    }

    [ClientCallback]
    void OnDestroy()
    {
        movement.Disable();

        GameManager.ClientOnGameOver -= ClientHandleGameOver;
    }

    void ClientHandleGameOver(string winnerName)
    {
        movement.Disable();
    }

    [ClientCallback]
    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        if (!hasAuthority) { return; }
        
        Vector2 moveDirection = movement.ReadValue<Vector2>();

        if (moveDirection.magnitude != 0)
        {
            float maxVelocityChange = maxAcceleration * Time.deltaTime;
            Vector3 playerVelocity = playerRb.velocity;
            Vector3 desiredVelocity = moveDirection * movementSpeed;

            playerVelocity.x = Mathf.MoveTowards(playerVelocity.x, desiredVelocity.x, maxVelocityChange);
            playerVelocity.z = Mathf.MoveTowards(playerVelocity.z, desiredVelocity.y, maxVelocityChange);

            playerRb.velocity = playerVelocity;
        }
    }

    #endregion
}
