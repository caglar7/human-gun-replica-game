using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

/// <summary>
///  method to generate a gun transform state UI, where 
///  the curent gun transformations are shown
///  
/// initial stickman should be considered as zero just for this UI
/// 
/// </summary>

namespace GAME
{
    public class GunStateUI : MonoBehaviour
    {
        #region Properties
        [SerializeField] RectTransform arrowRT;
        [SerializeField] GameObject circlePrefab;
        [SerializeField] GameObject rectanglePrefab;
        [SerializeField] float xDiff;
        [SerializeField] Transform parentObject;
        [SerializeField] GunData[] gunDatas;
        Vector2 nextPos;
        int stickmanCount;
        #endregion

        #region Awake, Init
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            nextPos = Vector2.zero;

            stickmanCount = 1;

            GenerateGunStateUI();
        } 
        #endregion

        #region Enable, Disable
        /// <summary>
        /// subs to events
        /// </summary>
        private void OnEnable()
        {
            EventManager.SlideUI += SlideState;
        }

        private void OnDisable()
        {
            EventManager.SlideUI -= SlideState;
        }
        #endregion

        #region Methods
        /// <summary>
        ///  generation UI for current gun states
        ///  using all the gunData 
        /// 
        /// </summary>
        private void GenerateGunStateUI()
        {
            foreach (GunData g in gunDatas)
            {
                for (int i = g.min; i <= g.max; i++)
                {
                    if (i == g.min) GenerateRectangle(nextPos, g.name);
                    else GenerateCircle(nextPos);
                    nextPos += new Vector2(xDiff, 0);
                }
            }
        }

        private GameObject GenerateCircle(Vector2 rPos)
        {
            GameObject clone = Instantiate(circlePrefab, parentObject);
            clone.GetComponent<RectTransform>().anchoredPosition = rPos;
            return clone;
        }

        private GameObject GenerateRectangle(Vector2 rPos, string gunName)
        {
            GameObject clone = Instantiate(rectanglePrefab, parentObject);
            clone.GetComponent<RectTransform>().anchoredPosition = rPos;

            clone.GetComponentInChildren<TextMeshProUGUI>().text = gunName;

            return clone;
        }

        private void SlideState(int currentCount)
        {
            if (stickmanCount != currentCount)
            {
                stickmanCount = currentCount;

                float nextX = -xDiff * (stickmanCount - 1);

                RectTransform rt = parentObject.GetComponent<RectTransform>();

                rt.DOKill(true);

                rt.DOAnchorPosX(nextX, .4f);
            }
        } 

        #endregion
    }
}
