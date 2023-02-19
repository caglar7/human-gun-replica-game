using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  enable, disabling colliders
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

        public void SetEnabledGun(Gun g)
        {
            enabledGun = g;

            if (!disableCalled) EnableColliders(true);
        }

        #endregion
    }

}