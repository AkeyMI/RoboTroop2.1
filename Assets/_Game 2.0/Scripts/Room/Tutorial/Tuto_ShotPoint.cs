using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuto_ShotPoint : MonoBehaviour
{
    [SerializeField] int life;
    Tuto tuto;
    SpawnerPool sp;
    void Start()
    {
        tuto = FindObjectOfType<Tuto>();
        sp = FindObjectOfType<SpawnerPool>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>() != null && life > 0)
        {
            life--;

            if (life == 0)
                Death();

            sp.GetParticle(12, transform.position);
            other.GetComponent<Bullet>().Destruction();
        }    
    }

    void Death()
    {
        tuto.ShotPoint();
        sp.GetParticle(10, transform.position);
        this.gameObject.SetActive(false);
    }
}
