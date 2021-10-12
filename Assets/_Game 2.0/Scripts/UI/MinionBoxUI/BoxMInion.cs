using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMInion : MonoBehaviour
{
    [SerializeField] Transform layout = default;
    [SerializeField] GameObject minionContainerPrefab = default;
    [SerializeField] Canvas canvas = default;
    [SerializeField] MinionContainer[] mContainerArray = default;

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
        for (int i = 0; i < mContainerArray.Length; i++)
        {
            MinionContainer minionContainer = mContainerArray[i].GetComponent<MinionContainer>();
            if (i < datas.Count)
            {
                minionContainer.SetData(datas[i]);
            }
            else
            {
                MinionUiBox mUiBox = mContainerArray[i].GetComponentInChildren<MinionUiBox>();
                Destroy(mUiBox.gameObject);
            }
        }
    }
}
