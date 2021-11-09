using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Trap : MonoBehaviour
{
    [SerializeField] public trapType _trapType;
    [SerializeField] int damage;
    [SerializeField] float timeToMakeDamage;
    [SerializeField] int life;
    
    [Header("Atributos Especiales Torreta")]
    [SerializeField] int maxAmmo;
    [SerializeField] float reloadTime;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform[] cannons;
   
    
    bool alive;
    bool minionStayOnTrigger;
    float actuaTimeOnTrigerMinion;
    bool naveStayOnTrigger;
    float actuaTimeOnTrigerNave;
    
    int currentAmmo;
    float actualTimeBS = -1;
    public void WakeUP()
    {
        actuaTimeOnTrigerMinion = timeToMakeDamage;
        actuaTimeOnTrigerNave = timeToMakeDamage;
        if (_trapType == trapType.Turret)
        {
            actualTimeBS = timeToMakeDamage;
            alive = true;
            currentAmmo = maxAmmo;
        }
    }

    private void FixedUpdate()
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

 
        if (actualTimeBS >= 0 && alive)
        {
            actualTimeBS -= Time.fixedDeltaTime;
            if (actualTimeBS <= 0)
                Shot();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (_trapType != trapType.Turret)
        {
            if (other.CompareTag("AtkMinion"))
                minionStayOnTrigger = true;          

            if (other.CompareTag("Nave"))
                naveStayOnTrigger = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (_trapType != trapType.Turret)
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
    void Shot()
    {
        foreach (Transform t in cannons)
        {
            GameObject newBullet = Instantiate(bullet, t);
            newBullet.GetComponent<BulletEnemy>().Init(damage);
        }
        //animator.SetBool("Shot", true);
        currentAmmo--;
        if (currentAmmo == 0)
            StartCoroutine(Reloading());
        else
            actualTimeBS = timeToMakeDamage;
    }
    public void ReciveDamage(int i)
    { 
        life -= i;
        if (life <= 0 && alive)
        {
            alive = false;
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<NavMeshObstacle>().enabled = false;
            //animator.SetBool("Dead", true);
        }
        
    }

    public IEnumerator Reloading()
    {
        //animator.SetBool("Shot", false);
        //animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        transform.Rotate(0, 45, 0);
        //animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.5f);
        actualTimeBS = timeToMakeDamage;
    }

    public enum trapType { DangerZone, Mine, ElectricFence, Peaks, Turret }
}
