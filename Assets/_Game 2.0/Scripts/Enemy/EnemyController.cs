using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDamagable
{
    [SerializeField] GameObject spawnPointBullet = default;
    [SerializeField] EnemyStats enemyStats = default;
    [SerializeField] NavMeshAgent agent = default;
    [SerializeField] Animator animator;
    [SerializeField] AudioClip shootSound = default;
    [SerializeField] AudioSource audioSource = default;
    [SerializeField] GameObject sound = default;
    [SerializeField] GameObject particulas = default;
    //private EnemyStats currentStats;

    private WaveController waveController;

    private bool isDead = false;

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

    //public readonly EnemyPatrolState PatrolState = new EnemyPatrolState();
    public readonly EnemyHuntState HuntState = new EnemyHuntState();
    public readonly EnemyDistanceAttackState AttackDistanceState = new EnemyDistanceAttackState();
    //public readonly EnemyMeleeAttackState AttackMeleeState = new EnemyMeleeAttackState();

    private void Start()
    {
        //currentStats = enemyFusionStats.enemyFusionStats[0];
        life = enemyStats.life;
        rb = GetComponent<Rigidbody>();
        //ChangeSpawnPoint(this.gameObject.transform.position);
        TransitionToState(HuntState);
        animator = this.GetComponent<Animator>();
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

    public NaveController LocatePLayer()
    {
        NaveController nave = FindObjectOfType<NaveController>();

        return nave;
    }

    public void CreateBullet()
    {
        GameObject bullet = Instantiate(enemyStats.bullet, spawnPointBullet.transform.position, spawnPointBullet.transform.rotation);
        audioSource.PlayOneShot(shootSound);
        bullet.GetComponent<BulletEnemy>().Init(enemyStats.damage);
        animator.SetBool("Shooting", true);
        StartCoroutine(Shooting());
    }

    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(.1f);
        animator.SetBool("Shooting", false);
    }

    public void Damage(int amount)
    {
        life -= amount;
        Debug.Log("Enemigo Golpeado");
        if (life <= 0 && !isDead)
        {
            isDead = true;
            animator.SetTrigger("Hit");
            waveController.KilledEnemy();
            FindObjectOfType<MinionController>().ReloadUlti();
            Instantiate(sound);
            Instantiate<GameObject>(particulas).transform.position = this.transform.position;
            Destroy(this.gameObject);
        }
    }
}
