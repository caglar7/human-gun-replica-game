using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// money rising on level complete based on distance travelled
/// 
/// shooting level end stones
/// 
/// </summary>

namespace GAME
{
    public class MoneyGain : MonoBehaviour
    {
        #region Properties
        [Header("Settings")]
        [SerializeField] float initDelay;
        [SerializeField] float duration;
        [SerializeField] [Range(1, 5)] int moneyRangeMin;
        [SerializeField] [Range(6, 10)] int moneyRangeMax;
        [SerializeField] GameObject icon;
        [SerializeField] TextMeshProUGUI tm;
        int count = 0; 
        #endregion

        #region Enable, Disable
        private void OnEnable()
        {
            EventManager.LevelComplete += ShowMoney;
        }

        private void OnDisable()
        {
            EventManager.LevelComplete -= ShowMoney;
        }
        #endregion

        #region Methods
        /// <summary>
        ///  rise money count and update text
        /// </summary>
        private void ShowMoney(int xReached)
        {
            StartCoroutine(ShowMoneyCo(xReached));
        }

        IEnumerator ShowMoneyCo(int xReached)
        {
            yield return new WaitForSeconds(initDelay);

            int moneyGain = Random.Range(moneyRangeMin, moneyRangeMax + 1) * xReached;

            float period = duration / moneyGain;

            for (int i = 0; i < moneyGain; i++)
            {
                count++;
                tm.text = count.ToString();

                yield return new WaitForSeconds(period);
            }
        } 
        #endregion
    }
}
