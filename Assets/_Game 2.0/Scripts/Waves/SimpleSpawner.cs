using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefab = default;

    private WaveController waveController;

    public void Init(WaveController waveController)
    {
        this.waveController = waveController;
    }

    public void Spawn()
    {
        GameObject go = Instantiate(prefab, transform.position, transform.rotation);
        go.GetComponent<EnemyController>().Init(waveController);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
