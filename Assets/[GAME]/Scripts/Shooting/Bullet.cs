using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

/// <summary>
/// bullet methods
/// </summary>

public class Bullet : MonoBehaviour
{
    BulletTypeControl[] bulletTypes;
    int bulletDamage;

    #region Awake, Init
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        bulletTypes = GetComponentsInChildren<BulletTypeControl>(true);
    } 
    #endregion

    public void Shoot(float range, float speed, int damage)
    {
        bulletDamage = damage;
        float duration = range / speed;
        float nextZ = transform.position.z + range;
        transform.DOMoveZ(nextZ, duration).SetEase(Ease.Linear)
            .OnComplete(() => {
                RemoveBullet();
            });
    }

    public void RemoveBullet()
    {
        transform.DOKill();
        PoolManager.instance.poolBullet.AddObjToPool(gameObject);
    }

    public int GetBulletDamage()
    {
        return bulletDamage;
    }

    public void SetBulletType(BulletType type)
    {
        for (int i = 0; i < bulletTypes.Length; i++)
        {
            if (bulletTypes[i].type == type) bulletTypes[i].gameObject.SetActive(true);
            else bulletTypes[i].gameObject.SetActive(false);
        }
    }
}

/// <summary>
/// update this enum everytime there is a new bullet type
/// </summary>
public enum BulletType
{
    Regular,
    Shotgun,
}
