using UnityEngine;
using DG.Tweening;

/// <summary>
/// implementing ICollectableMovable interface
/// 
/// movement of a collectable (money or stickman differs when stone blasted)
/// 
/// either they jump down or fly towards player (like in level end)
/// 
/// implementing Move() method so we can pick any behavior we want after stone is blasted
/// 
/// </summary>

namespace GAME
{
    public class Collectable_JumpDown : MonoBehaviour, ICollectableMovable
    {
        #region Properties
        [SerializeField] float groundLevelY;
        [SerializeField] float nextRotationX;
        [SerializeField] float zOffset;
        [SerializeField] float jumpDuration;
        [SerializeField] float jumpPower;
        [SerializeField] int jumpCount;
        [SerializeField] Ease ease;
        #endregion

        #region Methods
        /// <summary>
        /// collectable parent null, jumps down to groundLevelY
        /// </summary>
        public void Move()
        {
            transform.SetParent(null);

            Vector3 posNext = transform.position;
            posNext.y = groundLevelY;
            posNext.z += zOffset;
            transform.DOJump(posNext, jumpPower, jumpCount, jumpDuration).SetEase(ease);

            Vector3 rotEuler = transform.eulerAngles;
            rotEuler.x = nextRotationX;
            transform.DORotate(rotEuler, jumpDuration / 2f);
        } 
        #endregion
    }

}