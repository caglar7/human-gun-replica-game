using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable_Stickman : OnTriggerGun
{
    [SerializeField] int addCount = 1;

    public override void OnTrigger()
    {
        EventManager.GunTransformEvent(addCount);
        gameObject.SetActive(false);
    }
}
