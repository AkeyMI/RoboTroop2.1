using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //[SerializeField] float speed = 8f;
    [SerializeField] GameObject particulas;
    [SerializeField] BulletEffect bulletEffect = default;
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
            Destroy(this.gameObject);
            Debug.Log("Enemy Hit");
            return;
        }

        if (bulletEffect != null)
        {
            bulletEffect.Apply(this.gameObject, other.gameObject);
        }

        Instantiate<GameObject>(particulas).transform.position = this.transform.position;//Instanciar 
        Destroy(this.gameObject);
    }
}
