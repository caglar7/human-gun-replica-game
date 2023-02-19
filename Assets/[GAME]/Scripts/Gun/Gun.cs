using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// main gun class, and every gun object inherit this class
/// </summary>

namespace GAME
{
    public class Gun : MonoBehaviour
    {
        #region Properties
        [Header("Gun Parts")]
        public Transform gunPartsObj;
        [HideInInspector] public List<GunPart> gunParts = new List<GunPart>();

        [Header("Gun Data")]
        public GunData gunData;

        #endregion

        #region Init Method
        public void Init()
        {
            gunParts.AddRange(gunPartsObj.GetComponentsInChildren<GunPart>(true));
        }
        #endregion
    }
}
