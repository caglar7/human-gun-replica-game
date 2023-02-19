using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// obstacle that removes stickmans from our guns
/// </summary>

namespace GAME
{
    public class Obstacle : OnTriggerGun
    {
        #region Properties
        [SerializeField] int stickmanLose = 1; 
        #endregion

        #region Override
        public override void OnTrigger()
        {
            EventManager.ObstacleJumpEvent();
            EventManager.StickmanUpdateEvent(-stickmanLose, null, VisualMode.Remove);
        } 
        #endregion
    }
}
