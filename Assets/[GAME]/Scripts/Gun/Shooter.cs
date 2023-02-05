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
    #endregion

    #region Awake, Update
    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= period && isThereTarget())
        {
            timer = 0f;
            Shoot();
        }
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

    #endregion

    #region Init Method
    private void Init()
    {
        gunData = GetComponent<Gun>().gunData;
        range = gunData.range;
        period = gunData.shootingPeriod;
        speed = gunData.bulletSpeed;
        attackDamage = gunData.attackDamage;
        timer = period;
    }
    #endregion
}
