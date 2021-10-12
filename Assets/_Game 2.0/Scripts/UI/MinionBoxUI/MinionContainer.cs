using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MinionContainer : MonoBehaviour, IDropHandler
{
    private bool containerIsOccupied;
    private MinionUiBox minionUiBox;

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            minionUiBox = eventData.pointerDrag.GetComponent<MinionUiBox>();
            //minionUiBox.transform.SetParent(this.transform, false);
        }
    }

    public void SetData(MinionData data)
    {
        minionUiBox = GetComponentInChildren<MinionUiBox>();
        minionUiBox.SetMinionData(data);
        containerIsOccupied = true;
    }
}
