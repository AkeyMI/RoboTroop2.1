using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AparecerMinion : MonoBehaviour
{
    [SerializeField] GameObject cosa = default;
    [SerializeField] GameObject[] minionItem = default;

    private void Update()
    {
        if(cosa == null)
        {
            minionItem[Random.Range(0, minionItem.Length - 1)].SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
