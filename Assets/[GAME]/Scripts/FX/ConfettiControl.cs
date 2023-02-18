using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// confetti fx on level end trigger
/// </summary>

public class ConfettiControl : MonoBehaviour
{
    [SerializeField] List<ParticleSystem> confettiList = new List<ParticleSystem>();

    #region Enable, Disable

    /// <summary>
    ///  subs to level end event
    /// </summary>
    private void OnEnable()
    {
        EventManager.LevelEndStart += ShowConfetti;
    }

    private void OnDisable()
    {
        EventManager.LevelEndStart -= ShowConfetti;
    } 
    #endregion

    private void ShowConfetti()
    {
        StartCoroutine(ShowConfettiCo());
    }

    IEnumerator ShowConfettiCo()
    {
        List<int> list = new List<int>();
        for (int i = 0; i < confettiList.Count; i++)
        {
            list.Add(i);
        }

        while(list.Count > 0)
        {
            int index = Random.Range(0, confettiList.Count);
            
            if(list.Contains(index))
            {
                list.Remove(index); 
                confettiList[index].Play();

                yield return new WaitForSeconds(.1f);
            }
        }
    }
}
