using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// when a gun shoots, there will be some rebound or recoil
/// any stickman that is effected by this recoil has this component
/// </summary>

namespace GAME
{
    public class RecoilPart : MonoBehaviour
    {
        /// <summary>
        /// adjustable recoil
        /// </summary>
        /// <param name="level"></param>
        public void RecoilAnimation(float level)
        {
            transform.DOKill(true);

            float duration = .2f;
            float initZ = transform.localPosition.z;
            transform.DOLocalMoveZ(transform.localPosition.z - level, duration / 2f)
                .OnComplete(() => {
                    transform.DOLocalMoveZ(initZ, duration / 2f);
                });
        }
    }
}
