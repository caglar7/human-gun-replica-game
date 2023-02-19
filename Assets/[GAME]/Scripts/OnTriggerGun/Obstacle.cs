using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAME
{
    public class Obstacle : OnTriggerGun
    {
        [SerializeField] int stickmanLose = 1;

        public override void OnTrigger()
        {
            EventManager.ObstacleJumpEvent();
            EventManager.StickmanUpdateEvent(-stickmanLose, null, VisualMode.Remove);
        }
    }
}
