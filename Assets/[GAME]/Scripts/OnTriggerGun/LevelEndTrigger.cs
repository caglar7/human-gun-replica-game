using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// trigger level end start,
/// and that event gonna call camera, confetti methods
/// </summary>

namespace GAME
{
    public class LevelEndTrigger : OnTriggerGun
    {
        #region Override
        public override void OnTrigger()
        {
            EventManager.LevelFinishStageEvent();
        } 
        #endregion
    }
}
