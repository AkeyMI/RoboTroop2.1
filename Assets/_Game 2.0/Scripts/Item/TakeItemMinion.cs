using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect_Name", menuName = "RoboTroop/Effects/TakeItemMinion", order = 1)]
public class TakeItemMinion : Effect
{
    [SerializeField] MinionItemData data = default;

    public override void Apply()
    {
        FindObjectOfType<MinionController>().ChangeItemMinion(data);
    }
}
