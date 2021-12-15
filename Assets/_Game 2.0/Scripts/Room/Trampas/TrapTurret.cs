using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrapTurret : Trap
{
    [Header("Turret Atrubutes")]
    [SerializeField] int maxAmmo;
    [SerializeField] float reloadTime;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform[] cannons;
    [SerializeField] int ShotSound;
    int currentAmmo;
    float actualTimeBS = -1;
    CapsuleCollider capsuleCollider;

    public override void Deactivate(){}
    public override void Activate()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        alive = true;
        StartCoroutine(Reloading()); 
    }

    protected override void FixedUpdate()
    {
        if (actualTimeBS >= 0 && alive)
        {
            actualTimeBS -= Time.fixedDeltaTime;
            if (actualTimeBS <= 0)
                Shot();
        }
    }
    void Shot()
    {
        foreach (Transform t in cannons)
        {
            GameObject newBullet = Instantiate(bullet, t);
            newBullet.GetComponent<BulletEnemy>().Init(damage);
            sp.GetSound(ShotSound);
        }

        animator.SetBool("Shot", true);
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
            StartCoroutine(Death());
        }
    }

    IEnumerator Reloading()
    {
        capsuleCollider.enabled = false;
        animator.SetBool("Shot", false);
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        transform.Rotate(0, 45, 0);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.5f);
        capsuleCollider.enabled = true;
        actualTimeBS = timeToMakeDamage;
    }

    IEnumerator Death()
    {
        capsuleCollider.enabled = false;
        GetComponent<NavMeshObstacle>().enabled = false;
        animator.SetTrigger("Dead");
        yield return new WaitForSeconds(0);
        sp.GetParticle(10, transform.position);
    }
}
