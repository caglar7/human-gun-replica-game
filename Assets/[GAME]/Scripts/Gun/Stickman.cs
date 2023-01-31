using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  initial gun state where there is only a stickman
///  staring with idle and goes to running animations
/// </summary>

public class Stickman : Gun
{
    [SerializeField] Animator animator;

    public void TriggerAnimation(string param)
    {
        animator.SetTrigger(param);
    }
}
