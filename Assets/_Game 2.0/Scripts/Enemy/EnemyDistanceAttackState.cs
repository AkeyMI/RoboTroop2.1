using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistanceAttackState : EnemyBaseState
{
    private EnemyController enemyController;
    private bool thereIsNotPLayer;
    private Collider playerCollider;
    private float currentTime;

    public override void EnterState(EnemyController enemy)
    {
        enemyController = enemy;
    }

    public override void Update(EnemyController enemy)
    {
        PlayerStillCloseToAttack();
        CountDownToAttack();

        if (!thereIsNotPLayer)
        {
            Attack();
        }
    }

    private void PlayerStillCloseToAttack()
    {
        Collider[] players = Physics.OverlapSphere(enemyController.transform.position, enemyController.Stats.distanceAttack);

        foreach (var player in players)
        {
            if (player.CompareTag("Nave"))
            {
                thereIsNotPLayer = false;
                playerCollider = player;
                break;
            }
            else
            {
                thereIsNotPLayer = true;
            }
        }

        if (thereIsNotPLayer)
        {
            enemyController.TransitionToState(enemyController.HuntState);
        }
    }

    private void Attack()
    {
        Vector3 position = new Vector3(playerCollider.transform.position.x, enemyController.transform.position.y, playerCollider.transform.position.z);
        enemyController.transform.LookAt(position);

        if (currentTime <= 0)
        {
            enemyController.CreateBullet();
            currentTime = enemyController.Stats.attackSpeed;
        }
    }

    private void CountDownToAttack()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
    }
}
