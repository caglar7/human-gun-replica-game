using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// trigger level end start,
/// and that event gonna call camera, confetti methods
/// </summary>

public class LevelEndTrigger : OnTriggerGun
{

    public override void OnTrigger()
    {
        EventManager.LevelEndStartEvent();     
    }
}
