using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GAME
{
    public class Collectable_MoveToPlayer : MonoBehaviour, ICollectableMovable
    {
        [Header("Settings")]
        [SerializeField] Ease ease = Ease.Linear;
        [SerializeField] float jumpPower = 3f;
        [SerializeField] float duration = 1f;
        [SerializeField] float scaleDown = .1f;

        /// <summary>
        /// animate scaling up down and move to player
        /// </summary>
        public void Move()
        {
            Transform player = FindObjectOfType<PlayerMover>().transform;

            transform.SetParent(player);

            Vector3 initScale = transform.localScale;
            transform.DOScale(initScale * 1.3f, duration / 4f)
                .OnComplete(() => {

                    transform.DOScale(initScale * scaleDown, duration * 3f / 4f);
                });

            transform.DOLocalMove(Vector3.zero, duration).SetEase(ease);
        }
    }

}