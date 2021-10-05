using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMInion : MonoBehaviour
{
    [SerializeField] Transform layout = default;
    [SerializeField] GameObject minionContainerPrefab = default;

    public void CreateBoxElements(List<MinionData> datas)
    {
        for(int i = 0; i < datas.Count; i++)
        {
            GameObject container = Instantiate(minionContainerPrefab, layout);
            MinionContainer minionContainer = container.GetComponent<MinionContainer>();
            minionContainer.SetData(datas[i]);
        }
    }
}
