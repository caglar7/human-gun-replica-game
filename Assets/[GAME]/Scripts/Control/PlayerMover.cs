using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// player mover component
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

    [Header("Level End Throw Away Player")]
    [SerializeField] float throwJumpPower;
    [SerializeField] float throwDuration;
    [SerializeField] float jumpOffsetX;
    [SerializeField] float jumpOffsetY;
    ColliderHandle colliderHandle;

    float screenFractionForMaxRange;
    Vector3 initTouchPosition;
    float playerPosX;
    float screenXPerUnitMove;
    bool forwardMoveActive;
    bool moveActive;
    #endregion

    #region Awake, Update
    private void Awake()
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

    /// <summary>
    /// subs to events
    /// </summary>
    private void OnEnable()
    {
        EventManager.ObstacleJump += PlayerJump;
        EventManager.PlayerHitsTarget += PushBack;
        EventManager.LevelComplete += ThrowOnLevelEnd;
    }

    private void OnDisable()
    {
        EventManager.ObstacleJump -= PlayerJump;
        EventManager.PlayerHitsTarget -= PushBack;
        EventManager.LevelComplete -= ThrowOnLevelEnd;
    }

    #endregion

    #region Methods
    /// <summary>
    /// sets x position
    /// </summary>
    private void DragMove()
    {
        // testing 
        //float horizontal = Input.GetAxisRaw("Horizontal");
        //Vector3 pos = transform.position;
        //pos.x += (horizontal * Time.deltaTime * 10);
        //transform.position = pos;
        //return;

        if (!moveActive) return;

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
        if (!moveActive || !forwardMoveActive) return;
        
        transform.position += (Vector3.forward * forwardSpeed * Time.deltaTime);
    } 

    /// <summary>
    /// activate or deactivate forward movement
    /// currently used for a pushback method (when player bumps into a stone)
    /// </summary>
    /// <param name="value"></param>
    private void ForwardMovementActive(bool value)
    {
        forwardMoveActive = value;
    }

    /// <summary>
    ///  all inputs from player
    /// </summary>
    /// <param name="value"></param>
    private void MovementActive(bool value)
    {
        moveActive = value;
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

    /// <summary>
    /// push back when bumped to a stone
    /// </summary>
    private void PushBack()
    {
        ForwardMovementActive(false);
        transform.DOMoveZ(transform.position.z - pushZ, pushDuration).SetEase(pushEase)
            .OnComplete(() => {
                ForwardMovementActive(true);
            });
    }

    /// <summary>
    /// throw away player for a level complete feeling
    /// </summary>
    private void ThrowOnLevelEnd()
    {
        MovementActive(false);

        colliderHandle.EnableColliders(false);

        Vector3 nextRot = transform.eulerAngles + new Vector3(-360f, 0f, 0f);
        transform.DORotate(nextRot, throwDuration, RotateMode.FastBeyond360);

        Vector3 pos = transform.position;
        Vector3 jumpPos = 
            new Vector3(transform.position.x <= 0 ? -jumpOffsetX : jumpOffsetX, pos.y - jumpOffsetY, pos.z);

        transform.DOJump(jumpPos, throwJumpPower, 1, throwDuration)
            .OnComplete(() => gameObject.SetActive(false));
    }

    #endregion

    #region Init
    private void Init()
    {
        // screen sensitivity and pixels for max range
        screenFractionForMaxRange = Mathf.Clamp(1f - movementSensitivity, .1f, 1f);
        screenXPerUnitMove = (screenFractionForMaxRange * Screen.width) / (clampRange * 2f);
        forwardMoveActive = true;
        moveActive = true;
        colliderHandle = GetComponent<ColliderHandle>();
    }

    #endregion
}
