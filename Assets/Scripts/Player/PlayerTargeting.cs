using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class PlayerTargeting : NetworkBehaviour
{
    [SerializeField] LayerMask groundLayer;
    Camera mainCamera;
    Mouse mouse;

    void Start()
    {
        mainCamera = Camera.main;
        mouse = Mouse.current;
    }

    void Update()
    {
        FaceAtMousePosition();
    }

    void FaceAtMousePosition()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(mouse.position.ReadValue());

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            transform.LookAt(hit.point);
        }
    }
}
