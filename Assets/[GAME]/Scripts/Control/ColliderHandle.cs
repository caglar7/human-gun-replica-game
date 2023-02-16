using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHandle : MonoBehaviour
{
    [SerializeField] float disableDuration = 1f;
    Collider[] colliders;
    public static bool isUsed;

    #region Awake, Init
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        colliders = GetComponentsInChildren<Collider>();
        isUsed = false;
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
        yield return new WaitForSeconds(disableDuration);
        EnableColliders(true);
    }

    private void EnableColliders(bool value)
    {
        foreach(Collider c in colliders)
        {
            c.enabled = value;
        }
    }

    #endregion
}
