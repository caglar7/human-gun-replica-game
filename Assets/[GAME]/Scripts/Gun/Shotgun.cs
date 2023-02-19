using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// shotgun class, more info in scriptable object 
/// check Game/Data/guns folder
/// </summary>

namespace GAME
{
    public class Shotgun : Gun
    {
        #region Awake
        private void Awake()
        {
            Init();
        }
        #endregion
    }
}
