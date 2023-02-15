using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// drag control for player
/// </summary>

public class PlayerMover : MonoBehaviour
{
    #region Properties
    [Header("Settings")]
    [SerializeField] float forwardSpeed = 5f;
    [SerializeField] float clampRange = 3f;
    [Range(0f, 1f)] [SerializeField] float movementSensitivity = .5f;
    [SerializeField] float damping = 10f;

    [Header("Obstacle Jump Adjust")]
    [SerializeField] float height;
    [SerializeField] float jumpDuration;

    [Tooltip("Player hits the stones without destroying them")]
    [Header("Shooting Target Hit Adjust")]
    [SerializeField] float pushZ;
    [SerializeField] float pushDuration;
    [SerializeField] Ease pushEase;

    float screenFractionForMaxRange;
    Vector3 initTouchPosition;
    float playerPosX;
    float screenXPerUnitMove;
    bool forwardMoveActive;
    #endregion

    #region Start, Update
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        DragMove();
        ForwardMove();
    }
    #endregion

    #region Enable, Disable

    private void OnEnable()
    {
        EventManager.ObstacleJump += PlayerJump;
        EventManager.PlayerHitsTarget += PushBack;
    }

    private void OnDisable()
    {
        EventManager.ObstacleJump -= PlayerJump;
        EventManager.PlayerHitsTarget -= PushBack;
    }

    #endregion

    #region Methods
    /// <summary>
    /// sets x position
    /// </summary>
    private void DragMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            initTouchPosition = Input.mousePosition;
            playerPosX = transform.position.x;
        }
        else if (Input.GetMouseButton(0))
        {
            float diffX = (Input.mousePosition.x - initTouchPosition.x) / screenXPerUnitMove;
            Vector3 nextPos = transform.position;
            nextPos.x = Mathf.Clamp(playerPosX + diffX, -clampRange, clampRange);

            transform.position = Vector3.Lerp(transform.position, nextPos, Time.deltaTime * damping);
        }
    }

    /// <summary>
    ///  move in z dir
    /// </summary>
    private void ForwardMove()
    {
        if (!forwardMoveActive) return;
        
        transform.position += (Vector3.forward * forwardSpeed * Time.deltaTime);
    } 

    private void ForwardActive(bool value)
    {
        forwardMoveActive = value;
    }

    /// <summary>
    ///  value not used in this subs method, it's used in GunTransformer method
    /// </summary>
    /// <param name="value"></param>
    private void PlayerJump()
    {
        float initY = transform.position.y;
        transform.DOMoveY(initY + height, jumpDuration / 2f)
            .OnComplete(() => {
                transform.DOMoveY(initY, jumpDuration / 2f);
            });
    }

    private void PushBack()
    {
        ForwardActive(false);
        transform.DOMoveZ(transform.position.z - pushZ, pushDuration).SetEase(pushEase)
            .OnComplete(() => {
                ForwardActive(true);
            });
    }

    #endregion

    #region Init
    private void Init()
    {
        // screen sensitivity and pixels for max range
        screenFractionForMaxRange = Mathf.Clamp(1f - movementSensitivity, .1f, 1f);
        screenXPerUnitMove = (screenFractionForMaxRange * Screen.width) / (clampRange * 2f);
        forwardMoveActive = true;
    }

    #endregion
}
