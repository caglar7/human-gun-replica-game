using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

/// <summary>
///  shoot target where there is some cast on top of it
/// </summary>

namespace GAME
{
    public class ShootingTarget : MonoBehaviour
    {
        #region Properties
        [Header("Target Health")]
        [SerializeField] int health;

        [Header("Components")]
        TextMeshPro tmpro;
        Bullet bullet;
        Gun gun;
        ICollectableMovable[] collectableMovables;
        #endregion

        #region Awake
        private void Awake()
        {
            Init();
        }
        #endregion

        #region Trigger

        /// <summary>
        /// when bullet hits put bullet back to its pool, damage the stone
        /// when gun hits, call events for push back and removing stickman
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            bullet = other.GetComponent<Bullet>();
            gun = other.GetComponent<Gun>();

            #region Bullet Hits
            if (bullet)
            {
                GenerateDecal(bullet.transform.position);

                bullet.RemoveBullet();

                UpdateHealth(-bullet.GetBulletDamage());

                TextAnimate();
            }
            #endregion

            #region Player Hits

            if (gun)
            {
                if (GetComponent<LevelEndShootingTarget>())
                {
                    CanvasController.instance.SwitchCanvas(CanvasType.LevelEndMenu);

                    string xStr = transform.parent.GetComponent<LevelEndPart>().tm.text;
                    int x = xStr[xStr.Length - 1] - '0';

                    EventManager.LevelCompleteEvent(x);
                }
                else
                {
                    EventManager.StickmanUpdateEvent(-1, null, VisualMode.Remove);

                    EventManager.PlayerHitsTargetEvent();
                }
            } 
            #endregion
        }

        #endregion

        #region Health

        private void UpdateHealth(int added)
        {
            health = Mathf.Max(health + added, 0);
            tmpro.text = health.ToString();

            if (health == 0)
            {
                foreach (ICollectableMovable m in collectableMovables)
                {
                    m.Move();
                }

                GameObject blastEffect = PoolManager.instance.poolTargetRemoved.PullObjFromPool();
                blastEffect.transform.position = transform.position;

                gameObject.SetActive(false);
            }
        }

        public void SetInitHealth(int value)
        {
            health = value;
        }

        #endregion

        #region Animations, Effects

        /// <summary>
        /// generates a stone decals at position
        /// </summary>
        /// <param name="pos"></param>
        private void GenerateDecal(Vector3 pos, float duration = 1f)
        {
            GameObject clone = PoolManager.instance.poolBulletDecal.PullObjFromPool();

            pos.z = tmpro.transform.position.z;
            clone.transform.position = pos;
            StartCoroutine(RemoveDecalCo(clone, duration));
        }

        IEnumerator RemoveDecalCo(GameObject g, float duration)
        {
            yield return new WaitForSeconds(duration);
            PoolManager.instance.poolBulletDecal.AddObjToPool(g);
        }

        private void TextAnimate()
        {
            tmpro.transform.DOKill();
            tmpro.transform.DOPunchScale(tmpro.transform.localScale * 1.1f, .2f);
        }

        #endregion

        #region Init Method
        private void Init()
        {
            tmpro = GetComponentInChildren<TextMeshPro>();
            collectableMovables = GetComponentsInChildren<ICollectableMovable>();

            tmpro.text = health.ToString();
        }
        #endregion
    }

}