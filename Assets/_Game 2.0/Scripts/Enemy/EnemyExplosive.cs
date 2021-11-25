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
        explotionRange = enemyController.Stats.distanceAttack * 1.5f;
    }

    public override void Update(EnemyController enemy)
    {
        playerCollider = enemyController.ObjectToAttack;

        PlayerStillCloseToAttack();


        if ((!thereIsNotPLayer || enemyController.isDead) && !startAutoDestruction)
        {
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
        enemyController.isDead = true;
        startAutoDestruction = true;
        yield return new WaitForSeconds(enemyController.Stats.attackSpeed);
        Collider[] players = Physics.OverlapSphere(enemyController.transform.position, explotionRange);

        foreach (var player in players)
        {
            var damageable = player.GetComponent<IDamagable>();
            if (damageable != null)
                damageable.Damage(enemyController.Stats.damage);
        }

        enemyController.Death();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(enemyController.transform.position, explotionRange);  // o .DrawWireSphere
    }
}
