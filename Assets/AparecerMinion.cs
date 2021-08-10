using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AparecerMinion : MonoBehaviour
{
    [SerializeField] GameObject cosa = default;
    [SerializeField] GameObject minionItem = default;

    private void Update()
    {
        if(cosa == null)
        {
            minionItem.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
