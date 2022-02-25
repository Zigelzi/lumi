using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class Movement : NetworkBehaviour
{
    [SerializeField] [Range(0, 50f)] float movementSpeed = 20f;

    PlayerInputActions playerInputActions;
    InputAction movement;

    CharacterController characterController;

    #region Server


    #endregion

    #region Client
    void Start()
    {
        playerInputActions = new PlayerInputActions();

        movement = playerInputActions.Player.Movement;
        movement.Enable();

        characterController = GetComponent<CharacterController>();

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

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        if (!hasAuthority) { return; }
        Vector2 moveInput = movement.ReadValue<Vector2>();

        if (moveInput.magnitude != 0)
        {
 
            Vector3 movementDirection = new Vector3();
            movementDirection.x = moveInput.x * Time.deltaTime * movementSpeed;
            movementDirection.z = moveInput.y * Time.deltaTime * movementSpeed;

            characterController.Move(movementDirection);
            ConstrainPosition();
        }
    }

    void ConstrainPosition()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    #endregion
}
