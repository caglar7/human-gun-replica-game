using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// collectable
/// </summary>

namespace GAME
{
    public class Collectable_Stickman : OnTriggerGun
    {
        #region Properties
        [SerializeField] Transform stickman; 
        #endregion

        #region Methods
        /// <summary>
        /// get a visual clone from pool to animate stickman jumping to the player gun
        /// update stickman counts after jump is done and stickman is placed
        /// </summary>
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

        #endregion

    }

}