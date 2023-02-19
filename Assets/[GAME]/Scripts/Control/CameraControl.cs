using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using System;

/// <summary>
/// Cinemachine camera control,
/// to set camera in prefered position or rotation
/// </summary>

public class CameraControl : MonoBehaviour
{
    #region Properties
    [Header("Level End")]
    [SerializeField] Transform pointLevelEnd;
    [SerializeField] float setDuration;
    [SerializeField] Ease ease;

    CinemachineVirtualCamera cam;
    Transform player;

    #endregion

    #region Awake, Init

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        player = cam.Follow;
    }

    #endregion

    #region Enable, Disable
    private void OnEnable()
    {
        EventManager.LevelFinishStage += LevelEndView;
        EventManager.LevelComplete += StopFollowing;
    }

    private void OnDisable()
    {
        EventManager.LevelFinishStage -= LevelEndView;
        EventManager.LevelComplete -= StopFollowing;
    }
    #endregion

    #region Methods
    /// <summary>
    /// level end view, setting camera a above position and editing rotation
    /// so we can recognize x2,x3 texts better
    /// </summary>
    private void LevelEndView()
    {
        transform.DOMove(pointLevelEnd.position, setDuration).SetEase(ease);
        transform.DORotate(pointLevelEnd.eulerAngles, setDuration).SetEase(ease);
    }

    /// <summary>
    /// camera follow activated or deactivated
    /// </summary>
    private void StopFollowing() => cam.Follow = null;

    private void KeepFollowing() => cam.Follow = player;

    #endregion
}
