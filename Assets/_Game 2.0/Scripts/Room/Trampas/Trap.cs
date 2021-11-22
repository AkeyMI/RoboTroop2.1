using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected float timeToMakeDamage;

    [Header("Conditional Atributes")]
    [SerializeField] protected int particles;
    [SerializeField] protected int life;

    protected SpawnerPool sp;
    protected bool alive;
    protected bool minionStayOnTrigger;
    protected float actuaTimeOnTrigerMinion;
    protected bool naveStayOnTrigger;
    protected float actuaTimeOnTrigerNave;

    protected Animator animator;

    private void Awake()
    {
        if(GetComponent<Animator>() != null)
            animator = GetComponent<Animator>();
        sp = FindObjectOfType<SpawnerPool>();
    }
    public virtual void WakeUP()
    {
        actuaTimeOnTrigerMinion = timeToMakeDamage;
        actuaTimeOnTrigerNave = timeToMakeDamage;
    }

    protected virtual void FixedUpdate()
    {
        if (minionStayOnTrigger && actuaTimeOnTrigerMinion >= 0)
        {
            actuaTimeOnTrigerMinion -= Time.fixedDeltaTime;
            if (actuaTimeOnTrigerMinion <= 0)
            {
                FindObjectOfType<ShootController>().Damage(damage);
                actuaTimeOnTrigerMinion = timeToMakeDamage;
            }
        }

        if (naveStayOnTrigger && actuaTimeOnTrigerNave >= 0)
        {
            actuaTimeOnTrigerNave -= Time.fixedDeltaTime;
            if(actuaTimeOnTrigerNave <= 0)
            {
                FindObjectOfType<NaveController>().Damage(damage);
                actuaTimeOnTrigerNave = timeToMakeDamage;
            }
        }
    }

    virtual protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AtkMinion"))
            minionStayOnTrigger = true;

        if (other.CompareTag("Nave"))
            naveStayOnTrigger = true;
    }
    void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("AtkMinion"))
        {
            minionStayOnTrigger = false;
            actuaTimeOnTrigerMinion = timeToMakeDamage;
        }

        if (other.CompareTag("Nave"))
        {
            naveStayOnTrigger = false;
            actuaTimeOnTrigerNave = timeToMakeDamage;
        }      
    }
}
