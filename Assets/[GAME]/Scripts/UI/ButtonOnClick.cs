using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  on click handle
/// </summary>

public class ButtonOnClick : ButtonMethods
{
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        foreach(ButtonActionType action in actions)
        {
            ButtonAction(action);
        }
    }
}
