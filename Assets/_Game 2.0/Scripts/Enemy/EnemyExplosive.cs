using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class EnemyExplosive : EnemyBaseState
{
    EnemyController enemyController;
    bool thereIsNotPLayer;
    Collider playerCollider;
    bool startAutoDestruction = false;
    float explotionRange = 6;
    public override void EnterState(EnemyController enemy)
    {
        enemyController = enemy;
        //explotionRange = enemyController.Stats.distanceAttack * 1.5f;
    }

    public override void Update(EnemyController enemy)
    {
        playerCollider = enemyController.ObjectToAttack;

        if(!enemy.isDead)
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
        enemyController.sp.GetSound(enemyController.shotSound);
        enemyController.isDead = true;
        startAutoDestruction = true;
        enemyController._gameObject.SetActive(true);
        yield return new WaitForSeconds(enemyController.Stats.attackSpeed - 0.3f);
        enemyController.sp.GetParticle(15, enemyController.transform.position);
        yield return new WaitForSeconds(0.3f);
        Collider[] players = Physics.OverlapSphere(enemyController.transform.position, explotionRange);

        foreach (var player in players)
        {
            var damageable = player.GetComponent<IDamagable>();
            if (damageable != null)
                damageable.Damage(enemyController.Stats.damage);
        }

        enemyController.Death();
    }
    public override void DrawGizmos()
    {
#if UNITY_EDITOR
        base.DrawGizmos();
        Color _color = Color.red;
        _color.a = 0.25f;
        Handles.color = _color;
        Handles.DrawSolidDisc(enemyController.transform.position, enemyController.transform.up, explotionRange);
#endif
    }

}
