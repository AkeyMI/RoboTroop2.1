using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect_Name", menuName = "RoboTroop/BulletEffects/ExplosiveEffect", order = 1)]
public class ExplosiveBulletEffect : BulletEffect
{
    public float distanceExplosion = 2f;

    public override void Apply(GameObject bullet, GameObject enemyHit)
    {
        Collider[] enemies = Physics.OverlapSphere(bullet.transform.position, distanceExplosion);

        foreach (var enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<EnemyController>().Damage(bullet.GetComponent<Bullet>().Damage / 2);
                //Debug.Log(bullet.GetComponent<Bullet>().Damage / 2);
            }
        }
    }
}
