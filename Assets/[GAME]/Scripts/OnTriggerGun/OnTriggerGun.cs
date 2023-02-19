using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// main class for any object in environment that 
/// is gonna triggered or collected
/// 
/// a trigger object (such as a collectable) inherits this object
/// 
/// and overrides OnTrigger method for its own implementation
///
/// 
/// </summary>

namespace GAME
{
    public class OnTriggerGun : MonoBehaviour
    {
        #region Properties
        bool isUsed = false;
        Gun gun;

        #endregion

        #region Methods
        private void OnTriggerEnter(Collider other)
        {
            gun = other.GetComponent<Gun>();
            if (gun && isUsed == false)
            {
                isUsed = true;
                OnTrigger();
            }
        }

        public virtual void OnTrigger()
        {
            Debug.Log("OnTrigger() not implemented");
        } 

        #endregion
    }
}
