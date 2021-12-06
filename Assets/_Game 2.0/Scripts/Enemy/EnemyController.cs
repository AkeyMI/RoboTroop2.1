using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour, IDamagable
{
    [SerializeField] GameObject [] spawnPointBullet = default;
    [SerializeField] EnemyStats enemyStats = default;
    [SerializeField] NavMeshAgent agent = default;
    [SerializeField] Animator animator;
    [SerializeField] AudioClip shootSound = default;
    [SerializeField] AudioSource audioSource = default;
    [SerializeField] GameObject sound = default;
    [SerializeField] int particulas;
    [SerializeField] bool isATorreta = default;
    [SerializeField] Image reloadBarImage = default;

    [Header("")]
    public GameObject _gameObject;
    [HideInInspector]
    public SpawnerPool sp;

    private WaveController waveController;

    [HideInInspector]
    public bool isDead = false;

    public bool IsATorreta => isATorreta;

    public Rigidbody Rb => rb;
    public EnemyStats Stats => enemyStats;

    public NavMeshAgent Agent => agent;

    //public Vector3 PositionSpawn => positionSpawn;

    private EnemyBaseState currentState;
    //private EnemyBaseState currentAttackState;
    private Rigidbody rb;
    private Vector3 positionSpawn;
    private int life;
    //private float timeToChangeType = 6f;

    private int cargador;
    private float currentTimeToReload;

    //public readonly EnemyPatrolState PatrolState = new EnemyPatrolState();
    public readonly EnemyHuntState HuntState = new EnemyHuntState();
    public readonly EnemyDistanceAttackState AttackDistanceState = new EnemyDistanceAttackState();
    //public readonly EnemyMeleeAttackState AttackMeleeState = new EnemyMeleeAttackState();

    public readonly EnemyExplosive enemyExplosiveState = new EnemyExplosive();

    private Collider objectToAttack;

    public Collider ObjectToAttack => objectToAttack;

    private void Start()
    {
        //currentStats = enemyFusionStats.enemyFusionStats[0];
        life = enemyStats.life;
        rb = GetComponent<Rigidbody>();
        //ChangeSpawnPoint(this.gameObject.transform.position);
        TransitionToState(HuntState);
        animator = this.GetComponent<Animator>();
        LocatePLayer();
        sp = FindObjectOfType<SpawnerPool>();
        cargador = enemyStats.cargador;
        currentTimeToReload = enemyStats.timeToReload;
    }

    public void Init(WaveController waveController)
    {
        this.waveController = waveController;
        waveController.AddEnemy();
    }

    private void Update()
    {
        if (!isDead)
        {
            currentState.Update(this);
        }
    }

    public void TransitionToState(EnemyBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    //public void ChangeSpawnPoint(Vector3 vec)
    //{
    //    positionSpawn = new Vector3(vec.x, vec.y, vec.z);
    //}

    public void LocatePLayer()
    {
        NaveController nave = FindObjectOfType<NaveController>();

        CharacterController player = FindObjectOfType<CharacterController>();

        float enemyNave = Vector3.Distance(nave.transform.position, this.transform.position);
        float enemyPlayer = Vector3.Distance(player.transform.position, this.transform.position);

        if(enemyNave < enemyPlayer)
        {
            objectToAttack = nave.GetComponent<Collider>();
            //return nave.GetComponent<Collider>();
        }
        else
        {
            objectToAttack = player.GetComponent<Collider>();
            //return player.GetComponent<Collider>();
        }
    }

    public void CreateBullet()
    {
        if (cargador > 0)
        {
            foreach (GameObject go in spawnPointBullet)
            {
                GameObject bullet = Instantiate(enemyStats.bullet, go.transform.position, go.transform.rotation);
                bullet.GetComponent<BulletEnemy>().Init(enemyStats.damage);
            }
            cargador--;

            audioSource.PlayOneShot(shootSound);
            animator.SetBool("Shooting", true);
            StartCoroutine(Shooting());
        }
        else
        {
            ReloadGun();
            Debug.Log("Esta recargadon el enemigo");
        }
    }

    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(.1f);
        animator.SetBool("Shooting", false);
    }

    private void ReloadGun()
    {
        currentTimeToReload = enemyStats.timeToReload;
        reloadBarImage.enabled = true;
        while (currentTimeToReload > 0)
        {
            currentTimeToReload -= Time.deltaTime;
            reloadBarImage.fillAmount = (currentTimeToReload / enemyStats.timeToReload);
        }

        cargador = enemyStats.cargador;
        reloadBarImage.enabled = false;
        reloadBarImage.fillAmount = 1;
        Debug.Log("Termino de recargar");
    }

    public void Damage(int amount)
    {
        life -= amount;
        objectToAttack = FindObjectOfType<CharacterController>().GetComponent<Collider>();
        animator.SetTrigger("Hit");
        if (life <= 0 && !isDead)
        {
            isDead = true;
            Death();
        }
    }

    public void Particles(Vector3 vector)
    {
        sp.GetParticle(11, vector);
    }
    public void Death()
    {
        waveController.KilledEnemy();
        FindObjectOfType<MinionController>().ReloadUlti();
        Instantiate(sound);
        sp.GetParticle(particulas, transform.position);
        Destroy(this.gameObject);
    }
}
