using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class MoneyCount : MonoBehaviour
{
    #region Properties

    [SerializeField] Text text; 
    [SerializeField] RectTransform targetUI;
    [SerializeField] float duration;
    [SerializeField] Ease ease;
    int moneyCount;

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
    /// get money mesh from pool, tween it to the UI element with selected ease
    /// 
    /// </summary>
    private void Collect(Vector3 worldPos)
    {
        GameObject clone = PoolManager.instance.poolMoneyImage.PullObjFromPool();
        clone.transform.SetParent(transform.parent);

        RectTransform rtClone = clone.GetComponent<RectTransform>();
       
        Vector2 startPosUI = Camera.main.WorldToScreenPoint(worldPos);
        startPosUI.x -= (Screen.width);
        startPosUI.y -= (Screen.height);
        rtClone.anchoredPosition = startPosUI;

        rtClone.DOAnchorPos(targetUI.anchoredPosition, duration).SetEase(ease)
            .OnComplete(() => {

                UpdateCount(1);
                PoolManager.instance.poolMoneyImage.AddObjToPool(clone);
            });
    } 

    private void UpdateCount(int add)
    {
        moneyCount += add;
        text.text = moneyCount.ToString();
    }

    #endregion
}
