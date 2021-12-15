using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPool : MonoBehaviour
{
    [SerializeField] SpawnData[] particleData;
    [Space(10)]
    [SerializeField] GameObject audioPrefab;
    [SerializeField] ClipSettings[] clipData;
    private void Start()
    {
        foreach(SpawnData sp in particleData)
        {
            if(sp.prefab !=null)
                ObjectPooling.PreLoad(sp.prefab , 1);
        }
        ObjectPooling.PreLoad(audioPrefab, 5);
    }

    public void GetParticle(int i , Vector3 t)
    {
        GameObject c = ObjectPooling.GetObj(particleData[i].prefab);
        c.transform.position = t;
        c.GetComponentInChildren<ParticleSystem>().Play();
        StartCoroutine (Despawn(particleData[i].prefab, c, particleData[i].timeDestroy));
    }

    public void GetSound(int s)
    {
        GameObject c = ObjectPooling.GetObj(audioPrefab);
        AudioSource audio = c.GetComponent<AudioSource>();
        audio.clip = clipData[s].clip;
        audio.pitch = Random.Range(clipData[s].minPitch, clipData[s].maxPitch);
        audio.volume = clipData[s].volume;
        audio.Play();
        StartCoroutine(Despawn(audioPrefab, c, clipData[s].clip.length));
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

[System.Serializable]
public struct ClipSettings
{
    public AudioClip clip;
    public float volume;
    public float minPitch;
    public float maxPitch;
}