using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMInion : MonoBehaviour
{
    [SerializeField] Transform layoutIn = default;
    [SerializeField] Transform layout = default;
    [SerializeField] GameObject minionContainerPrefab = default;
    [SerializeField] Canvas canvas = default;

    public Canvas GetCanvas => canvas;

    public void CreateBoxElements(List<MinionData> datas)
    {
        for(int i = 0; i < datas.Count; i++)
        {
            GameObject container = Instantiate(minionContainerPrefab, layout);
            MinionContainer minionContainer = container.GetComponent<MinionContainer>();
            minionContainer.SetData(datas[i]);
        }
    }

    public void CreateInElements(List<MinionData> datas)
    {
        for (int i = 0; i < datas.Count; i++)
        {
            GameObject container = Instantiate(minionContainerPrefab, layoutIn);
            MinionContainer minionContainer = container.GetComponent<MinionContainer>();
            minionContainer.SetData(datas[i]);
        }
    }
}
