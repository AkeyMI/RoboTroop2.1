using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMine : Trap
{
    [SerializeField] int explotionSound;
    public override void Activate(){}
    protected override void FixedUpdate(){}
    public override void Deactivate(){}
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
        sp.GetSound(explotionSound);
        if (minionStayOnTrigger)
            FindObjectOfType<ShootController>().Damage(damage);
        if (naveStayOnTrigger)
            FindObjectOfType<NaveController>().Damage(damage);

        Destroy(this.gameObject);
    }
}
