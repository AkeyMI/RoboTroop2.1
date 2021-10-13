using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPool : MonoBehaviour
{
    [SerializeField] SpawnData[] spData;

    private void Awake()
    {
        for (int i = 0; i < spData.Length; i++)
        {
            if(spData[i].prefab !=null)
                ObjectPooling.PreLoad(spData[i].prefab , 1);
        }

        //foreach (GameObject go in )
        //{
        //   if(go != null)
        //        ObjectPooling.PreLoad(go, 1);
        //}
    }

    public void GetParticle(int i , Vector3 t)
    {
        GameObject c = ObjectPooling.GetObj(spData[i].prefab);
        c.transform.position = t;
        c.GetComponentInChildren<ParticleSystem>().Play();
        StartCoroutine (Despawn(spData[i].prefab, c, spData[i].timeDestroy));
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