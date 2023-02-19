using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// SceneLoader, singleton, loads next level
/// has levelIndex field
/// </summary>

namespace GAME
{
    public class SceneLoader : MonoBehaviour
    {
        #region Properties
        int sceneCount;
        [HideInInspector] public int currentLevelIndex;
        #endregion

        #region Awake, Init

        public static SceneLoader instance;
        private void Awake()
        {
            if (instance == null) instance = this;
            else if (instance != this) Destroy(gameObject);

            Init();
        }
        
        /// <summary>
        /// init method
        /// 
        /// get total scene count in build settings
        /// 
        /// get current level int
        /// 
        /// </summary>
        private void Init()
        {
            sceneCount = SceneManager.sceneCountInBuildSettings;

            currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// get build index 
        /// 
        /// load next index, watch for max index and go back to zero
        /// 
        /// </summary>
        public void NextLevel()
        {
            if (currentLevelIndex == sceneCount - 1) SceneManager.LoadScene(0);
            else SceneManager.LoadScene(currentLevelIndex + 1);
        }

        #endregion
    }
}
