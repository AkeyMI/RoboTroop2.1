using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour, IRoomActivables
{
    [SerializeField] Wave[] waves;

    private int currentWave;

    private int enemyCount;

    CameraController cam;

    private void Start()
    {
        cam = FindObjectOfType<CameraController>();
    }
    public void AddEnemy()
    {
        enemyCount++;
        Debug.Log("La cantidad de enemigos es: " + enemyCount);
    }

    public void Activate()
    {
       // Debug.Log("Primera wave");
        waves[0].Init(this);
        waves[0].SpawnWave();
    }

    public void KilledEnemy()
    {
        enemyCount--;
        cam.Shake(3.5f, 0.1f);
        cam.Offset(-0.5f);

        if(enemyCount <= 0)
        {
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
