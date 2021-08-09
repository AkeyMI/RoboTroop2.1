using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionItemUi : MonoBehaviour
{
    [SerializeField] Image fillAmount = default;

    public Image GetFillAmount()
    {
        return fillAmount;
    }
}
