using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Spell : NetworkBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] int launchForce = 1;
    [SerializeField] int lifetime = 3;
    [SerializeField] LayerMask groundLayer;

    Rigidbody spellRb;

    #region Server
    void Start()
    {
        LaunchSpell();

        GameManager.ServerOnGameOver += ServerHandleGameOver;
    }

    void OnDestroy()
    {
        GameManager.ServerOnGameOver -= ServerHandleGameOver;
    }

    [Server]
    void ServerHandleGameOver(LumiNetworkPlayer player)
    {
        DestroySelf();
    }

    public void LaunchSpell()
    {
        Debug.Log("Launched spell on server");
        spellRb = GetComponent<Rigidbody>();
        if (spellRb == null) { return; }

        spellRb.velocity = -transform.forward * launchForce;

        Debug.Log($"Spell velocity set to {spellRb.velocity}");
    }
    public override void OnStartServer()
    {
        base.OnStartServer();

        Invoke(nameof(DestroySelf), lifetime);
    }

    [ServerCallback]
    private void OnCollisionEnter(Collision collision)
    {
        Health health;

        if (IsEnemy(collision.collider) && collision.gameObject.TryGetComponent<Health>(out health))
        {
            health.TakeDamage(damage);
            DestroySelf();
        }

        if (!IsPlayers(collision.collider) && !IsGround(collision.collider))
        {
            Debug.Log("Spell hit something that isn't players and ground");
            
            DestroySelf();
        }
    }

    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }

    [Server]
    bool IsEnemy(Collider other)
    {
        NetworkIdentity identity;

        if (other.TryGetComponent<NetworkIdentity>(out identity))
        {
            if (identity.connectionToClient != connectionToClient)
            {
                return true;
            }
            else 
            { 
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    [Server]
    bool IsPlayers(Collider other)
    {
        NetworkIdentity identity;

        if (other.TryGetComponent<NetworkIdentity>(out identity))
        {
            if (identity.connectionToClient == connectionToClient)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    [Server]
    bool IsGround(Collider other)
    {
        if (groundLayer == (groundLayer | (1 << other.gameObject.layer)))
        {
            return true;
        }
        else 
        { 
            return false;
        }
    }
    #endregion
}
