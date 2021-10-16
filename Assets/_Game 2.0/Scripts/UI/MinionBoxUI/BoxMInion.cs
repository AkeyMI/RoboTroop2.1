using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMInion : MonoBehaviour
{
    [SerializeField] Transform layout = default;
    [SerializeField] GameObject minionContainerPrefab = default;
    [SerializeField] Canvas canvas = default;
    [SerializeField] MinionContainer[] mContainerArray = default;
    [SerializeField] GameObject minionUiBoxPrefab = default;

    private MinionContainer[] containerBox;

    private List<MinionData> atkMinionsList = new List<MinionData>();
    private List<MinionData> atkMinionsBoxList = new List<MinionData>();

    public Canvas GetCanvas => canvas;

    public void CreateBoxElements(List<MinionData> datas)
    {
        containerBox = new MinionContainer[datas.Count];

        for(int i = 0; i < datas.Count; i++)
        {
            GameObject container = Instantiate(minionContainerPrefab, layout);
            MinionContainer minionContainer = container.GetComponent<MinionContainer>();
            containerBox[i] = minionContainer;
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
                if(mContainerArray[i].GetComponentInChildren<MinionUiBox>() == null)
                {
                    GameObject uiBox = Instantiate(minionUiBoxPrefab, mContainerArray[i].transform);
                    uiBox.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                }

                minionContainer.SetData(datas[i]);
            }
            else
            {
                MinionUiBox mUiBox = mContainerArray[i].GetComponentInChildren<MinionUiBox>();
                Destroy(mUiBox.gameObject);
            }
        }
    }

    public void DoneEditing()
    {

    }
}
