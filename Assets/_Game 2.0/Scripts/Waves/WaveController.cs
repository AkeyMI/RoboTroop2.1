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
        Debug.Log("Primera wave");
        waves[0].Init(this);
        waves[0].SpawnWave();
    }

    public void KilledEnemy()
    {
        enemyCount--;

        if(enemyCount <= 0)
        {
            Debug.Log("Siguiente wave");
            NextWave();
        }
    }

    public void NextWave()
    {
        currentWave++;

        if (currentWave < waves.Length)
        {
            waves[currentWave].Init(this);
            waves[currentWave].SpawnWave();
            Debug.Log(currentWave);
        }
        else
        {
            GetComponentInParent<Room>().EndWave();
        }
    }

    public void Deactivate()
    {
        Destroy(this.gameObject);
    }
}
