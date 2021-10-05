using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionContainer : MonoBehaviour
{
    public void SetData(MinionData data)
    {
        GetComponentInChildren<MinionUiBox>().SetMinionData(data);
    }
}
