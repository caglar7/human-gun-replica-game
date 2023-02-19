using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

/// <summary>
/// bullet class
/// </summary>

namespace GAME
{
    public class Bullet : MonoBehaviour
    {
        #region Properties
        BulletTypeControl[] bulletTypes;
        int bulletDamage; 
        #endregion

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

        #region Methods

        /// <summary>
        /// shoot method, tweening bullet range distance from
        /// shooting point
        /// </summary>
        /// <param name="range"></param>
        /// <param name="speed"></param>
        /// <param name="damage"></param>
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

        /// <summary>
        /// back to pool
        /// </summary>
        public void RemoveBullet()
        {
            transform.DOKill();
            PoolManager.instance.poolBullet.AddObjToPool(gameObject);
        }

        /// <summary>
        /// the object bullet hits get damage info from bullet
        /// </summary>
        /// <returns></returns>
        public int GetBulletDamage()
        {
            return bulletDamage;
        }

        /// <summary>
        ///  currently we have standard or shotgun bullets
        /// </summary>
        /// <param name="type"></param>
        public void SetBulletType(BulletType type)
        {
            for (int i = 0; i < bulletTypes.Length; i++)
            {
                if (bulletTypes[i].type == type) bulletTypes[i].gameObject.SetActive(true);
                else bulletTypes[i].gameObject.SetActive(false);
            }
        }

        #endregion
    }

    /// <summary>
    /// update this enum everytime there is a new bullet type
    /// </summary>
    public enum BulletType
    {
        Regular,
        Shotgun,
    }
}
