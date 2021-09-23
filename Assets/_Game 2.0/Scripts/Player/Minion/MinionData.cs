using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Minion_Name", menuName = "RoboTroop/MinionData", order = 1)]
public class MinionData : ScriptableObject
{
    public string minionName = default;
    public GameObject minionPrefab = default;
    public GameObject bulletPrefab = default;
    public GameObject minionUI = default;

    public int damage = 1;
    public int ammo = 5;
    public float timeToReload = 1f;
    public float timeForAttack = 0.5f;
    public float bulletSpeed = 15f;
    public int life = 1;
}
