using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTransformer : MonoBehaviour
{
    List<Gun> gunList = new List<Gun>();

    private void Awake()
    {
        gunList.AddRange(GetComponentsInChildren<Gun>(true));
    }


}