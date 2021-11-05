using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrapTurret : MonoBehaviour
{
    [SerializeField] Transform [] cannons;
    [SerializeField] GameObject bullet;
    [SerializeField] int damage;
    [SerializeField] int life;
    [SerializeField] int maxAmmo;
    [SerializeField] float timeBetweenShots;
    Animator animator;
    bool alive = false;
    int currentAmmo;
    float actualTimeBS;
    bool invencible;
    public void WakeUP()
    {
        actualTimeBS = timeBetweenShots;
        alive = true;
        currentAmmo = maxAmmo;
        //animator.SetBool("Reloading", false);
    }

    void FixedUpdate()
    {
        if (alive)
        {
            if (actualTimeBS > 0)
                actualTimeBS -= Time.fixedDeltaTime;
            if(currentAmmo > 0 && actualTimeBS <= 0)
                Shoot();          
        }
    }

    void Shoot()
    {
        foreach(Transform t in cannons)
        {
            GameObject newBullet = Instantiate(bullet, t);
            newBullet.GetComponent<BulletEnemy>().Init(damage);
        }
        //animator.SetBool("Shot", true);
        currentAmmo--;
        if (currentAmmo == 0)
            StartCoroutine(Reloading());
        else
            actualTimeBS = timeBetweenShots;
    }

    public void Damage(int i)
    {
        if (!invencible)
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
    }

    public IEnumerator Reloading ()
    {
        //animator.SetBool("Shot", false);
        //animator.SetBool("Reloading", true);
        invencible = true;
        yield return new WaitForSeconds(2.5f);
        currentAmmo = maxAmmo;
        transform.Rotate(0,45,0);
        //animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.5f);
        invencible = false;
        actualTimeBS = timeBetweenShots;
    }
}
