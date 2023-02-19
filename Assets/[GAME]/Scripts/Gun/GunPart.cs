using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  assigned to each gun part (stickmans)
///  mostly used for accessing those stickmans
/// </summary>

namespace GAME
{
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
        /// <summary>
        /// show hide stickmans on gun by toggling renderer enabled
        /// </summary>
        /// <param name="value"></param>
        public void EnableRenderer(bool value)
        {
            rend.enabled = value;
        }

        #endregion
    }
}
