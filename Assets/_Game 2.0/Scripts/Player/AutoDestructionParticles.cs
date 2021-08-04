using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestructionParticles : MonoBehaviour
{
    [SerializeField] float lifeTime;
    void Start()
    {
        StartCoroutine(AutoDestruction(lifeTime));
    }

    IEnumerator AutoDestruction(float i)
    {
        yield return new WaitForSeconds(i);
        Destroy(this.gameObject);
    }
}
