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
    #endregion

    #region Methods
    public static void StartGameEvent()
    {
        StartGame?.Invoke();
    } 
    #endregion
}
