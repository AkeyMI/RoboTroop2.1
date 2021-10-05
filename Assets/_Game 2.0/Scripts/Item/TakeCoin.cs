using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect_Name", menuName = "RoboTroop/Effects/TakeCoin", order = 1)]

public class TakeCoin : Effect
{
    [SerializeField] int value;
    public override void Apply()
    {
        FindObjectOfType<PlayerData>().ManageBudget(value);
    }

}
