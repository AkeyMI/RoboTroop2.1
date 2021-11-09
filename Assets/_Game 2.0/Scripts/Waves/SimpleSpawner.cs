using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefab = default;
    [SerializeField] bool isATorreta = default;
    private WaveController waveController;
    SpawnerPool sp;
    private void Start()
    {
        sp = FindObjectOfType<SpawnerPool>();
    }
    public void Init(WaveController waveController)
    {
        this.waveController = waveController;
    }

    public void Spawn()
    {
        StartCoroutine(Spawning());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }

    IEnumerator Spawning ()
    {
        sp.GetParticle(14, transform.position);
        yield return new WaitForSeconds(2.25f);
        GameObject go = Instantiate(prefab, transform.position, transform.rotation);
        if (!isATorreta)
        {
            go.GetComponent<EnemyController>().Init(waveController);
        }
        else
        {
            go.GetComponentInChildren<EnemyController>().Init(waveController);
        }
    }

}
