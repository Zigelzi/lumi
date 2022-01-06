using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Spell : NetworkBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] int launchForce = 1;
    [SerializeField] int lifetime = 3;

    Rigidbody spellRb;

    #region Server

    [ServerCallback]
    void Start()
    {
        LaunchSpell();

        GameManager.ServerOnGameOver += ServerHandleGameOver;
    }

    [ServerCallback]
    void OnDestroy()
    {
        GameManager.ServerOnGameOver -= ServerHandleGameOver;
    }

    [Server]
    void ServerHandleGameOver(LumiNetworkPlayer player)
    {
        DestroySelf();
    }

    [Server]
    public void LaunchSpell()
    {
        spellRb = GetComponent<Rigidbody>();
        if (spellRb == null) { return; }

        spellRb.velocity = -transform.forward * launchForce;
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
    #endregion
}
