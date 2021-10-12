using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DestructibleObject : MonoBehaviour
{
    [SerializeField] int life;    
    [SerializeField] int timeToDestroyFragments;
    [SerializeField] GameObject [] lod;

    bool isdead = false;
    int a;
    int b;
    private void Start()
    {
        if (life == 0)
            life = 2;

        a = life;
        b = lod.Length - 1;
        life = life * lod.Length;

        if (timeToDestroyFragments == 0)
            timeToDestroyFragments = 3;
    }
    public void Damage(int f, Transform or)
    {
        if (!isdead)
        {
            life -= f;
            if (life <= 0)
            {
                GetComponent<BoxCollider>().enabled = false;
                GetComponent<NavMeshObstacle>().enabled = false;

                isdead = true;
            }

            for (int i = b; life <= b * a; i--)
            {
                if (b >= 0)
                {
                    Rigidbody[] rb = lod[b].GetComponentsInChildren<Rigidbody>();

                    foreach (Rigidbody t in rb)
                    {
                        t.isKinematic = false;
                        t.AddForce(or.forward * 5, ForceMode.VelocityChange);
                    }
                    StartCoroutine(ToDestroy(timeToDestroyFragments, lod[b]));
                }
                b--;
            }

        }
    }

    IEnumerator ToDestroy(int i, GameObject dg)
    {
        yield return new WaitForSeconds (i);
        dg.SetActive(false);
    }

}