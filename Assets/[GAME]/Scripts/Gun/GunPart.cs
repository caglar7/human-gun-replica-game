using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GunPart : MonoBehaviour
{

    // all visuals are shown with stickmanVisual object, it' in a pool
    public void StickmanMove_Collected(Transform stickman, float duration)
    {
        stickman.SetParent(transform);
        stickman.DORotate(Vector3.zero, duration);
        stickman.DOLocalJump(Vector3.zero, 4f, 1, duration)
            .OnComplete(() => {
                stickman.SetParent(null);
                PoolManager.instance.poolStickmanVisual.AddObjToPool(stickman.gameObject);
            });
    }

    public void StickmanMove_GateCollected(int count, float duration, float delayBetween)
    {
                
    }

    public void StickmanMove_Removed(int count, float duration, float delayBetween)
    {

    }

}
