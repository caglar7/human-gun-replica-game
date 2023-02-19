using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Editing level end visuals in edit mode
/// instead of placing objects statically, we use a platform prefab and a starting pos
/// then generating level end models based on preferences
/// </summary>

namespace GAME
{
    public class LevelEndEdit : MonoBehaviour
    {
        #region Properties
        [Header("Settings")]
        [SerializeField] int partCount;
        [SerializeField] Color[] colors;

        [Header("References")]
        [SerializeField] GameObject prefab;
        [SerializeField] Transform startPoint;
        Vector3 nextPos;
        float zDiff;
        bool isUpdatedZ;
        private const int COUNT_LIMIT = 20;
        #endregion

        #region Validate Methods

#if UNITY_EDITOR
        /// <summary>
        /// on validate is called to update level end models
        /// can directly call the update here in OnValidate but
        /// it logs out too many annoying warnings, we update with delay to bypass some logs
        /// </summary>
        private void OnValidate()
        {
            UnityEditor.EditorApplication.delayCall += _OnValidate;
        }

        private void _OnValidate()
        {
            if (this == null) return;

            UpdateLevelEnd();
        }

#endif

        #endregion

        #region Methods

        /// <summary>
        /// update
        /// </summary>
        private void UpdateLevelEnd()
        {
            if (partCount < 0 || partCount > COUNT_LIMIT)
                Debug.LogWarning("Bounds: " + "(0, " + COUNT_LIMIT + ")");


            partCount = Mathf.Clamp(partCount, 0, COUNT_LIMIT);
            ShowParts(partCount);
        }

        /// <summary>
        /// if parts already instantiated, there is no need to do it again
        /// using the parts that already exist and adding more if needed
        /// 
        /// adjust for model positions
        /// 
        /// setting x2, x3 texts 
        /// 
        /// setting colors
        /// 
        /// </summary>
        /// <param name="count"></param>
        private void ShowParts(int count)
        {
            if (count == 0)
            {
                foreach (LevelEndPart part in GetParts())
                {
                    DestroyImmediate(part.gameObject);
                }
                return;
            }

            int alreadyHave = GetParts().Length;
            isUpdatedZ = false;

            for (int i = 0; i < count; i++)
            {
                GameObject g = null;

                if (i < alreadyHave) g = GetParts()[i].gameObject;
                else g = GenerateEndPlatform();

                SetZDiff(g);

                nextPos = startPoint.localPosition + (Vector3.forward * zDiff * i);

                g.transform.localPosition = nextPos;
                g.SetActive(true);

                SetCountText(g, i + 2);

                SetColor(g, i);

                SetShootingHealths(g, (i * 5) + (i == 0 ? 2 : 0));
            }

            if (alreadyHave > count)
            {
                for (int i = count; i < alreadyHave; i++)
                {
                    GetParts()[i].gameObject.SetActive(false);
                }
            }
        }

        /// <summary>
        /// get all LevelEndPart.cs active or not
        /// </summary>
        /// <returns></returns>
        private LevelEndPart[] GetParts()
        {
            return transform.GetComponentsInChildren<LevelEndPart>(true);
        }

        /// <summary>
        /// get a level end platform object, parent is transform
        /// </summary>
        /// <returns></returns>
        private GameObject GenerateEndPlatform()
        {
            GameObject clone = Instantiate(prefab, transform);
            return clone;
        }

        /// <summary>
        /// z diff is obtained from renderer bounds
        /// 
        /// zDiff is used to position objects in right places
        /// 
        /// </summary>
        /// <param name="g"></param>
        private void SetZDiff(GameObject g)
        {
            if (isUpdatedZ) return;

            zDiff = g.GetComponent<MeshRenderer>().bounds.extents.z * 2f;
            isUpdatedZ = true;
        }

        /// <summary>
        /// setting x2, x3 etc. texts
        /// </summary>
        /// <param name="g"></param>
        /// <param name="value"></param>
        private void SetCountText(GameObject g, int value)
        {
            foreach (TextMeshPro tm in g.GetComponentsInChildren<TextMeshPro>())
            {
                if (tm.gameObject.GetComponent<Text_CountX>())
                {
                    tm.text = "x" + value.ToString();
                    break;
                }
            }
        }

        /// <summary>
        /// on level end shooting healths goes like 2, 10, 20, 30
        /// this method sets all 3 of them on a level end model
        /// </summary>
        /// <param name="g"></param>
        /// <param name="health"></param>
        private void SetShootingHealths(GameObject g, int health)
        {
            foreach (ShootingTarget t in g.GetComponentsInChildren<ShootingTarget>())
            {
                t.SetInitHealth(health);
            }
        }

        /// <summary>
        /// setting colors with prob block
        /// 
        /// no need to create additional materials for each new object
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="index"></param>
        private void SetColor(GameObject g, int index)
        {
            int indexMod = index % colors.Length;

            MaterialPropertyBlock probBlock = new MaterialPropertyBlock();
            Renderer rend = g.GetComponent<Renderer>();

            rend.GetPropertyBlock(probBlock, 0);
            probBlock.SetColor("_Color", colors[indexMod]);
            rend.SetPropertyBlock(probBlock, 0);
        }

        #endregion
    }

}