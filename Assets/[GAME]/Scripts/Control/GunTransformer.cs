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
    [SerializeField] Transform pointAddRemove; 

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
    private void StickmanUpdate(int added, Transform stickman, VisualMode mode)
    {
        if (stickmanCount == 1 && added < 0) return;

        #region Standard Collecting
        // standard, collecting stickmans on platform, checking condition for gun switch
        // special condition and animation for new gun
        if (added > 0 && mode == VisualMode.StandardCollect)
        {
            Transform gunPart = GetGunPart(stickmanCount + added);

            Gun nextGun = GetGun(stickmanCount + added);

            if (currentGun.GetComponent<Stickman>() || currentGun == nextGun)
            {
                ShowStickmanVisual(stickman, gunPart, mode);
                StartCoroutine(NextTransformCo(added));
            }
            else
            {
                PoolManager.instance.poolStickmanVisual.AddObjToPool(stickman.gameObject);
                NextTransform(added);
                StartCoroutine(AnimateNewGun(currentGun));
            }
        }
        #endregion

        #region Gate Collecting
        // collecting stickmans with math gates, stickmans shows up on midair
        // iteration for more than 1, condition to check if switching to a new gun
        else if (added > 0 && mode == VisualMode.GateCollect)
        {
            Gun nextGun = GetGun(stickmanCount + added);

            if(nextGun != currentGun)
            {
                NextTransform(added);
                StartCoroutine(AnimateNewGun(currentGun));
            }
            else
            {
                StartCoroutine(AddStickman(added, .2f, .1f));
            }
        }
        #endregion

        #region Removing
        // removing stickmans, just having a "added" value negative means removing
        // iteration
        else if (added < 0)
        {

        } 
        #endregion
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

        currentGun = GetGun(stickmanCount);

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
    private Gun GetGun(int count)
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
    private Transform GetGunPart(int count)
    {
        Gun gun = GetGun(count);

        // testing
        // test gun and gunParts count here why it returns null
        // ...

        if (gun && count <= gun.gunParts.Count)
        {
            return gun.gunParts[count - 1].transform;
        }
        else return null;
    }

    #endregion

    #region Gun Transform Async Methods

    /// <summary>
    /// animation for stickmans flying when adding or removing gun parts
    /// </summary>
    /// <param name="currentStickman"></param>
    /// <param name="gunPart"></param>
    /// <returns></returns>
    private void ShowStickmanVisual(Transform currentStickman, Transform gunPart, VisualMode mode)
    {
        GunPart targetGunPart = gunPart.GetComponent<GunPart>();
        currentStickman.SetParent(gunPart);
        currentStickman.DOLocalRotate(Vector3.zero, animTime_StickmanJump);

        if(mode == VisualMode.StandardCollect)
        {
            Vector3 pos = currentStickman.position;
            Vector3 posAdded = new Vector3(0f, jumpHeight, jumpOffsetZ);
            currentStickman.DOMove(pos + posAdded, animTime_StickmanJump / 2f).SetEase(easeUp)                                                                                                                                                                   
                .OnComplete(() => {

                    currentStickman.DOLocalMove(Vector3.zero, animTime_StickmanJump / 2f).SetEase(easeDown)
                    .OnComplete(() => {

                        currentStickman.SetParent(null);
                        PoolManager.instance.poolStickmanVisual.AddObjToPool(currentStickman.gameObject);

                    });
                });
        }
        else if(mode == VisualMode.GateCollect)
        {
            currentStickman.DOLocalMove(Vector3.zero, animTime_StickmanJump)
                .OnComplete(() => {
                    currentStickman.SetParent(null);
                    PoolManager.instance.poolStickmanVisual.AddObjToPool(currentStickman.gameObject);
                });
        }
        else
        {
            // ...
        }

        AnimateLimbs(currentStickman, targetGunPart, 1f);

        ColorHandle colorHandle = currentStickman.GetComponent<ColorHandle>();
        if (colorHandle) colorHandle.SetColor(targetGunPart.color);
    }

    /// <summary>
    /// animating current stickman which moves in air, to the next gun part
    /// stickman limb rotations
    /// 
    /// time fraction is based on animTime_StickmanJump
    /// 
    /// </summary>
    /// <param name="current"></param>
    /// <param name="target"></param>
    /// <param name="timeFraction"></param>
    private void AnimateLimbs(Transform current, GunPart gunPart, float timeFraction)
    {
        float duration = Mathf.Clamp01(timeFraction) * animTime_StickmanJump;

        Transform[] currentLimbs = current.GetComponentsInChildren<Transform>();

        for (int i = 0; i < currentLimbs.Length; i++)
        {
            currentLimbs[i].DORotate(gunPart.stickmanLimbs[i].eulerAngles, duration);
        }
    }

    /// <summary>
    /// switching to a new gun with animation, just a regular shake
    /// to individual gun parts
    /// </summary>
    /// <param name="gun"></param>
    /// <returns></returns>
    IEnumerator AnimateNewGun(Gun gun)
    {
        if(gun)
        {
            foreach(GunPart g in gun.gunParts)
            {
                g.transform.DOShakePosition(.3f, 1.5f);
                yield return 0;
            }
        }
    }

    /// <summary>
    /// calling NextTransform with a initial delay
    /// </summary>
    /// <param name="added"></param>
    /// <returns></returns>
    IEnumerator NextTransformCo(int added)
    {
        yield return new WaitForSeconds(animTime_StickmanJump);
        NextTransform(added);
    }

    /// <summary>
    /// adding stickmans independent from a collectable stickman
    /// just add as many as you like (if we got enough guns to support it :) )
    /// </summary>
    /// <param name="howMany"></param>
    /// <param name="delayBetween"></param>
    /// <returns></returns>
    IEnumerator AddStickman(int howMany, float addDuration, float initDelay)
    {
        yield return new WaitForSeconds(initDelay);

        int startCount = stickmanCount + 1;
        float period = addDuration / howMany;

        for (int i = 0; i < howMany; i++)
        {
            Transform gunPart = GetGunPart(startCount + i);
            Transform stickman = PoolManager.instance.GenerateVisualStickman();
            stickman.position = pointAddRemove.position;

            ShowStickmanVisual(stickman, gunPart, VisualMode.GateCollect);

            StartCoroutine(NextTransformCo(1));

            yield return new WaitForSeconds(period);
        }
    }

    /// <summary>
    /// removing stickmans from our gun
    /// </summary>
    /// <param name="howMany"></param>
    /// <param name="delayBetween"></param>
    /// <returns></returns>
    IEnumerator RemoveStickman(int howMany, float delayBetween)
    {
        yield return 0;
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

public enum VisualMode
{
    StandardCollect,
    GateCollect,
    Remove,
}