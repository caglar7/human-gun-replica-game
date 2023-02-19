using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
///  on press handle, only pointer down, not a click
/// </summary>

namespace GAME
{
    public class ButtonOnPressed : ButtonMethods, IPointerDownHandler
    {
        #region Methods

        public void OnPointerDown(PointerEventData eventData)
        {
            foreach (ButtonActionType action in actions)
            {
                ButtonAction(action);
            }
        } 
        #endregion
    }
}
