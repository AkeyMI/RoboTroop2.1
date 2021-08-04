using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NaveController : MonoBehaviour, IDamagable
{
    [SerializeField] int life = 10;
    [SerializeField] int shieldLife = 10;
    [SerializeField] GameObject shield = default;
    [SerializeField] NavMeshAgent agent = default;

    private int currentLife;
    private int currentShieldLife;
    private CharacterController player;

    private Stunable stunStatus;

    private void Awake()
    {
        stunStatus = GetComponent<Stunable>();
    }

    private void Start()
    {
        currentLife = life;
        currentShieldLife = shieldLife;
        player = FindObjectOfType<CharacterController>();
    }

    private void Update()
    {
        if (stunStatus.IsStunned)
        {
            agent.isStopped = true;
            return;
        }
        else
        {
            agent.isStopped = false;
        }
       

        MoveNave();
    }

    private void MoveNave()
    {
        agent.SetDestination(player.transform.position);
    }

    public void ShieldDamage(int amount)
    {
        currentShieldLife -= amount;

        if(currentShieldLife <= 0)
        {
            shield.SetActive(false);
        }
    }

    public void Damage(int amount)
    {
        currentLife -= amount;

        if(currentLife <= 0)
        {
            //Dead
        }
    }

    public void HealNave(int amount)
    {
        currentLife += amount;

        if(currentLife >= life)
        {
            currentLife = life;
        }
    }

    public void RepairShield(int amount)
    {
        currentShieldLife += amount;

        if(currentShieldLife >= shieldLife)
        {
            currentShieldLife = shieldLife;
        }
    }
}
