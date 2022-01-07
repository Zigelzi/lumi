using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class Targeting : NetworkBehaviour
{
    [SerializeField] LayerMask groundLayer;
    Camera mainCamera;
    Mouse mouse;

    #region Client
    [ClientCallback]
    void Start()
    {
        mainCamera = Camera.main;
        mouse = Mouse.current;

        GameManager.ClientOnGameOver += ClientHandleGameOver;
    }

    void OnDestroy()
    {
        GameManager.ClientOnGameOver -= ClientHandleGameOver;
    }

    [Client]
    void ClientHandleGameOver(string winnerName)
    {
        DisableTargeting();
    }

    [Client]
    void DisableTargeting()
    {
        enabled = false;
    }

    [ClientCallback]
    void Update()
    {
        FaceAtMousePosition();
    }

    [Client]
    void FaceAtMousePosition()
    {
        if (!hasAuthority) { return; }

        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(mouse.position.ReadValue());

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            transform.LookAt(hit.point);
        }
    }
    #endregion
}
