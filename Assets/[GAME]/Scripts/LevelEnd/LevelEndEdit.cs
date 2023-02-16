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
    [SerializeField] int partCount;
    [SerializeField] Color[] colors;

    [Header("References")]
    [SerializeField] GameObject prefab;
    [SerializeField] Transform startPoint;

    List<Transform> parts = new List<Transform>();
    float zDiff;
    bool isInitCalled;

    private void OnEnable()
    {
        // testing
        //isInitCalled = false;
        //Init();
    }

    private void OnValidate()
    {
        // do it with finding LevelEndPart.cs in children
        // ...

        //UpdateLevelEnd();
    }

    private void UpdateLevelEnd()
    {
        if(partCount > 0 && colors.Length > 0)
        {
            // testing
            //Init();

            RemovePlatforms();

            AddPlatforms(partCount);
        }
    }

    /// <summary>
    /// pull one reference and find bounds for z
    /// init list
    /// </summary>
    /// <returns></returns>
    private void Init()
    {
        if (isInitCalled) return;

        GameObject clone = GenerateEndPlatform(HideFlags.HideInHierarchy);
        zDiff = clone.GetComponent<MeshRenderer>().bounds.extents.x * 2f;
        clone.gameObject.SetActive(false);

        parts = new List<Transform>();

        isInitCalled = true;

    }

    /// <summary>
    ///  generate an end platform
    ///  this works in edit mode so im instantiating like there is no tomorrow
    /// </summary>
    /// <param name="flags"></param>
    /// <returns></returns>
    private GameObject GenerateEndPlatform(HideFlags flags = HideFlags.None)
    {
        GameObject clone = Instantiate(prefab, transform);
        clone.hideFlags = flags;
        return clone;
    }

    /// <summary>
    ///  remove the currently active ones
    /// </summary>
    private void RemovePlatforms()
    {

        foreach (Transform t in parts)
        {
            if(t) t.gameObject.SetActive(false);
        }
    }

    private void AddPlatforms(int count)
    {
        //if(count > parts.Count)
        //{
        //    int generateCount = count - parts.Count;
        //    foreach (Transform t in parts)
        //    {
        //        t.gameObject.SetActive(true);
        //    }

        //    for (int i = 0; i < generateCount; i++)
        //    {
        //        GameObject clone = GenerateEndPlatform();
        //        parts.Add(clone.transform);
        //    }
        //}
        //else
        //{

        //}

        int listCount = parts.Count;
        for (int i = 0; i < count; i++)
        {
            if(i < listCount)
            {
                parts[i].gameObject.SetActive(true);
            }
            else
            {
                GameObject clone = GenerateEndPlatform();
                parts.Add(clone.transform);
            }
        }
        
    }
}
