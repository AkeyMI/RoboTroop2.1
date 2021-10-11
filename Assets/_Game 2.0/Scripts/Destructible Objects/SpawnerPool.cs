using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPool : MonoBehaviour
{
    
    [SerializeField] GameObject[] particulas;
    [SerializeField] float[] lifeTimeParticles;

    private void Awake()
    {
        foreach (GameObject go in particulas)
        {
            if(go != null)
                ObjectPooling.PreLoad(go, 1);
        }
    }

    public void GetParticle(int i , Vector3 t)
    {
        GameObject c = ObjectPooling.GetObj(particulas[i]);
        c.transform.position = t;
        c.GetComponentInChildren<ParticleSystem>().Play();
        StartCoroutine (Despawn(particulas[i], c, lifeTimeParticles[i]));
    }

    IEnumerator Despawn(GameObject prefab,GameObject instance , float i)
    {
        yield return new WaitForSeconds(i);
        ObjectPooling.ReObj(prefab, instance);
    }

}
