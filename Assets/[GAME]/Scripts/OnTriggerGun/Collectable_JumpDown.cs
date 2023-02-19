using UnityEngine;
using DG.Tweening;

namespace GAME
{
    public class Collectable_JumpDown : MonoBehaviour, ICollectableMovable
    {
        [SerializeField] float groundLevelY;
        [SerializeField] float nextRotationX;
        [SerializeField] float zOffset;
        [SerializeField] float jumpDuration;
        [SerializeField] float jumpPower;
        [SerializeField] int jumpCount;
        [SerializeField] Ease ease;

        /// <summary>
        /// object set parent null, jumps down to groundLevelY
        /// new rotation
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
    }

}