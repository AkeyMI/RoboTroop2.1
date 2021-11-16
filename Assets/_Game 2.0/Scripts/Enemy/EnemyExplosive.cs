using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosive : EnemyBaseState
{
    EnemyController enemyController;
    bool thereIsNotPLayer;
    Collider playerCollider;
    bool startAutoDestruction = false;
    float explotionRange;
    public override void EnterState(EnemyController enemy)
    {
        enemyController = enemy;
    }

    public override void Update(EnemyController enemy)
    {
        playerCollider = enemyController.ObjectToAttack;

        PlayerStillCloseToAttack();

        if (!thereIsNotPLayer && !startAutoDestruction)
        {
            startAutoDestruction = true;
            enemy.StartCoroutine(Autodestruction());
        }
    }

    private void PlayerStillCloseToAttack()
    {
        float squareDistance = Vector3.SqrMagnitude(playerCollider.transform.position - enemyController.transform.position);

        if (squareDistance <= enemyController.Stats.distanceAttack  * enemyController.Stats.distanceAttack)
            thereIsNotPLayer = false;       
        else
            thereIsNotPLayer = true;

        if (thereIsNotPLayer)
            enemyController.TransitionToState(enemyController.HuntState);
    }

    IEnumerator Autodestruction()
    {
        explotionRange = enemyController.Stats.distanceAttack *2;
        yield return new WaitForSeconds(enemyController.Stats.attackSpeed);
        Collider[] players = Physics.OverlapSphere(enemyController.transform.position, explotionRange);

        foreach (var player in players)
        {
            var damageable = player.GetComponent<IDamagable>();
            if (damageable != null)
                damageable.Damage(enemyController.Stats.damage);
        }

        enemyController.Damage(enemyController.Stats.life);
    }
}
