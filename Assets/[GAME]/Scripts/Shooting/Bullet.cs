using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// bullet methods
/// </summary>

public class Bullet : MonoBehaviour
{
    public void Shoot(float range, float speed)
    {
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
}
