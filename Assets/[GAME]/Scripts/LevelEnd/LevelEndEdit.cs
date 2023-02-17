using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
///  keep the track of the object in this transform
///  working with child cound is more cleaner
/// </summary>

public class LevelEndEdit : MonoBehaviour
{
    [Header("Settings")]
    //[SerializeField] bool updateActive;
    [SerializeField] int partCount;
    [SerializeField] Color[] colors;

    [Header("References")]
    [SerializeField] GameObject prefab;
    [SerializeField] Transform startPoint;
    Vector3 nextPos;
    float zDiff;
    bool isUpdatedZ = false;
    private const int COUNT_LIMIT = 20;

#if UNITY_EDITOR
    private void OnValidate()
    {
        //if (!updateActive) return;

        print("Validate Called");

        UnityEditor.EditorApplication.delayCall += _OnValidate;
    }

    private void _OnValidate()
    {
        if (this == null) return;

        UpdateLevelEnd();
    }
#endif

    private void UpdateLevelEnd()
    {
        if(partCount < 0 || partCount > COUNT_LIMIT)
            Debug.LogWarning("Bounds: " + "(0, " + COUNT_LIMIT + ")");


        partCount = Mathf.Clamp(partCount, 0, COUNT_LIMIT);
        ShowParts(partCount);
    }

    private void ShowParts(int count)
    {
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
        }

        if(alreadyHave > count)
        {
            for(int i=count; i<alreadyHave; i++)
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

    private GameObject GenerateEndPlatform()
    {
        GameObject clone = Instantiate(prefab, transform);
        return clone;
    }

    private void SetZDiff(GameObject g)
    {
        if (isUpdatedZ) return;

        zDiff = g.GetComponent<MeshRenderer>().bounds.extents.z * 2f;
        isUpdatedZ = true;
    }

    private void SetCountText(GameObject g, int value)
    {
        g.GetComponentInChildren<TextMeshPro>().text = "x" + value.ToString();
    }

    private void SetColor(GameObject g, int index)
    {
        int indexMod = index % colors.Length;

        MaterialPropertyBlock probBlock = new MaterialPropertyBlock();
        Renderer rend = g.GetComponent<Renderer>();

        rend.GetPropertyBlock(probBlock, 0);
        probBlock.SetColor("_Color", colors[indexMod]);
        rend.SetPropertyBlock(probBlock, 0);
    }
}
