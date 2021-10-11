using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] float speed = 8f;
    [SerializeField] float timeStun = 1f;
    [SerializeField] int particulas;
    private int damage;
    private Rigidbody rb;
    SpawnerPool sp;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Init(int amount)
    {
        damage = amount;
    }

    private void Start()
    {
        rb.velocity = transform.forward * speed;
        sp = FindObjectOfType<SpawnerPool>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) return;

        if (other.CompareTag("Nave"))
        {
            other.GetComponent<NaveController>().Damage(damage);
            other.GetComponent<Stunable>().Stun(timeStun);
        }
        else if(other.CompareTag("Player"))
        {
            other.GetComponent<Stunable>().Stun(timeStun);
        }
        else if(other.CompareTag("ShieldNave"))
        {
            //FindObjectOfType<NaveController>().ShieldDamage(damage);
            other.GetComponentInParent<NaveController>().ShieldDamage(damage);
            //other.GetComponentInParent<Stunable>().Stun(timeStun);
        }
        else if(other.CompareTag("MinionShield"))
        {
            other.GetComponent<Shield>().ShieldHit(damage);
        }
        else if(other.CompareTag("AtkMinion"))
        {
            other.GetComponent<ShootController>().Damage(damage);
        }
        sp.GetParticle(particulas, transform.position);
        Destroy(this.gameObject);
    }
}
