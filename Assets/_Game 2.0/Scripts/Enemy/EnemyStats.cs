using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_Name", menuName = "RoboTroop/Enemy", order = 1)]
public class EnemyStats : ScriptableObject
{
    public int life = 3;
    public float attackSpeed = 10f;
    public float distanceAttack = 5f;
    public float speed = 10f;
    public GameObject bullet = default;
    public int damage = 1;
    public bool explosive;
}
