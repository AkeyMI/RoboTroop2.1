using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestructionParticles : MonoBehaviour
{
    [SerializeField] float lifeTime;
    void Start()
    {
        if (lifeTime == 0)
            lifeTime = 5;
        StartCoroutine(AutoDestruction(lifeTime));
    }

    IEnumerator AutoDestruction(float i)
    {
        yield return new WaitForSeconds(i);
        Destroy(this.gameObject);
    }
}
