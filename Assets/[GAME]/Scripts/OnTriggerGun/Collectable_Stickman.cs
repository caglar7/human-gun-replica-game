using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable_Stickman : OnTriggerGun
{
    [SerializeField] Transform stickman;

    public override void OnTrigger()
    {
        Transform cloneTransform = PoolManager.instance.GenerateVisualStickman();
        cloneTransform.position = stickman.position;
        cloneTransform.rotation = stickman.rotation;

        EventManager.StickmanUpdateEvent(1, cloneTransform, VisualMode.StickmanCollect);
        gameObject.SetActive(false);
    }
}
