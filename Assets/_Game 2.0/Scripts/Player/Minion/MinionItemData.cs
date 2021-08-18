using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Minion_Name", menuName = "RoboTroop/MinionItemData", order = 1)]
public class MinionItemData : ScriptableObject
{
    public string minionName = default;
    public GameObject minionPrefab = default;
    public GameObject minionUi = default;
    public Effect effect = default;
    public int reloadUlti = 3;
}
