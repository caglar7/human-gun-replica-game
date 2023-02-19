using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  initial gun state where there is only a stickman
///  staring with idle and goes to running animations
/// </summary>

namespace GAME
{
    public class Stickman : Gun
    {
        #region Properties

        [Header("Stickman Params")]
        [SerializeField] Animator animator;
        #endregion

        #region Awake

        private void Awake()
        {
            Init();
        }

        #endregion

        #region Enable, Disable

        /// <summary>
        ///  subs to events
        /// </summary>
        private void OnEnable()
        {
            EventManager.StartGame += RunAnimation;
        }

        private void OnDisable()
        {
            EventManager.StartGame -= RunAnimation;
        }

        #endregion

        #region Animation Methods
        private void RunAnimation() => animator.SetTrigger("Run");

        private void IdleAnimation() => animator.SetTrigger("Idle");

        #endregion
    }
}
