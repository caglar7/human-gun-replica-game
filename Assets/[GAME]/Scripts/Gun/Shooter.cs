using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    #region Properties
    GunData gunData;
    #endregion

    #region Awake, Update
    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        
    }

    #endregion


    #region Init Method
    private void Init()
    {
        gunData = GetComponent<Gun>().gunData;
    }
    #endregion
}
