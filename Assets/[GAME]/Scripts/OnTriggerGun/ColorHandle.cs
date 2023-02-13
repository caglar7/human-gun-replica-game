using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHandle : MonoBehaviour
{
    MaterialPropertyBlock matPB;
    Renderer rend;

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

    public void SetColor(Color color)
    {
        rend.GetPropertyBlock(matPB);
        matPB.SetColor("_Color", color);
        rend.SetPropertyBlock(matPB);
    }

    public void SetColorSmooth()
    {

    }
}
