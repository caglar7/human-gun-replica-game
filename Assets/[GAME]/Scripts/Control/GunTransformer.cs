using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// transforming from one gun to another based on stickman count
/// </summary>

public class GunTransformer : MonoBehaviour
{
    #region Properties
    List<Gun> gunList;
    Gun currentGun;
    int stickmanCount;

    [Header("Animation Settings")]
    [SerializeField] float animTime_StickmanJump;
    [SerializeField] float jumpHeight;
    [SerializeField] float jumpOffsetZ;
    [SerializeField] Ease easeUp;
    [SerializeField] Ease easeDown;

    #endregion

    #region Awake
    private void Awake()
    {
        Init();
    }
    #endregion

    #region Enable, Disable
    /// <summary>
    /// subs to events
    /// </summary>
    private void OnEnable()
    {
        EventManager.StickmanUpdate += StickmanUpdate;
    }

    private void OnDisable()
    {
        EventManager.StickmanUpdate -= StickmanUpdate;
    }

    #endregion

    #region Gun Transform Methods

    /// <summary>
    /// handling 3 cases for stickman update here
    /// 
    /// * collecting stickmans
    /// 
    /// * collecting with gates
    /// 
    /// * removing stickmans (happens at gates, obstacles)
    /// 
    /// </summary>
    /// <param name="added"></param>
    /// <param name="stickman"></param>
    private void StickmanUpdate(int added, Transform stickman, bool isJump)
    {
        if (stickmanCount == 1 && added < 0) return;

        // only collecting stickman for now 
        if (added > 0 && stickman != null)       // collecting stickman case
        {
            // here use the next gun part

            // testing
            Transform nextPart = GetNextGunPart(stickmanCount + added);

            ShowStickmanVisual(stickman, nextPart, true);

        }
        else if (added > 0 && stickman == null)  // collecting with gate
        {

        }
        else if (added < 0 && stickman == null)
        {

        }
    }

    /// <summary>
    /// method to activate proper gun and gun parts 
    /// based on the count param given
    /// </summary>
    /// <param name="count"></param>
    private void NextTransform(int added)
    {
        stickmanCount = Mathf.Max(stickmanCount + added, 1);

        foreach (GunPart gPart in currentGun.gunParts)
            gPart.EnableRenderer(false);

        currentGun = GetCurrentGun(stickmanCount);

        if (currentGun)
        {
            for (int i = 0; i < currentGun.gunParts.Count; i++)
            {
                if (i < stickmanCount) currentGun.gunParts[i].EnableRenderer(true);
                else currentGun.gunParts[i].EnableRenderer(false);
            }
        }
    }

    /// <summary>
    ///  gets current gun based on the stickman count value passed in
    ///  this count is a local value, so it's independent from main stickmanCount
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    private Gun GetCurrentGun(int count)
    {
        #region prev logic
        //foreach (Gun g in gunList)
        //{
        //    if (stickmanCount >= g.gunData.min && stickmanCount <= g.gunData.max)
        //    {
        //        if (currentGun != g)
        //        {
        //            //g.gameObject.SetActive(true);
        //            currentGun = g;
        //        }
        //    }
        //    else g.gameObject.SetActive(false);
        //} 
        #endregion

        Gun gun = null;
        foreach(Gun g in gunList)
        {
            if (count >= g.gunData.min && count <= g.gunData.max)
            {
                gun = g;
                break;
            }
        }

        return gun;
    }

    /// <summary>
    /// next count as for stickman count after we add
    /// 
    /// before we might get the data where the stickman gonna be located
    /// 
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    private Transform GetNextGunPart(int nextCount)
    {
        Gun gun = GetCurrentGun(nextCount);

        // testing
        // test gun and gunParts count here why it returns null
        // ...

        if (gun && nextCount <= gun.gunParts.Count)
        {
            return gun.gunParts[nextCount - 1].transform;
        }
        else return null;
    }

    #endregion

    #region Gun Transform Async Methods

    /// <summary>
    /// moving stickmanVisual to the target transform
    /// </summary>
    /// <param name="currentStickman"></param>
    /// <param name="targetStickman"></param>
    /// <returns></returns>
    private void ShowStickmanVisual(Transform currentStickman, Transform targetStickman, bool isJump)
    {
        // make sure the stickman obj (params) are on the same sorf of hierarchy

        currentStickman.SetParent(targetStickman);
        currentStickman.DOLocalRotate(Vector3.zero, animTime_StickmanJump);

        if(isJump)
        {
            //currentStickman.DOJump(Vector3.zero, 5f, 1, animTime_StickmanJump)
            //    .OnComplete(() => {
            //        currentStickman.SetParent(null);
            //        PoolManager.instance.poolStickmanVisual.AddObjToPool(currentStickman.gameObject);
            //    });

            // do move up and move to gun part
            Vector3 pos = currentStickman.position;
            Vector3 posAdded = new Vector3(0f, jumpHeight, jumpOffsetZ);
            currentStickman.DOMove(pos + posAdded, animTime_StickmanJump / 2f).SetEase(easeUp)                                                                                                                                                                   
                .OnComplete(() => {

                    currentStickman.DOLocalMove(Vector3.zero, animTime_StickmanJump / 2f).SetEase(easeDown)
                    .OnComplete(() => {

                        currentStickman.SetParent(null);
                        PoolManager.instance.poolStickmanVisual.AddObjToPool(currentStickman.gameObject);

                        // update gun here
                        // ...

                    });
                });
        }
        else
        {
            currentStickman.DOLocalMove(Vector3.zero, animTime_StickmanJump)
                .OnComplete(() => {
                    currentStickman.SetParent(null);
                    PoolManager.instance.poolStickmanVisual.AddObjToPool(currentStickman.gameObject);
                });
        }
       
    }

    IEnumerator NextTransformCo(int added)
    {
        yield return new WaitForSeconds(animTime_StickmanJump);
        NextTransform(added);
    }

    #endregion

    #region Init Method
    /// <summary>
    /// init gun list 
    /// 
    /// stickman count start with 1 since we have a stick at start
    /// </summary>
    private void Init()
    {
        gunList = new List<Gun>();
        stickmanCount = 1;
        gunList.AddRange(GetComponentsInChildren<Gun>(true));
        currentGun = (gunList.Count > 0) ? gunList[0] : null;
    }
    #endregion

}
