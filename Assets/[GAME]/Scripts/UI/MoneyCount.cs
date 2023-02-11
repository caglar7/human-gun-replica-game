using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

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

    #region Awake
    private void Awake()
    {
        Init();
    }
    #endregion

    #region Init Method
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
        clone.transform.SetParent(transform.parent);

        RectTransform rtClone = clone.GetComponent<RectTransform>();
      
        Vector2 startPosUI = Camera.main.WorldToScreenPoint(worldPos);
        float posX = UnityEngine.Random.Range(startPosUI.x - startPosRange, startPosUI.x + startPosRange);
        float posY = UnityEngine.Random.Range(startPosUI.y - startPosRange, startPosUI.y + startPosRange);
        startPosUI = new Vector2(posX, posY);

        rtClone.anchoredPosition = startPosUI;

        rtClone.DOAnchorPos(targetUIPos, duration).SetEase(ease)
            .OnComplete(() => {

                PoolManager.instance.poolMoneyImage.AddObjToPool(clone);
                UpdateCount(1);
                UpdateAnimation();
            });
    } 
    
    private void UpdateCount(int add)
    {
        moneyCount += add;
        text.text = moneyCount.ToString();
    }

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
