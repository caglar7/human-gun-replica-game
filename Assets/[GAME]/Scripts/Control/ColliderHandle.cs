using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHandle : MonoBehaviour
{
    [SerializeField] float initDisableTime = 1f;
    Gun[] guns;
    Gun enabledGun;
    public static bool isUsed;

    #region Awake, Init
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        guns = GetComponentsInChildren<Gun>();
        isUsed = false;
        EnableGunCollider(guns[0].gunData.name);
    }

    #endregion

    #region OnEnable, OnDisable

    private void OnEnable()
    {
        EventManager.CollidersDisabled += DisableForDuration;
    }

    private void OnDisable()
    {
        EventManager.CollidersDisabled -= DisableForDuration;
    }

    #endregion

    #region Methods

    /// <summary>
    /// disabling all colliders in player for duration
    /// </summary>
    /// <param name="duration"></param>
    private void DisableForDuration()
    {
        StartCoroutine(DisableForDurationCo());
    }

    IEnumerator DisableForDurationCo()
    {
        EnableColliders(false);
        yield return new WaitForSeconds(initDisableTime);
        EnableColliders(true);
    }

    private void EnableColliders(bool value)
    {
        enabledGun.enabled = value;
    }

    public void EnableGunCollider(string gunName)
    {
        foreach(Gun g in guns)
        {
            if (g.gunData.name == gunName)
            {
                g.GetComponent<Collider>().enabled = true;
                enabledGun = g;
            }
            else g.GetComponent<Collider>().enabled = false;
        }
    }

    #endregion
}
