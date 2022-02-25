using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Spell : NetworkBehaviour
{
    [SerializeField] string spellName;
    [SerializeField] int id;
    [SerializeField] int damage = 10;
    [SerializeField] int projectileSpeed = 1;
    [SerializeField] int lifetime = 3;
    [SerializeField] LayerMask groundLayer;

    Rigidbody spellRb;

    public int Id { get { return id; } }

    #region Server
    void Start()
    {
        GameManager.ServerOnGameOver += ServerHandleGameOver;
    }

    [ServerCallback]
    void Update()
    {
        MoveSpell();
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

    public void MoveSpell()
    {
        transform.Translate(-Vector3.forward * Time.deltaTime * projectileSpeed);
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

        Debug.Log($"Collided with {collision.gameObject}");
        Debug.Log($"Collision is {collision}");

        if (IsEnemy(collision.collider) && collision.gameObject.TryGetComponent<Health>(out health))
        {
            health.TakeDamage(damage);
            DestroySelf();
        }

        if (!IsPlayers(collision.collider) && !IsGround(collision.collider))
        {            
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
