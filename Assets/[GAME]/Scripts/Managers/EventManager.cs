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
    public static event Action CollidersDisabled;
    public static event Action LevelEndStart;   // when passes finish line, starts shooting
    public static event Action LevelEndDone;    // when level is complete, scene is done
    public static event Action<int> SlideUI;
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

    public static void CollidersDisabledEvent()
    {
        CollidersDisabled?.Invoke();
    }

    public static void LevelEndStartEvent()
    {
        LevelEndStart?.Invoke();
    }

    public static void LevelEndDoneEvent()
    {
        LevelEndDone?.Invoke();
    }

    public static void SlideUIEvent(int count)
    {
        SlideUI?.Invoke(count);
    }

    #endregion
}
