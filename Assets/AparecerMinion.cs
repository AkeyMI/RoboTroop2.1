using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AparecerMinion : MonoBehaviour
{
    [SerializeField] GameObject cosa = default;
    [SerializeField] GameObject[] minionItem = default;
    [SerializeField] Transform spawnpointItem = default;

    private void Update()
    {
        if(cosa == null)
        {
            Instantiate(minionItem[Random.Range(0, minionItem.Length - 1)], spawnpointItem.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
