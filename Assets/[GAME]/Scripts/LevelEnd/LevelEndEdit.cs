using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndEdit : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int partCount;
    [SerializeField] Color[] colors;

    [Header("References")]
    [SerializeField] GameObject prefab;
    [SerializeField] Transform startPoint;

    List<Transform> parts;
    float zDiff;
    bool isInitCalled;

    private void OnValidate()
    {
        UpdateLevelEnd();
    }

    private void UpdateLevelEnd()
    {
        if(partCount > 0 && colors.Length > 0)
        {
            Init();

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
        Destroy(clone, 1f);

        parts = new List<Transform>();

        isInitCalled = true;

        print("init call");
    }

    /// <summary>
    ///  generate an end platform
    ///  this works in edit mode so im instantiating like there is no tomorrow
    /// </summary>
    /// <param name="flags"></param>
    /// <returns></returns>
    private GameObject GenerateEndPlatform(HideFlags flags = HideFlags.None)
    {
        GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        clone.hideFlags = flags;
        return clone;
    }

    /// <summary>
    ///  remove the currently active ones
    /// </summary>
    private void RemovePlatforms()
    {
        //foreach(Transform t in parts)
        //{
        //    t.gameObject.SetActive(false);
        //}

        // destroy fist later with setactive logic
        for (int i = 0; i < parts.Count; i++)
        {
            Transform t = parts[0];
            parts.RemoveAt(0);
            Destroy(t.gameObject);
        }
    }

    private void AddPlatforms(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject clone = GenerateEndPlatform();
            parts.Add(clone.transform);
        }
    }
}
