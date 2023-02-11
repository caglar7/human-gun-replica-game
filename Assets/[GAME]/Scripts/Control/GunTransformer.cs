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
        EventManager.GunTransform += NextTransform;
    }

    private void OnDisable()
    {
        EventManager.GunTransform -= NextTransform;
    }

    #endregion

    #region Gun Transform Methods
    /// <summary>
    /// 
    /// clamp stickman count to min => 1, so at least we have a pistol
    /// at all times
    /// 
    /// get correct gun for stickman count
    /// 
    /// activate deactivate parts for current stickmanCount
    /// 
    /// </summary>
    /// <param name="addedCount"> added stickman count </param>
    private int UpdateStickmanCount(int addedCount)
    {
        // here
        // should create a class etc. to handle stickman counts and access
        // this count
        // ...

        stickmanCount = Mathf.Max(stickmanCount + addedCount, 1);
        return stickmanCount;
    }

    /// <summary>
    /// using the stickmanCount value, get correct gun
    /// for the count
    /// </summary>
    private Gun GetCurrentGun()
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
            if (stickmanCount >= g.gunData.min && stickmanCount <= g.gunData.max)
            {
                gun = g;
                break;
            }
        }

        return gun;
    }

    /// <summary>
    /// using stickmanCount, activate gun parts (stickmans)
    /// this activation will be after an collectable jump animation end
    /// also color animation
    /// </summary>
    private void NextTransform()
    {
        if (stickmanCount == 1) return;

        Gun gun = GetCurrentGun();
        foreach (Gun g in gunList)
        {
            g.gameObject.SetActive(false);
        }
        gun.gameObject.SetActive(true);

        if (gun)
        {
            for (int i = 0; i < gun.gunParts.Count; i++)
            {
                if (i < stickmanCount) gun.gunParts[i].gameObject.SetActive(true);
                else gun.gunParts[i].gameObject.SetActive(false);
            }
             
        }
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
        currentGun = null;
        stickmanCount = 1;
        gunList.AddRange(GetComponentsInChildren<Gun>(true));

        // testing
        print("gun list count: " + gunList.Count);
    } 
    #endregion
}
