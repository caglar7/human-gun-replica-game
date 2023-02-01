using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  initial gun state where there is only a stickman
///  staring with idle and goes to running animations
/// </summary>

public class Stickman : Gun
{
    #region Properties
    [SerializeField] Animator animator; 
    #endregion

    #region Start
    private void Start()
    {
        EventManager.StartGame += RunAnimation;
    } 
    #endregion

    #region Animation Methods
    private void RunAnimation() => animator.SetTrigger("Run");

    private void IdleAnimation() => animator.SetTrigger("Idle");

    #endregion
}
