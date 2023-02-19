using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// stickman visual color handling, 
/// chaning color to the target gunPart
/// or fading out when removing stickmans
/// </summary>

namespace GAME
{
    public class ColorHandle : MonoBehaviour
    {
        #region Properties
        MaterialPropertyBlock matPB;
        Renderer rend; 
        #endregion

        #region Awake, Init
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            matPB = new MaterialPropertyBlock();
            rend = GetComponentInChildren<Renderer>();
        }
        #endregion

        #region Color Methods
        /// <summary>
        ///  set color with prop block
        /// </summary>
        /// <param name="color"></param>
        public void SetColor(Color color)
        {
            color.a = 1f;
            rend.GetPropertyBlock(matPB);
            matPB.SetColor("_Color", color);
            rend.SetPropertyBlock(matPB);
        }

        /// <summary>
        /// fading, alpha goes from 1f to 0f
        /// </summary>
        /// <param name="duration"></param>
        public void FadeOut(float duration = .5f)
        {
            Color color = rend.material.color;
            rend.GetPropertyBlock(matPB);

            float colorA = color.a;
            DOTween.To(() => colorA, x => colorA = x, 0f, duration).SetEase(Ease.InSine)
                .OnUpdate(() =>
                {

                    color.a = colorA;

                    matPB.SetColor("_Color", color);
                    rend.SetPropertyBlock(matPB);

                });
        } 
        #endregion

    }
}
