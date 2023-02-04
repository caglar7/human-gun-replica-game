using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    #region Properties
    [Header("Gun Parts")]
    public Transform gunPartsObj;
    [HideInInspector] public List<GunPart> gunParts = new List<GunPart>();

    [Header("Gun Data")]
    public GunData gunData;

    #endregion

    #region Init Method
    public void Init()
    {
        gunParts.AddRange(gunPartsObj.GetComponentsInChildren<GunPart>(true));
    } 
    #endregion
}
