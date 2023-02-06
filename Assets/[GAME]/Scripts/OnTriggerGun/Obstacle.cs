using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : OnTriggerGun
{
    [SerializeField] int stickmanLose = 1;

    public override void OnTrigger()
    {
        EventManager.GunTransformEvent(-stickmanLose);
        EventManager.ObstacleJumpEvent();
    }
}