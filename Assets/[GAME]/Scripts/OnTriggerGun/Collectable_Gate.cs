using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Collectable_Gate : OnTriggerGun
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

    public override void OnTrigger()
    {
        otherGate.enabled = false;

        gameObject.SetActive(false);

        EventManager.StickmanUpdateEvent(addedStickman, null, false);
    }
}
