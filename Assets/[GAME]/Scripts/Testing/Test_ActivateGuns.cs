using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
///  just to enable disable renderers so we can work in edit mode
///  on other things while viewing guns
/// </summary>

[ExecuteInEditMode]
public class Test_ActivateGuns : MonoBehaviour
{

    [SerializeField]
    private GunNames gunName;

    private void OnEnable()
    {
        ShowGun(gunName);
    }

    private void OnDisable()
    {
        ShowGun(GunNames.Stickman);
    }

    private void OnValidate()
    {
        ShowGun(gunName);
    }

    private void ShowGun(GunNames name)
    {
        foreach(Gun gun in transform.GetComponentsInChildren<Gun>())
        {
            if(gun.gunData.name == name.ToString())
            {
                foreach(Renderer r in gun.transform.GetComponentsInChildren<Renderer>())
                {
                    r.enabled = true;
                }
            }
            else
            {
                foreach (Renderer r in gun.transform.GetComponentsInChildren<Renderer>())
                {
                    r.enabled = false;
                }
            }
        }
    }

    enum GunNames
    {
        Stickman,
        Pistol,
        SMG,
        Shotgun,
    }
}


