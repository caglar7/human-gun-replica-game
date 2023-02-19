using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  on click handle
/// </summary>

namespace GAME
{
    public class ButtonOnClick : ButtonMethods
    {
        #region Properties, Start
        Button button;

        private void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }
        #endregion

        #region Methods
        private void OnClick()
        {
            foreach (ButtonActionType action in actions)
            {
                ButtonAction(action);
            }
        } 
        #endregion
    }
}
