using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletEffect : ScriptableObject
{
    public abstract void Apply(GameObject bullet, GameObject enemyHit);
}
