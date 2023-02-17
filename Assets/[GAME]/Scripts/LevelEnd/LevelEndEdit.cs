using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  keep the track of the object in this transform
///  working with child cound is more cleaner
/// </summary>

[ExecuteInEditMode]
public class LevelEndEdit : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] bool updateActive;
    [SerializeField] int partCount;
    [SerializeField] Color[] colors;

    [Header("References")]
    [SerializeField] GameObject prefab;
    [SerializeField] Transform startPoint;

    List<Transform> parts = new List<Transform>();
    float zDiff;

    private void OnValidate()
    {
        if (!updateActive) return;

        
    }

    private void UpdateLevelEnd()
    {
        if (partCount > 0 && colors.Length > 0)
        {

            RemovePlatforms();

            AddPlatforms(partCount);
        }
    }


    /// <summary>
    ///  remove the currently active ones
    /// </summary>
    private void RemovePlatforms()
    {
        foreach(LevelEndPart part in transform.GetComponentsInChildren<LevelEndPart>(false))
        {
            part.gameObject.SetActive(false);
        }
    }

    private void AddPlatforms(int count)
    {
        LevelEndPart[] currentParts = transform.GetComponentsInChildren<LevelEndPart>(false);

        for (int i = 0; i < count; i++)
        {
            if (i < currentParts.Length) currentParts[i].gameObject.SetActive(true);
            else GenerateEndPlatform();
        }
    }

    /// <summary>
    ///  generate an end platform
    ///  this works in edit mode so im instantiating like there is no tomorrow
    /// </summary>
    /// <param name="flags"></param>
    /// <returns></returns>
    private GameObject GenerateEndPlatform()
    {
        GameObject clone = Instantiate(prefab, transform);
        return clone;
    }
}
