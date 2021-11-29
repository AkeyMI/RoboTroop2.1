using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect_Name", menuName = "RoboTroop/Effects/TakeAtkMinion", order = 1)]
public class TakeAtkMinionEffect : Effect
{
    [SerializeField] MinionData data = default;

    public override void Apply()
    {
        FindObjectOfType<MinionController>().ChangeAtkMinion(data);
        FindObjectOfType<DataCollector>().SavePedestal(data.minionName);
    }
}
