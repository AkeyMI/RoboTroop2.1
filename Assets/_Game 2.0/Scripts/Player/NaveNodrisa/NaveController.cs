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
    [SerializeField] float timeDownForCure = 2f;
    [SerializeField] float cooldownToCure = 0.5f;
    [SerializeField] int autoHeal = 2;
    [SerializeField] ParticleSystem healUp;

    Transform mainObjective;

    public event Action<int, int> onLifeChange;
    public event Action<int, int> onShieldChange;

    private float currentTimeForCure;
    private float currentCooldownToCure;
    private bool canCure;
    private bool isHealing;
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
        currentTimeForCure = timeDownForCure;
        currentCooldownToCure = cooldownToCure;
        canCure = false;
        isHealing = false;
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

        if (currentLife < life && !isHealing)
        {
            CooldownForCure();
        }
        
        HealNave(autoHeal);

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
            currentTimeForCure = timeDownForCure;
            isHealing = false;
            canCure = false;
            //Debug.Log("No se puede curar");

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

    private void CooldownForCure()
    {
        currentTimeForCure -= Time.deltaTime;

        //Debug.Log("Tiempo para curar: " + currentTimeForCure);

        if(currentTimeForCure <= 0)
        {
            canCure = true;
        }
        else
        {
            canCure = false;
        }
    }

    public void HealNave(int amount)
    {
        if (canCure)
        {
            isHealing = true;
            //Debug.Log("Esta curando");

            currentCooldownToCure -= Time.deltaTime;

            if (currentCooldownToCure <= 0)
            {
                currentLife += amount;
                currentCooldownToCure = cooldownToCure;

                onLifeChange?.Invoke(currentLife, life);

                healUp.Play();
                if (currentLife >= life)
                {
                    currentLife = life;
                    isHealing = false;
                    canCure = false;
                }
            }
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
