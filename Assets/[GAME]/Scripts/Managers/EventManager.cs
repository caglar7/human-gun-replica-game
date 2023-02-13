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
    public static event Action<int, Transform, bool> StickmanUpdate;
    public static event Action ObstacleJump;
    public static event Action<Vector3> MoneyCollect;
    #endregion

    #region Methods
    public static void StartGameEvent()
    {
        StartGame?.Invoke();
    } 

    public static void StickmanUpdateEvent(int count, Transform t = null, bool isJump = false)
    {
        StickmanUpdate?.Invoke(count, t, isJump);
    }

    public static void ObstacleJumpEvent()
    {
        ObstacleJump?.Invoke();
    }

    public static void MoneyCollectEvent(Vector3 worldPos)
    {
        MoneyCollect?.Invoke(worldPos);
    }

    #endregion
}
