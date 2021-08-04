using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour, IRoomActivables
{
    [SerializeField] Wave[] waves;

    private int currentWave;

    private int enemyCount;

    public void AddEnemy()
    {
        enemyCount++;
    }

    public void Activate()
    {
        waves[0].Init(this);
        waves[0].SpawnWave();
    }

    public void KilledEnemy()
    {
        enemyCount--;

        if(enemyCount <= 0)
        {
            NextWave();
        }
    }

    public void NextWave()
    {
        currentWave++;

        if (currentWave <= waves.Length)
        {
            waves[currentWave].Init(this);
            waves[currentWave].SpawnWave();
        }
    }
}
