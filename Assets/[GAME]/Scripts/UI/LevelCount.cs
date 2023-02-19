using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// just set level text on awake
/// </summary>

namespace GAME
{
    public class LevelCount : MonoBehaviour
    {
        #region Get Set Level Count

        TextMeshProUGUI tm;

        private void Awake()
        {
            tm = GetComponent<TextMeshProUGUI>();
            tm.text = "Level " + (SceneLoader.instance.currentLevelIndex + 1).ToString();
        } 
        #endregion
    }
}
