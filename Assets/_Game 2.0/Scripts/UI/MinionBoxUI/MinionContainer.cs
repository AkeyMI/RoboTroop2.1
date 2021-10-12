using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MinionContainer : MonoBehaviour, IDropHandler
{
    private MinionUiBox minionUiBox;

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            MinionUiBox newMinionUiBox = eventData.pointerDrag.GetComponent<MinionUiBox>();
            if (minionUiBox != null)
            {
                minionUiBox.SetMinionContainer(newMinionUiBox.MContainer);
                minionUiBox.transform.SetParent(newMinionUiBox.MContainer.transform, false);
            }
            newMinionUiBox.transform.SetParent(this.transform, false);
            newMinionUiBox.SetMinionContainer(this);

            minionUiBox = newMinionUiBox;
        }
    }

    public void SetData(MinionData data)
    {
        minionUiBox = GetComponentInChildren<MinionUiBox>();
        minionUiBox.SetMinionData(data);
        minionUiBox.SetMinionContainer(this);
    }
}
