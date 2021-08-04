using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    //[SerializeField] GameObject bulletPrefab = default;
    [SerializeField] GameObject spawnBullet = default;
    //[SerializeField] float timeForAttack = 0.5f;
    //[SerializeField] int ammo = 5;
    //[SerializeField] float timeToReload = 1f;
    //[SerializeField] int damage = 1;
    //[SerializeField] ItemDistance item = default;
    [SerializeField] Animator animator;
    [SerializeField] MinionData data;

    private int currentAmmo;
    private bool isReloading = false;

    private float timeOfLastAttack;

    private void Start()
    {
        //currentAmmo = item.ammo;
        currentAmmo = data.ammo;
    }

    private void Update()
    {
        //if (!CanShoot()) return;

        ShootOrReload();
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
                Shoot();
            }
            else
            {
                StartCoroutine(Reload());
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < data.ammo)
        {
            StartCoroutine(Reload());
        }
    }
    IEnumerator Shooting()
    {
        animator.SetBool("Shooting", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Shooting", false);
    }
    IEnumerator Reload()
    {
        Debug.Log("Esta recargando");
        isReloading = true;

        yield return new WaitForSeconds(data.timeToReload);

        currentAmmo = data.ammo;

        isReloading = false;
    }

    private void Shoot()
    {
        if (Time.time > timeOfLastAttack)
        {
            GameObject bullet = Instantiate(data.bulletPrefab, spawnBullet.transform.position, spawnBullet.transform.rotation);
            bullet.GetComponent<Bullet>().Init(data.damage, data.bulletSpeed);
            timeOfLastAttack = Time.time + data.timeForAttack;
            currentAmmo--;
            StartCoroutine(Shooting());
        }
    }

    //public void ChangeItem(Item item)
    //{
    //    this.item = (ItemDistance)item;
    //}
}
