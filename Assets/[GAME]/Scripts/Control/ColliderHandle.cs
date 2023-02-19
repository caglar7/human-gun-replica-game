using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  enabling, disabling colliders
/// </summary>

namespace GAME
{
    public class ColliderHandle : MonoBehaviour
    {
        #region Properties
        [SerializeField] float initDisableTime = 1f;
        Gun[] guns;
        Gun enabledGun;
        public static bool isUsed;
        bool disableCalled = false;
        #endregion

        #region Awake, Init
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            guns = GetComponentsInChildren<Gun>();
            isUsed = false;
            SetEnabledGun(guns[0]);
        }

        #endregion

        #region OnEnable, OnDisable

        /// <summary>
        /// subs to events
        /// </summary>
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
            disableCalled = true;
            EnableColliders(false);

            yield return new WaitForSeconds(initDisableTime);

            EnableColliders(true);
            disableCalled = false;
        }
        
        /// <summary>
        /// enable or disable collider on the current gun
        /// </summary>
        /// <param name="value"></param>
        public void EnableColliders(bool value)
        {
            if (value)
            {
                foreach (Gun g in guns)
                {
                    if (g.gunData.name == enabledGun.gunData.name)
                        enabledGun.GetComponent<Collider>().enabled = true;
                    else g.GetComponent<Collider>().enabled = false;
                }
            }
            else
                enabledGun.GetComponent<Collider>().enabled = false;
        }

        /// <summary>
        /// same as currentGun
        /// </summary>
        /// <param name="g"></param>
        public void SetEnabledGun(Gun g)
        {
            enabledGun = g;

            if (!disableCalled) EnableColliders(true);
        }

        #endregion
    }

}