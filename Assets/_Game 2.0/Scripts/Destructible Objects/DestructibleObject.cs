using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    [SerializeField] int life;
    [SerializeField] GameObject [] lod;
    bool isdead = false;
    int l;

    private void Start()
    {
        l = life;
    }
    public void Damage(int f, Transform or)
    {
        if (!isdead)
        {
            life -= f;
            if (life <= 0)
            {
                GetComponent<BoxCollider>().enabled = false;
                isdead = true;
            }
            for (int i = l - 1; i >= life; i--)
            {
                if (i >= 0)
                {
                    lod[i].GetComponent<Rigidbody>().isKinematic = false;
                    lod[i].GetComponent<Rigidbody>().AddForce(or.forward * 5, ForceMode.VelocityChange);
                }
            }
            l = life;

        }
    }

}
