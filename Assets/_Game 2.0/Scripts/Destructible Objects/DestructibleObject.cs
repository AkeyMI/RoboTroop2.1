using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    [SerializeField] int life;
    [SerializeField] GameObject [] lod;
    bool isdead = false;
    int a;
    int b;
    private void Start()
    {
        a = life;
        b = lod.Length - 1;
        life = life * lod.Length;
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
            
            if (life <= (b*a) )
            {
                Rigidbody[] rb = lod[b].GetComponentsInChildren<Rigidbody>();
                foreach (Rigidbody t in rb)
                {
                    t.isKinematic = false;
                    t.AddForce(or.forward * 5, ForceMode.VelocityChange);
                }
                b--;
            }
        }
    }

}
