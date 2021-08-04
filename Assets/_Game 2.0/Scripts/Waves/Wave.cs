using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private SimpleSpawner[] spawners;

    WaveController waveController;

    public void Init(WaveController waveController)
    {
        this.waveController = waveController;
    }

    private void Start()
    {
        spawners = GetComponentsInChildren<SimpleSpawner>(true);
    }

    public void SpawnWave()
    {
        for(int i = 0; i < spawners.Length; i++)
        {
            spawners[i].Init(waveController);
            spawners[i].Spawn();
        }
    }
}
