using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraControl : MonoBehaviour
{
    CinemachineVirtualCamera cam;

    [Header("Level End")]
    [SerializeField] Transform pointLevelEnd;
    [SerializeField] float setDuration;
    [SerializeField] Ease ease;

    #region Enable, Disable
    private void OnEnable()
    {
        EventManager.LevelEndStart += LevelEndView;
    }

    private void OnDisable()
    {
        EventManager.LevelEndStart -= LevelEndView;
    } 
    #endregion

    private void LevelEndView()
    {
        transform.DOMove(pointLevelEnd.position, setDuration).SetEase(ease);
        transform.DORotate(pointLevelEnd.eulerAngles, setDuration).SetEase(ease);

        print("camera control new view");
    }
}
