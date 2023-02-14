using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    #region Properties
    [SerializeField] Transform shootPoint;
    GunData gunData;
    float period, timer;
    float range, speed;
    int attackDamage;
    bool isActive;
    int gunID;
    #endregion

    #region Awake, Update
    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (!isActive) return;

        timer += Time.deltaTime;
        if(timer >= period && isThereTarget())
        {
            timer = 0f;
            Shoot();

            print(transform.name + " shoot");
        }
    }

    #endregion

    #region Enable, Disable

    private void OnEnable()
    {
        EventManager.EnableGun += EnableShooting;
    }

    private void OnDisable()
    {
        EventManager.EnableGun -= EnableShooting;
    }

    #endregion

    #region Shooting Related

    private bool isThereTarget()
    {
        RaycastHit[] hits = Physics.RaycastAll(shootPoint.position, Vector3.forward, range);

        foreach(RaycastHit hit in hits)
        {
            if (hit.collider.GetComponent<ShootingTarget>()) return true;
        }
        return false;
    }

    private void Shoot()
    {
        Bullet bullet = PoolManager.instance.poolBullet.PullObjFromPool().GetComponent<Bullet>();
        bullet.transform.position = shootPoint.position;
        bullet.Shoot(range, speed, attackDamage);
    }

    public void EnableShooting(int id)
    {
        if (gunID == id) isActive = true;
        else isActive = false;

        // testing
        print(transform.name + ": " + isActive);
    }

    #endregion

    #region Init Method
    private void Init()
    {
        gunData = GetComponent<Gun>().gunData;
        range = gunData.range;
        period = gunData.shootingPeriod;
        speed = gunData.bulletSpeed;
        attackDamage = gunData.attackDamage;
        gunID = gunData.id;
        timer = period;
    }
    #endregion
}
