using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// bullet methods
/// </summary>

public class Bullet : MonoBehaviour
{
    int bulletDamage;

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
}
