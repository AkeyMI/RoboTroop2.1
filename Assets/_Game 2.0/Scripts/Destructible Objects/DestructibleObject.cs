using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DestructibleObject : MonoBehaviour
{
    [SerializeField] int life;    
    [SerializeField] int timeToDestroyFragments;
    [SerializeField] GameObject [] lod;
    SpawnerPool sp;
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
        sp = FindObjectOfType<SpawnerPool>();
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

                    foreach (Rigidbody r in rb)
                    {
                        r.isKinematic = false;
                        r.AddForce(or.forward * 5, ForceMode.VelocityChange);
                        sp.GetParticle(9, r.GetComponent<Transform>().transform.position);
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
        foreach(Transform t in dg.GetComponentsInChildren<Transform>())
            sp.GetParticle(9, t.GetComponent<Transform>().transform.position);
        
        dg.SetActive(false);
    }

}
