using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootController : MonoBehaviour, IDamagable
{
    //[SerializeField] GameObject bulletPrefab = default;
    [SerializeField] GameObject[] spawnBullet = default;
    //[SerializeField] float timeForAttack = 0.5f;
    //[SerializeField] int ammo = 5;
    //[SerializeField] float timeToReload = 1f;
    //[SerializeField] int damage = 1;
    //[SerializeField] ItemDistance item = default;
    [SerializeField] MinionData data;
    [SerializeField] float timeShootOff;
    [SerializeField] Image reloadBarImage = default;
    [SerializeField] AudioClip shootSound = default;
    [SerializeField] AudioSource audioSource = default;
    [SerializeField] bool automatic;
    [SerializeField] int bulletsXShoot;
    [SerializeField] float timeBetweenShoots;
    [SerializeField] Mesh gizmoMesh;
    Animator animator;
    private int currentAmmo;
    private bool isReloading = false;

    public MinionData Data => data;

    private float timeOfLastAttack;

    private int currentLife;

    private void Start()
    {
        //currentAmmo = item.ammo;
        animator = GetComponent<Animator>();
        currentAmmo = data.ammo;
        currentLife = data.life;
    }

    private void Update()
    {
        //if (!CanShoot()) return;

        ShootOrReload();

        if(Input.GetKeyDown(KeyCode.M))
        {
            Die();
        }

    }

    private void OnEnable()
    {
        isReloading = false;
    }

    private void ShootOrReload()
    {
        if (Input.GetMouseButton(0))
        {
            
            if (isReloading)
            {
                return;
            }

            if (currentAmmo > 0)
            {
                if (automatic) 
                    animator.SetBool("Shooting", true);
                Shoot();
            }
            else
            {
                StartCoroutine(Reload());
            }
        }
        else 
            if(automatic) 
                animator.SetBool("Shooting", false);

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < data.ammo)
        {
            StartCoroutine(Reload());
        }

    }
    IEnumerator Shooting()
    {
        animator.SetBool("Shooting", true);
        yield return new WaitForSeconds(timeShootOff);
        animator.SetBool("Shooting", false);
    }
    IEnumerator Reload()
    {
        if (automatic)
            animator.SetBool("Shooting", false);
       
        isReloading = true;
        float currentTime = data.timeToReload;
        reloadBarImage.enabled = true;

        //yield return new WaitForSeconds(data.timeToReload);

        while(currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            reloadBarImage.fillAmount = (currentTime / data.timeToReload);
            yield return null;
        }

        currentAmmo = data.ammo;

        isReloading = false;

        reloadBarImage.enabled = false;
        reloadBarImage.fillAmount = 1;
    }

    private void Shoot()
    {
        if (Time.time > timeOfLastAttack)
        {
            if (bulletsXShoot > 1)
            {
                StartCoroutine(Rafaga());
            }
            else
            {
                foreach (GameObject i in spawnBullet)
                {
                    GameObject bullet = Instantiate(data.bulletPrefab, i.transform.position, i.transform.rotation);
                    bullet.GetComponent<Bullet>().Init(data.damage, data.bulletSpeed);
                }
                audioSource.PlayOneShot(shootSound);
                timeOfLastAttack = Time.time + data.timeForAttack;
                currentAmmo--;
                if (!automatic)
                    StartCoroutine(Shooting());
            }
        }
    }

    IEnumerator Rafaga()
    {
        for (int i = 0; i < bulletsXShoot; i++)
        {
            foreach (GameObject y in spawnBullet)
            {
                GameObject bullet = Instantiate(data.bulletPrefab, y.transform.position, y.transform.rotation);
                bullet.GetComponent<Bullet>().Init(data.damage, data.bulletSpeed);
            }
            audioSource.PlayOneShot(shootSound);
            timeOfLastAttack = Time.time + data.timeForAttack;
            currentAmmo--;
            if (!automatic)
                StartCoroutine(Shooting());
            yield return new WaitForSeconds(timeBetweenShoots);
        }

    }

    public void Damage(int amount)
    {
        currentLife -= amount;

        if(currentLife <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<MinionController>().NextMinion();
    }

    //public void ChangeItem(Item item)
    //{
    //    this.item = (ItemDistance)item;
    //}

    private void OnDrawGizmos()
    {
        foreach (GameObject f in spawnBullet)
        {
            Gizmos.DrawMesh(gizmoMesh, f.transform.position, f.transform.rotation);
            Gizmos.color = Color.yellow;
        }
    }

}
