using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

/// <summary>
/// UI money count in Game Menu, 
///
/// updating money count and animating
/// 
/// </summary>

namespace GAME
{
    public class MoneyCount : MonoBehaviour
    {
        #region Properties
        [Header("Money Collect Settings")]
        [SerializeField] Text text;
        [SerializeField] RectTransform targetUI;
        [SerializeField] float duration;
        [SerializeField] Ease ease;
        [SerializeField] float startPosRange;
        [SerializeField] float animScale;
        [SerializeField] float animTime;
        int moneyCount;
        Vector3 initScale;
        Vector2 targetUIPos;

        #endregion

        #region Awake, Init
        private void Awake()
        {
            Init();
        }
        
        /// <summary>
        /// money count UI in GameMenu,
        /// 
        /// get target pos and init scale for animations
        /// 
        /// </summary>
        private void Init()
        {
            moneyCount = 0;

            Vector2 pos = targetUI.anchoredPosition;
            pos += new Vector2(Screen.width, Screen.height);
            targetUIPos = pos;

            initScale = transform.localScale;
        }
        #endregion

        #region Enable, Disable
        /// <summary>
        /// subs to events
        /// </summary>
        private void OnEnable()
        {
            EventManager.MoneyCollect += Collect;
        }

        private void OnDisable()
        {
            EventManager.MoneyCollect -= Collect;
        }

        #endregion

        #region Methods
        /// <summary>
        /// 
        /// get money mesh from pool, tween it to the UI element with selected ease
        /// 
        /// </summary>
        private void Collect(Vector3 worldPos)
        {
            GameObject clone = PoolManager.instance.poolMoneyImage.PullObjFromPool();

            Transform canvasObj = CanvasController.instance.transform;

            clone.transform.SetParent(canvasObj);

            RectTransform rtClone = clone.GetComponent<RectTransform>();

            Vector2 startPosUI = Camera.main.WorldToScreenPoint(worldPos) / canvasObj.localScale.x;
            float posX = UnityEngine.Random.Range(startPosUI.x - startPosRange, startPosUI.x + startPosRange);
            float posY = UnityEngine.Random.Range(startPosUI.y, startPosUI.y + startPosRange);
            startPosUI = new Vector2(posX, posY);

            rtClone.anchoredPosition = startPosUI;

            rtClone.DOAnchorPos(targetUIPos, duration).SetEase(ease)
                .OnComplete(() =>
                {

                    PoolManager.instance.poolMoneyImage.AddObjToPool(clone);
                    UpdateCount(1);
                    UpdateAnimation();
                });
        }

        /// <summary>
        /// add and update text
        /// </summary>
        /// <param name="add"></param>
        private void UpdateCount(int add)
        {
            moneyCount += add;
            text.text = moneyCount.ToString();
        }

        /// <summary>
        ///  scale up and down everytime money collected on platform
        /// </summary>
        private void UpdateAnimation()
        {
            transform.DOKill();
            transform.DOScale(animScale * initScale, animTime / 2f)
                .OnComplete(() => {
                    transform.DOScale(initScale, animTime / 2f);
                });
        }

        #endregion
    }
}
