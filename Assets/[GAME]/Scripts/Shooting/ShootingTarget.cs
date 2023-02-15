using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

/// <summary>
///  shoot target where there is some cast on top of it
/// </summary>

public class ShootingTarget : MonoBehaviour
{
    #region Properties
    [Header("Target Health")]
    [SerializeField] int health;

    [Header("Components")]
    TextMeshPro tmpro;
    Bullet bullet;
    Gun gun;
    Collectable_JumpDown[] collectableJumps;
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

        if (bullet)
        {
            GenerateDecal(bullet.transform.position);

            bullet.RemoveBullet();

            UpdateHealth(-bullet.GetBulletDamage());

            TextAnimate();
        }

        if(gun)
        {
            EventManager.StickmanUpdateEvent(-1, null, VisualMode.Remove);
            EventManager.PlayerHitsTargetEvent();
        }
    }


    #endregion

    #region Health

    private void UpdateHealth(int added)
    {
        health = Mathf.Max(health + added, 0);

        tmpro.text = health.ToString();

        if(health == 0)
        {
            foreach(Collectable_JumpDown cj in collectableJumps)
            {
                cj.transform.SetParent(null);
                cj.JumpDown();
            }
            
            // stone blast effect here

            gameObject.SetActive(false);

        }
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

        print("1");
    }

    #endregion

    #region Init Method
    private void Init()
    {
        tmpro = GetComponentInChildren<TextMeshPro>();
        tmpro.text = health.ToString();
        collectableJumps = GetComponentsInChildren<Collectable_JumpDown>();
    } 
    #endregion
}
