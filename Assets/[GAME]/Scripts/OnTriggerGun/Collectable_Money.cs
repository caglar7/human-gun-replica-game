using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// collectable
/// </summary>

namespace GAME
{
    public class Collectable_Money : OnTriggerGun
    {
        #region Override
        /// <summary>
        ///  removing this object and get a small money mesh from pool
        ///  moves up to UI, updating the money count on UI
        /// </summary>
        public override void OnTrigger()
        {
            EventManager.MoneyCollectEvent(transform.position);

            transform.SetParent(null);
            gameObject.SetActive(false);
        } 

        #endregion
    }
}
