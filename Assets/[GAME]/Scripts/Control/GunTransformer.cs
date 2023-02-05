using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private void NextTransform(int addedCount)
    {
        stickmanCount = Mathf.Max(stickmanCount + addedCount, 1);

        ActivateCurrentGun();

        ActivateGunParts();
    }

    /// <summary>
    /// using stickmanCount, activate gun parts (stickmans)
    /// this activation will be after an collectable jump animation end
    /// also color animation
    /// </summary>
    private void ActivateGunParts()
    {
        if(currentGun)
        {


            for (int i = 0; i < currentGun.gunParts.Count; i++)
            {
                if (i < stickmanCount) currentGun.gunParts[i].gameObject.SetActive(true);
                else currentGun.gunParts[i].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// using the stickmanCount value, activate correct gun
    /// for the count
    /// </summary>
    private void ActivateCurrentGun()
    {
        foreach (Gun g in gunList)
        {
            if (stickmanCount >= g.gunData.min && stickmanCount <= g.gunData.max)
            {
                if (currentGun != g)
                {
                    g.gameObject.SetActive(true);
                    currentGun = g;

                    // testing
                    print("current gun" + currentGun.gunData.name);
                }
                break;
            }
            else g.gameObject.SetActive(false);
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
