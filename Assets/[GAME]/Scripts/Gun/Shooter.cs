using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// component responsible for shooting,
/// does automatic shooting, every gun object contains this class
/// </summary>

public class Shooter : MonoBehaviour
{
    #region Properties
    [SerializeField] Transform shootPoint;
    [SerializeField] [Range(0f, 1f)] float recoilLevel;
    GunData gunData;
    RecoilPart[] recoilParts;
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
        }
    }

    #endregion

    #region Enable, Disable

    /// <summary>
    ///  subs to events
    /// </summary>
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

    /// <summary>
    /// send a raycast, check hits if there is a shooting target on the way
    /// </summary>
    /// <returns></returns>
    private bool isThereTarget()
    {

        RaycastHit[] hits = Physics.RaycastAll(shootPoint.position, Vector3.forward, range);

        foreach(RaycastHit hit in hits)
        {
            if (hit.collider.GetComponent<ShootingTarget>()) return true;
        }
        return false;
    }
    
    /// <summary>
    /// get a bullet from pool, set its position to shooting point
    /// then shoot, calling its method in Bullet.cs
    /// </summary>
    private void Shoot()
    {
        Bullet bullet = PoolManager.instance.poolBullet.PullObjFromPool().GetComponent<Bullet>();
        bullet.SetBulletType(gunData.bulletType);

        bullet.transform.position = shootPoint.position;
        bullet.Shoot(range, speed, attackDamage);

        StartCoroutine(RecoilCo(recoilLevel));
    }

    /// <summary>
    /// enabled with event, passing gun id to recognize which gun
    /// </summary>
    /// <param name="id"></param>
    public void EnableShooting(int id)
    {
        if (gunID == id) isActive = true;
        else isActive = false;
    }

    /// <summary>
    /// recoil animation, async
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    IEnumerator RecoilCo(float level)
    {
        foreach(RecoilPart r in recoilParts)
        {
            r.RecoilAnimation(level);
            yield return 0;
        }
    }

    #endregion

    #region Init Method
    private void Init()
    {
        gunData = GetComponent<Gun>().gunData;
        recoilParts = GetComponentsInChildren<RecoilPart>();
        range = gunData.range;
        period = gunData.shootingPeriod;
        speed = gunData.bulletSpeed;
        attackDamage = gunData.attackDamage;
        gunID = gunData.id;
        timer = period;
    }
    #endregion
}
