using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    Collectable_JumpDown collectableJump;
    #endregion

    #region Awake
    private void Awake()
    {
        Init();
    }
    #endregion

    #region Collision

    private void OnTriggerEnter(Collider other)
    {
        bullet = other.GetComponent<Bullet>();
        gun = other.GetComponent<Gun>();
        if (bullet)
        {
            bullet.RemoveBullet();
            UpdateHealth(-bullet.GetBulletDamage());
        }

        if(gun)
        {
            // push player back
            // ...
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
            collectableJump.transform.SetParent(null);
            collectableJump.JumpDown();

            // effect here
            // ...

            gameObject.SetActive(false);

        }
    }

    #endregion

    #region Init Method
    private void Init()
    {
        tmpro = GetComponentInChildren<TextMeshPro>();
        tmpro.text = health.ToString();
        collectableJump = GetComponentInChildren<Collectable_JumpDown>();
    } 
    #endregion
}
