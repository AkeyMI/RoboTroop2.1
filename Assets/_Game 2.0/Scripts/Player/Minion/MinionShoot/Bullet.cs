using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //[SerializeField] float speed = 8f;
    [SerializeField] int particulas;
    [SerializeField] BulletEffect bulletEffect = default;
    [SerializeField] int life;
    [SerializeField] int destroySound;
    private int damage;
    private Rigidbody rb;
    private float currentSpeed;
    SpawnerPool sp;
    public int Damage => damage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sp = FindObjectOfType<SpawnerPool>();
    }

    public void Init(int amount, float speed)
    {
        damage = amount;
        currentSpeed = speed;
    }

    private void Start()
    {
        rb.velocity = transform.forward * currentSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().Damage(damage);
            other.GetComponent<EnemyController>().Particles(transform.position);
            if (bulletEffect != null)
            {
                bulletEffect.Apply(this.gameObject, other.gameObject);
            }
            Destruction();
        }

        if(other.CompareTag("Obj"))
        {
            other.GetComponent<DestructibleObject>().Damage(damage, transform);
            Destruction();
        }

        if (other.CompareTag("Walls"))
        {
            life--;
            if(life <= 0)
                Destruction();
            else
                sp.GetSound(destroySound);
        }
            
        if (other.CompareTag("Trap"))
        {
            if (other.GetComponent<TrapTurret>() != null)
            {
                other.GetComponent<TrapTurret>().ReciveDamage(damage);
                Destruction();
            }
            if (other.GetComponent<TrapEF>() != null)
            {
                other.GetComponent<TrapEF>().ReciveDamage(damage);
                Destruction();
            }
        }
    }

    public void Destruction()
    {
        sp.GetParticle(particulas, transform.position);
        sp.GetSound(destroySound);
        sp.GetSound(0);
        Destroy(this.gameObject);
    }
}
