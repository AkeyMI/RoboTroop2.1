using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMine : Trap
{
    public override void WakeUP(){}
    protected override void FixedUpdate(){}
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AtkMinion") || other.CompareTag("Nave"))
        {
            if(other.CompareTag("AtkMinion"))
                minionStayOnTrigger = true;
            else
                naveStayOnTrigger = true;

            if (!alive)
            {
                alive = true;
                StartCoroutine(Explosion());
            }
        }
    }

    IEnumerator Explosion()
    {
        animator.SetTrigger("Activate");
        GetComponent<CapsuleCollider>().radius = 1.5F;

        yield return new WaitForSeconds(timeToMakeDamage);

        sp.GetParticle(particles, transform.position);

        if (minionStayOnTrigger)
            FindObjectOfType<ShootController>().Damage(damage);
        if (naveStayOnTrigger)
            FindObjectOfType<NaveController>().Damage(damage);

        Destroy(this.gameObject);
    }
}
