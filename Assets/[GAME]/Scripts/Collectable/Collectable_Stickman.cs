using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable_Stickman : Collectable
{
    [SerializeField] int addCount = 1;

    public override void OnCollect()
    {
        EventManager.GunTransformEvent(addCount);
        gameObject.SetActive(false);
    }
}
