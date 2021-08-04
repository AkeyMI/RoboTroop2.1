using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Minion_Name", menuName = "RoboTroop/MinionDefenceData", order = 1)]
public class MinionDefenceData : ScriptableObject
{
    public string minionName = default;
    public GameObject minionPrefab = default;
    public GameObject minionUi = default;

    public int lifeShield = 15;
    public float timeToReloadShield = 5f;
}
