using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Awake, Start
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    private void Start()
    {
        PauseGame();
    }

    #endregion

    public void PauseGame()
    {
        Time.timeScale = 0f;

        
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
