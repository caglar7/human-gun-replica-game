using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Event Manager
/// </summary>

public class EventManager : MonoBehaviour
{
    #region Events
    public static event Action StartGame;
    public static event Action<int, Transform, VisualMode> StickmanUpdate;
    public static event Action ObstacleJump;
    public static event Action<Vector3> MoneyCollect;
    public static event Action<int> EnableGun;
    public static event Action PlayerHitsTarget;
    #endregion

    #region Methods
    public static void StartGameEvent()
    {
        StartGame?.Invoke();
    } 

    public static void StickmanUpdateEvent(int count, Transform t, VisualMode mode)
    {
        StickmanUpdate?.Invoke(count, t, mode);
    }

    public static void ObstacleJumpEvent()
    {
        ObstacleJump?.Invoke();
    }

    public static void MoneyCollectEvent(Vector3 worldPos)
    {
        MoneyCollect?.Invoke(worldPos);
    }

    public static void EnableGunEvent(int id)
    {
        EnableGun?.Invoke(id);
    }

    public static void PlayerHitsTargetEvent()
    {
        PlayerHitsTarget?.Invoke();
    }

    #endregion
}
