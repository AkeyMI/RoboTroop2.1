using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionUiBox : MonoBehaviour
{
    [SerializeField] Image image = default;

    public void SetMinionData(MinionData data)
    {
        //Modificar UI con la data sprite
        image.sprite = data.sprite;
    }
}
