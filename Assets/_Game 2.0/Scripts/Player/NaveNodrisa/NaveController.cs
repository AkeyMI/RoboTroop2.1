using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class NaveController : MonoBehaviour, IDamagable
{
    [SerializeField] int life = 10;
    [SerializeField] int shieldLife = 10;
    [SerializeField] GameObject shield = default;
    [SerializeField] NavMeshAgent agent = default;
    Transform mainObjective;

    public event Action<int, int> onLifeChange;
    public event Action<int, int> onShieldChange;

    private int currentLife;
    private int currentShieldLife;
    private CharacterController player;

    private Stunable stunStatus;
    CameraController cam;
    private void Awake()
    {
        stunStatus = GetComponent<Stunable>();
    }

    private void Start()
    {
        currentLife = life;
        currentShieldLife = shieldLife;
        player = FindObjectOfType<CharacterController>();
        mainObjective = GameObject.Find("Nave Main Objective").transform;
        cam = FindObjectOfType<CameraController>();
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
        agent.SetDestination(mainObjective.position);
    }

    public void ShieldDamage(int amount)
    {
        currentShieldLife -= amount;

        onShieldChange?.Invoke(currentShieldLife, shieldLife);

        cam.Shake(2, 0.1f);

        if (currentShieldLife <= 0)
        {
            shield.SetActive(false);
        }
    }

    public void Damage(int amount)
    {
        if (currentShieldLife <= 0)
        {
            currentLife -= amount;

            onLifeChange?.Invoke(currentLife, life);

            cam.Shake(2.5f, 0.1f);
            if (currentLife <= 0)
            {
                SceneManager.LoadScene(3);
            }
        }
        else
            ShieldDamage(amount);
    }

    public void HealNave(int amount)
    {
        currentLife += amount;

        onLifeChange?.Invoke(currentLife, life);

        if (currentLife >= life)
        {
            currentLife = life;
        }
    }

    public void RepairShield(int amount)
    {
        currentShieldLife += amount;

        onShieldChange?.Invoke(currentShieldLife, shieldLife);

        if (currentShieldLife >= shieldLife)
        {
            currentShieldLife = shieldLife;
        }
    }
}
