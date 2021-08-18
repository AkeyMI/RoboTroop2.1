using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KamikaseMovement : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent = default;
    [SerializeField] float distanceToExplote = 0.2f;
    [SerializeField] float distanceExplosion = 2f;
    [SerializeField] int damage = 5;

    private EnemyController[] enemies;

    private EnemyController enemyToAttack;

    private float closeEnemy;

    private bool minionHaveATarget;

    private void Start()
    {
        //enemies = FindObjectsOfType<EnemyController>();
        closeEnemy = 100;
    }

    private void Update()
    {
        if(!TargetIsStillAlive())
            GetObjetiveEnemy();

        if (enemyToAttack == null) return;

        AttackEnemy();

        Explote();
    }

    private void GetObjetiveEnemy()
    {
        enemies = FindObjectsOfType<EnemyController>();

        for (int i = 0; i < enemies.Length; i++)
        {
            float currentCloseEnemy = Vector3.Distance(this.transform.position , enemies[i].transform.position);

            if(currentCloseEnemy < closeEnemy)
            {
                closeEnemy = currentCloseEnemy;
                enemyToAttack = enemies[i];
            }
        }

        if(enemyToAttack == null)
        {
            Destroy(this.gameObject);
        }
    }

    private bool TargetIsStillAlive()
    {
        if(enemyToAttack == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void AttackEnemy()
    {
        agent.SetDestination(enemyToAttack.transform.position);
    }

    private void Explote()
    {
        float distance = Vector3.Distance(this.transform.position, enemyToAttack.transform.position);

        if(distance <= distanceToExplote)
        {
            Collider[] enemies = Physics.OverlapSphere(this.transform.position, distanceExplosion);

            foreach (var enemy in enemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    enemy.GetComponent<EnemyController>().Damage(damage);
                }
            }
            Debug.Log("Exploto");
            Destroy(this.gameObject);
        }
    }
}
