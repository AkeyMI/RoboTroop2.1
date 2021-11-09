using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //[SerializeField] float speed = 8f;
    [SerializeField] int particulas;
    [SerializeField] BulletEffect bulletEffect = default;
    [SerializeField] AudioClip hitSound = default;
    [SerializeField] AudioSource audioSource = default;
    [SerializeField] GameObject sound = default;
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
            Destruction();
        if (other.CompareTag("Trap"))
        {
            other.GetComponent<Trap>().ReciveDamage(damage);
            Destruction();
        }
    }

    public void Destruction()
    {
        sp.GetParticle(particulas, transform.position);
        Instantiate(sound);
        Destroy(this.gameObject);
    }
}
