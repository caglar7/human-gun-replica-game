using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTransformer : MonoBehaviour
{
    List<Gun> gunList = new List<Gun>();
    int stickmanCount = 0;

    private void Awake()
    {
        gunList.AddRange(GetComponentsInChildren<Gun>(true));
    }

    private void NextTransform(int count)
    {
        stickmanCount += count;
        Debug.Log("adding " + count);

    }
}
