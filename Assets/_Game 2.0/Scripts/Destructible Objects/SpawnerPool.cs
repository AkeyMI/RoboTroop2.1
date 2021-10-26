using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPool : MonoBehaviour
{
    [SerializeField] SpawnData[] particleData;

    private void Start()
    {
        foreach(SpawnData sp in particleData)
        {
            if(sp.prefab !=null)
                ObjectPooling.PreLoad(sp.prefab , 1);
        }
    }

    public void GetParticle(int i , Vector3 t)
    {
        GameObject c = ObjectPooling.GetObj(particleData[i].prefab);
        c.transform.position = t;
        c.GetComponentInChildren<ParticleSystem>().Play();
        StartCoroutine (Despawn(particleData[i].prefab, c, particleData[i].timeDestroy));
    }

    IEnumerator Despawn(GameObject prefab,GameObject instance , float i)
    {
        yield return new WaitForSeconds(i);
        ObjectPooling.ReObj(prefab, instance);
    }

}

[System.Serializable]
public struct SpawnData
{
    public GameObject prefab;
    public float timeDestroy;
}