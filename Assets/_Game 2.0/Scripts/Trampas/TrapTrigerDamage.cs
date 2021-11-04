using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigerDamage : MonoBehaviour
{    
    [SerializeField] trapType _trapType;
    [SerializeField] int damage;
    [SerializeField] float timeToMakeDamage;
    [SerializeField] LayerMask canDamage;

    Animator animator;
    float actualTime;
    bool start = false;

    void WakeUP()
    {
        start = true;
        switch (_trapType)
        {
            case trapType.DangerZone:
                actualTime = timeToMakeDamage;
                break;
            case trapType.Mine:

                break;
            case trapType.ElectricFence:
                actualTime = timeToMakeDamage;
                break;
            case trapType.Peaks:
                actualTime = timeToMakeDamage;
                break;
        }
    }
    void FixedUpdate()
    {
        if (start)
        {
            if (actualTime >= 0)
                actualTime -= Time.fixedDeltaTime;
        }
    }

    public enum trapType { DangerZone, Mine, ElectricFence, Peaks}

}
