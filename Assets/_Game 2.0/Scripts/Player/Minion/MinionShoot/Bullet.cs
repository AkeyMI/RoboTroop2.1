using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //[SerializeField] float speed = 8f;
    [SerializeField] GameObject particulas;
    private int damage;
    private Rigidbody rb;
    private float currentSpeed;

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
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().Damage(damage);
            Destroy(this.gameObject);
            Debug.Log("Enemy Hit");
        }
        if (other.CompareTag("Player"))
        {
            return;
        }
        else
        {
            Instantiate<GameObject>(particulas).transform.position = this.transform.position;//Instanciar 
            Destroy(this.gameObject);
        }
    }
}
