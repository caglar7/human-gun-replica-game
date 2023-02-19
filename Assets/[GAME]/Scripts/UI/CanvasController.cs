using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// main canvas and switching methods
/// 
/// any more new subcanvas added
/// update the enum below
/// </summary>

namespace GAME
{
    public class CanvasController : MonoBehaviour
    {
        #region Properties
        SubCanvas[] subCanvases;
        #endregion

        #region Awake

        public static CanvasController instance { get; private set; }

        private void Awake()
        {
            if (instance == null) instance = this;
            else if (instance != this) Destroy(gameObject);

            Init();
        }

        #endregion

        #region Methods
        /// <summary>
        /// deactive canvases and activate the next one
        /// </summary>
        /// <param name="type"></param>
        public void SwitchCanvas(CanvasType type)
        {
            foreach (SubCanvas sub in subCanvases)
            {
                if (sub.canvasType == type) sub.gameObject.SetActive(true);
                else sub.gameObject.SetActive(false);
            }
        }
        #endregion

        #region Init
        private void Init()
        {
            subCanvases = GetComponentsInChildren<SubCanvas>(true);
            SwitchCanvas(CanvasType.StartMenu);
        }
        #endregion
    }

    public enum CanvasType
    {
        StartMenu,
        GameMenu,
        LevelEndMenu,
    }
}
