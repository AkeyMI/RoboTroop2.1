using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapEF : Trap
{
    [Header("Atributos Electric Fence")]
    [SerializeField] GameObject electricFence;
    [SerializeField] float timeActive;
    [SerializeField] bool startActive;
    BoxCollider boxTrigger;
    float currentTimeActive = -1;

    private void Start()
    {
        boxTrigger = GetComponent<BoxCollider>();
        boxTrigger.enabled = false;
        electricFence.SetActive(false);

    }
    public override void Activate()
    {
        alive = true;
        if (startActive)
        {
            boxTrigger.enabled = true;
            electricFence.SetActive(true);
            currentTimeActive = timeActive;
        }
        else
            StartCoroutine(Reload());

    }
    protected override void FixedUpdate()
    {
        if (alive)
        {
            if (currentTimeActive >= 0)
            {
                currentTimeActive -= Time.fixedDeltaTime;
                if (currentTimeActive <= 0)
                    StartCoroutine(Reload());
            }

            base.FixedUpdate();
        }
    }

    public void ReciveDamage(int i)
    {
        return;
        if (alive)
        { 
            life -= i;
            if (life <= 0)
                Deactivate();
        }      
    }

    public override void Deactivate()
    {
        alive = false;
        //foreach (CapsuleCollider cc in GetComponents<CapsuleCollider>())
        //    cc.enabled = false;

        boxTrigger.enabled = false;
        electricFence.SetActive(false);
    }

    IEnumerator Reload()
    {
        minionStayOnTrigger = false;
        naveStayOnTrigger = false;
        boxTrigger.enabled=false;
        electricFence.SetActive(false);
        yield return new WaitForSeconds(timeActive);
        if (alive)
        {
            boxTrigger.enabled = true;
            currentTimeActive = timeActive;
            electricFence.SetActive(true);
        }
    }
}
