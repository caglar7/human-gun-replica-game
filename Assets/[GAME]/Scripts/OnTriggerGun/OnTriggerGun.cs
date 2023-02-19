using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAME
{
    public class OnTriggerGun : MonoBehaviour
    {
        bool isUsed = false;
        Gun gun;

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
    }
}
