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
    int stickmanCount_Instant;
    int maxStickmanCount;
    ColliderHandle colliderHandle;

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

        Gun currentGun_Instant = GetGun(stickmanCount_Instant);
        stickmanCount_Instant += added;
        Gun nextGun_Instant = GetGun(stickmanCount_Instant);

        #region Standard Collecting
        // standard, collecting stickmans on platform, checking condition for gun switch
        // special condition and animation for new gun
        if (added > 0 && mode == VisualMode.StickmanCollect)
        {
            Transform gunPart = GetGunPart(stickmanCount_Instant);
            
            if(currentGun_Instant != nextGun_Instant && !currentGun_Instant.GetComponent<Stickman>())
            {
                PoolManager.instance.poolStickmanVisual.AddObjToPool(stickman.gameObject);
                NextTransform(added);
                StartCoroutine(AnimateNewGun(currentGun));
            }
            else
            {
                ShowStickmanVisual(stickman, gunPart, mode);
                StartCoroutine(NextTransformCo(added));
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
                StartCoroutine(AddStickmanCo(added, .2f, .1f));
            }

        }
        #endregion

        #region Removing
        // removing stickmans, just having a "added" value negative means removing
        // iteration
        else if (added < 0)
        {
            Gun nextGun = GetGun(stickmanCount + added);

            if (nextGun != currentGun)
            {
                NextTransform(added);
                if(!nextGun.GetComponent<Stickman>()) StartCoroutine(AnimateNewGun(currentGun));
            }
            else
            {
                StartCoroutine(RemoveStickmanCo(added, .5f, .1f));
            }

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
        stickmanCount = Mathf.Clamp(stickmanCount + added, 1, maxStickmanCount);

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

        if (!currentGun.GetComponent<Stickman>())
            EventManager.EnableGunEvent(currentGun.gunData.id);

        colliderHandle.SetEnabledGun(currentGun);

        EventManager.SlideUIEvent(stickmanCount);
    }

    /// <summary>
    ///  gets current gun based on the stickman count value passed in
    ///  this count is a local value, so it's independent from main stickmanCount
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    private Gun GetGun(int count)
    {
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

        ColorHandle colorHandle = currentStickman.GetComponent<ColorHandle>();
        if (colorHandle) colorHandle.SetColor(targetGunPart.color);

        currentStickman.SetParent(gunPart);

        #region Standard Collect
        if (mode == VisualMode.StickmanCollect)
        {
            Vector3 pos = currentStickman.position;
            Vector3 posAdded = new Vector3(0f, jumpHeight, jumpOffsetZ);

            currentStickman.DOMove(pos + posAdded, animTime_StickmanJump / 2f).SetEase(easeUp)
                .OnComplete(() =>
                {
                    currentStickman.DOLocalMove(Vector3.zero, animTime_StickmanJump / 2f).SetEase(easeDown)
                    .OnComplete(() =>
                    {
                        PoolManager.instance.poolStickmanVisual.AddObjToPool(currentStickman.gameObject);
                    });
                });

            currentStickman.DOLocalRotate(Vector3.zero, animTime_StickmanJump);

            AnimateLimbs(currentStickman, targetGunPart, 1f);
        }
        #endregion

        #region Gate Collect
        else if (mode == VisualMode.GateCollect)
        {
            currentStickman.DOLocalMove(Vector3.zero, animTime_StickmanJump)
                .OnComplete(() =>
                {
                    PoolManager.instance.poolStickmanVisual.AddObjToPool(currentStickman.gameObject);
                });

            currentStickman.DOLocalRotate(Vector3.zero, animTime_StickmanJump);

            AnimateLimbs(currentStickman, targetGunPart, 1f);
        }
        #endregion

        #region Removing
        else
        {
            if (colorHandle) colorHandle.FadeOut();

            currentStickman.SetParent(transform);
            currentStickman.DOLocalMove(pointAddRemove.localPosition, animTime_StickmanJump)
                .OnComplete(() =>
                {

                    PoolManager.instance.poolStickmanVisual.AddObjToPool(currentStickman.gameObject);
                });
        } 
        #endregion

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
    /// sets all of them right away, not animating here
    /// </summary>
    private void SetLimbsRotPos(Transform current, GunPart gunPart)
    {
        Transform[] currentArr = current.GetComponentsInChildren<Transform>();

        for (int i = 0; i < gunPart.stickmanLimbs.Length; i++)
        {
            currentArr[i].eulerAngles = gunPart.stickmanLimbs[i].eulerAngles;
            currentArr[i].position = gunPart.stickmanLimbs[i].position;
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
    IEnumerator AddStickmanCo(int howMany, float duration, float initDelay)
    {
        yield return new WaitForSeconds(initDelay);

        int startCount = stickmanCount + 1;
        float period = duration / howMany;

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
    IEnumerator RemoveStickmanCo(int howMany, float duration, float initDelay)
    {
        yield return new WaitForSeconds(initDelay);

        int howManyPositive = Mathf.Abs(howMany);

        float period = duration / howManyPositive;

        for (int i = 0; i < howManyPositive; i++)
        {
            if (stickmanCount == 1) break;

            Transform gunPart = GetGunPart(stickmanCount);
            Transform stickman = PoolManager.instance.GenerateVisualStickman();

            SetLimbsRotPos(stickman, gunPart.GetComponent<GunPart>());

            NextTransform(-1);

            ShowStickmanVisual(stickman, gunPart, VisualMode.Remove);

            yield return new WaitForSeconds(period);
        }
    }

    #endregion

    #region Init Method
    /// <summary>
    /// init gun list 
    /// stickman count start with 1 since we have a stickman at start
    /// </summary>
    private void Init()
    {
        gunList = new List<Gun>();
        stickmanCount = 1;
        stickmanCount_Instant = 1;
        gunList.AddRange(GetComponentsInChildren<Gun>(true));
        currentGun = (gunList.Count > 0) ? gunList[0] : null;
        colliderHandle = GetComponent<ColliderHandle>();

        foreach(Gun g in gunList)
        {
            if (g.gunData.max > maxStickmanCount) maxStickmanCount = g.gunData.max;
        }
    }

    #endregion
}

public enum VisualMode
{
    StickmanCollect,
    GateCollect,
    Remove,
    None,
}