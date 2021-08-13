using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect_Name", menuName = "RoboTroop/Effects/KamikaseEffect", order = 1)]
public class KamikaseEffect : Effect
{
    [SerializeField] MinionItemData data = default;

    public override void Apply()
    {
        throw new System.NotImplementedException();
    }
}
