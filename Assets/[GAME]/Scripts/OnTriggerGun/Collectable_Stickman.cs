using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAME
{
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

            if (!ColliderHandle.isUsed)
            {
                ColliderHandle.isUsed = true;
                EventManager.CollidersDisabledEvent();
            }
        }
    }

}