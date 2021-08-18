using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikaseController : MonoBehaviour
{
    [SerializeField] int KamikaseCount = 4;
    [SerializeField] float radiusForSpawn = 2f;
    [SerializeField] GameObject kamikasePrefab = default;

    private bool firstTime = true;

    //private MinionItemData data;

    //public void Init(MinionItemData itemData)
    //{
    //    data = itemData;
    //}

    private void OnEnable()
    {
        if(firstTime)
        {
            firstTime = false;
            return;
        }

        SpawnKamikases();
    }

    private void SpawnKamikases()
    {
        for(int i = 0; i < KamikaseCount; i ++)
        {
            var radians = 2 * Mathf.PI / KamikaseCount * i;

            var vertical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);

            var spawnDir = new Vector3(horizontal, 0, vertical);

            var point = new Vector3(this.transform.position.x, 0, this.transform.position.z);

            var spawnPos = point + spawnDir * radiusForSpawn;

            Instantiate(kamikasePrefab, spawnPos, Quaternion.identity);
        }

        this.gameObject.SetActive(false);
        //Destroy(this.gameObject);
    }
}
