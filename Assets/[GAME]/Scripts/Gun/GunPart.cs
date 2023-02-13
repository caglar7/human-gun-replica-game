using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  assigned to each gun part (stickmans)
///  mostly used for accessing those stickmans
/// </summary>

public class GunPart : MonoBehaviour
{
    #region Properties
    Renderer rend;
    [HideInInspector] public Transform[] stickmanLimbs;
    [HideInInspector] public Color color;
    #endregion

    #region Awake, Init
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        rend = GetComponentInChildren<Renderer>();
        stickmanLimbs = GetComponentsInChildren<Transform>();
        color = GetComponentInChildren<Renderer>().material.color;
    }
    #endregion

    #region Methods

    public void EnableRenderer(bool value)
    {
        rend.enabled = value;
    }

    #endregion
}
