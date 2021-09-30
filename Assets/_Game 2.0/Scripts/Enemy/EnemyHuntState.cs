using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHuntState : EnemyBaseState
{
    private EnemyController enemyController;
    private float speed;
    private Vector3 newDirection;
    private Vector3 newPosition;
    //private bool thereIsNotPLayer;
    private Collider playercol;
    private NavMeshAgent agent;

    public override void EnterState(EnemyController enemy)
    {
        enemyController = enemy;
        //enemyController.LocatePLayer();
        Debug.Log("Entro en caza");
    }

    public override void Update(EnemyController enemy)
    {
        WereIsThePLayer();
        PlayerIsCloseToAttack();

        HuntingPLayer(playercol);
    }

    private void WereIsThePLayer()
    {
        //NaveController player = enemyController.LocatePLayer();

        //enemyController.LocatePLayer();
        playercol = enemyController.ObjectToAttack;

        //playercol = player.GetComponent<Collider>();
    }

    private void PlayerIsCloseToAttack()
    {
        //Collider[] players = Physics.OverlapSphere(enemyController.transform.position, enemyController.Stats.distanceAttack);

        //foreach (var player in players)
        //{
        //    if (player.CompareTag("Nave"))
        //    {
        //        //ChangeToAttack();
        //        enemyController.TransitionToState(enemyController.AttackDistanceState);
        //    }
        //    else if(player.CompareTag("AtkMinion"))
        //    {
        //        enemyController.TransitionToState(enemyController.AttackDistanceState);
        //    }
        //}

        float dist = Vector3.Distance(playercol.transform.position, enemyController.transform.position);

        if(dist <= enemyController.Stats.distanceAttack)
        {
            enemyController.TransitionToState(enemyController.AttackDistanceState);
        }
    }

    private void HuntingPLayer(Collider player)
    {
        //speed = enemyController.Stats.speed * Time.deltaTime;
        //newPosition = player.transform.position;
        //newPosition.y = enemyController.transform.position.y;
        //newDirection = newPosition - enemyController.transform.position;
        //enemyController.transform.LookAt(newPosition);
        //enemyController.transform.position += newDirection * speed;

        if (!enemyController.IsATorreta)
        {
            agent = enemyController.Agent;

            agent.SetDestination(player.transform.position);
        }
    }
}
