using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect_Name", menuName = "RoboTroop/Effects/TakeNaveShield", order = 1)]

public class TakeNaveShield : Effect
{
    public override void Apply()
    {
        FindObjectOfType<NaveController>().RepairShield();
    }
}
