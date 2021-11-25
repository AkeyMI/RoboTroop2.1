using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AparecerMinion : MonoBehaviour
{
    [SerializeField] GameObject[] minionItem = default;

    public void SpawnItem()
    {
        Instantiate(minionItem[Random.Range(0, minionItem.Length - 1)], transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
