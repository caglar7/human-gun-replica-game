using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// anything a button can do here
/// 
/// given the action type, can do
/// switching canvases
/// loading scenes
/// quiting game
/// 
/// </summary>

public enum ButtonActionType
{
    Pause,
    Resume,
    SwitchCanvas_StartMenu,
    SwitchCanvas_GameMenu,
    SwitchCanvas_LevelEndMenu,
}

[RequireComponent(typeof(Button))]
public class ButtonMethod : MonoBehaviour
{
    [SerializeField] ButtonActionType[] actions;
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

    private void ButtonAction(ButtonActionType type)
    {
        switch(type)
        {
            case ButtonActionType.Pause:
                GameManager.instance.PauseGame();
                break;

            case ButtonActionType.Resume:
                GameManager.instance.ResumeGame();
                break;

            case ButtonActionType.SwitchCanvas_StartMenu:
                CanvasController.instance.SwitchCanvas(CanvasType.StartMenu);
                break;

            case ButtonActionType.SwitchCanvas_GameMenu:
                CanvasController.instance.SwitchCanvas(CanvasType.GameMenu);
                break;

            case ButtonActionType.SwitchCanvas_LevelEndMenu:
                CanvasController.instance.SwitchCanvas(CanvasType.LevelEndMenu);
                break;
        }
    }
}
