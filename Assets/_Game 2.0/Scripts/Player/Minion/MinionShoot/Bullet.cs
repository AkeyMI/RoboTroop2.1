using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //[SerializeField] float speed = 8f;
    [SerializeField] GameObject particulas;
    [SerializeField] BulletEffect bulletEffect = default;
    [SerializeField] AudioClip hitSound = default;
    [SerializeField] AudioSource audioSource = default;
    [SerializeField] GameObject sound = default;
    private int damage;
    private Rigidbody rb;
    private float currentSpeed;

    public int Damage => damage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
        if (other.CompareTag("Player")) return;

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

    }

    public void Destruction()
    {
        Instantiate<GameObject>(particulas).transform.position = this.transform.position;//Instanciar 
        Instantiate(sound);
        Destroy(this.gameObject);
    }
}
