using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikaseController : MonoBehaviour
{
    [SerializeField] float timeForSpawnKamikases = 1f;

    private MinionItemData data;

    public void Init(MinionItemData itemData)
    {
        data = itemData;
    }

    private void OnEnable()
    {
        StartCoroutine(KamiCoroutine());
    }

    private IEnumerator KamiCoroutine()
    {
        yield return new WaitForSeconds(timeForSpawnKamikases);

    }
}
