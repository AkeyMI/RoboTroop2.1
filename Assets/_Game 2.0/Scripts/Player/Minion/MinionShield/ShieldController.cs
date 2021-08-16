using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [SerializeField] GameObject shield = default;
    [SerializeField] int lifeShield = 15;
    [SerializeField] float timeToReloadShield = 5f;
    
    Animator animator;

    private int currentLifeShield;
    private bool shieldIsBroken;

    private bool shieldIsFixingUp;

    private void Start()
    {
        shield.GetComponent<Shield>().Init(this);
        animator = GetComponent<Animator>();
        currentLifeShield = lifeShield;
    }

    private void Update()
    {
        if (!shieldIsBroken)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetBool("HoldingShield", true);
                UseShield();
            }

            if (Input.GetMouseButtonUp(0))
            {
                animator.SetBool("HoldingShield", false);
                OffShield();
            }
        }
        else
        {
            if (!shieldIsFixingUp)
            {
                StartCoroutine(FixShield());
                animator.SetBool("HoldingShield", false);
                OffShield();
            }
        }
    }

    public void ShieldDamage(int amount)
    {
        currentLifeShield -= amount;

        if(currentLifeShield <= 0)
        {
            shieldIsBroken = true;
        }
    }

    IEnumerator FixShield()
    {
        shieldIsFixingUp = true;
        yield return new WaitForSeconds(timeToReloadShield);
        currentLifeShield = lifeShield;
        shieldIsBroken = false;
        shieldIsFixingUp = false;
    }

    private void UseShield()
    {
        shield.SetActive(true);
    }

    private void OffShield()
    {
        shield.SetActive(false);
    }

}
