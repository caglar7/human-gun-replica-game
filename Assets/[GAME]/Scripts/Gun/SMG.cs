using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SMG gun, more info in Game/Data/Guns
/// </summary>

namespace GAME
{
    public class SMG : Gun
    {
        #region Awake
        private void Awake()
        {
            Init();
        }
        #endregion
    }
}
