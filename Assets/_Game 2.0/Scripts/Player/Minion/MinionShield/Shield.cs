using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private ShieldController shieldController;
    private Animator animator;
    [SerializeField] GameObject minionShield;

    public void Start()
    {
        animator = minionShield.GetComponent<Animator>();
    }
    public void Init(ShieldController sC)
    {
        shieldController = sC;
    }

    public void ShieldHit(int amount)
    {
        animator.SetBool("HitShield", true);
        shieldController.ShieldDamage(amount);
        StartCoroutine(ShieldAnimationHit());
        //animator.SetBool("HitShield", false);
    }

    private IEnumerator ShieldAnimationHit()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("HitShield", false);
    }
}
