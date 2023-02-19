using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GAME
{
    public class RecoilPart : MonoBehaviour
    {

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
