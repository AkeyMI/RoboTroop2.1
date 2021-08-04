using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect_Name", menuName = "RoboTroop/Effects/TakeShieldMinion", order = 1)]
public class TakeShieldMinion : Effect
{
    [SerializeField] MinionDefenceData data = default;

    public override void Apply()
    {
        FindObjectOfType<MinionController>().ChangeShieldMinion(data);
    }
}
