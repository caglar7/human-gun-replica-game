using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Collectable_Gate : Collectable
{
    [SerializeField] Collider otherGate;
    [SerializeField] int addedStickman;
    TextMeshPro tmpro;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        tmpro = GetComponentInChildren<TextMeshPro>();
        tmpro.text = (addedStickman > 0) ? "+" + addedStickman.ToString() : tmpro.text;
        tmpro.text = (addedStickman < 0) ? addedStickman.ToString() : tmpro.text;
    }

    public override void OnCollect()
    {
        otherGate.enabled = false;

        EventManager.GunTransformEvent(addedStickman);

        gameObject.SetActive(false);
    }
}
