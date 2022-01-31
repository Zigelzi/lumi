using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;

public class Health : NetworkBehaviour
{
    [SerializeField] int maxHealth = 100;

    [SyncVar(hook = nameof(ClientHandleHealthUpdate))]
    [SerializeField] int currentHealth;

    public int MaxHealth { get { return maxHealth; } }

    public static event Action<LumiNetworkPlayer> ServerOnPlayerDefeat;
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
        LumiNetworkPlayer player = connectionToClient.identity.GetComponent<LumiNetworkPlayer>();
        ServerOnPlayerDefeat?.Invoke(player);
    }

    #endregion

    #region Client
    void ClientHandleHealthUpdate(int oldHealth, int newHealth)
    {
        ClientOnHealthUpdate?.Invoke(newHealth, maxHealth);
    }
    #endregion
}
