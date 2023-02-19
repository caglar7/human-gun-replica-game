using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// pistol, check its scriptable object for more info,
/// in Game/Data/Guns
/// 
/// </summary>

namespace GAME
{
    public class Pistol : Gun
    {
        #region Awake
        private void Awake()
        {
            Init();
        }

        #endregion
    }
}
