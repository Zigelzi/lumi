﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;

public class PlayerHealth : NetworkBehaviour
{
    [SerializeField] int maxHealth = 100;

    [SyncVar(hook = nameof(ClientHandleHealthUpdate))]
    [SerializeField] int currentHealth;

    public event Action ServerOnDie;
    public event Action<int, int> ClientOnHealthUpdate;

    #region Server
    public override void OnStartServer()
    {
        base.OnStartServer();

        currentHealth = maxHealth;
    }

    [Server]
    public void TakeDamage(int amount)
    {
        if(currentHealth > 0)
        {
            currentHealth -= amount;
        }
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    [Server]
    void Die()
    {
        ServerOnDie?.Invoke();
    }

    #endregion

    #region Client
    void ClientHandleHealthUpdate(int oldHealth, int newHealth)
    {
        ClientOnHealthUpdate?.Invoke(newHealth, maxHealth);
    }
    #endregion
}