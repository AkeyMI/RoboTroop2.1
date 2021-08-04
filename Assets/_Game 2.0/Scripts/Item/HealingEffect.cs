using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect_Name", menuName = "RoboTroop/Effects/HealingEffect", order = 1)]
public class HealingEffect : Effect
{
    [SerializeField] int lifeToCure = 1;

    public override void Apply()
    {
        FindObjectOfType<NaveController>().HealNave(lifeToCure);
    }
}
