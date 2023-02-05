using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectable_Money : Collectable
{
    [SerializeField] float groundLevelY;
    [SerializeField] float zOffset = 10f;
    [SerializeField] float jumpDuration = 1f;
    [SerializeField] float jumpPower = 1f;
    [SerializeField] int jumpCount = 2;
    [SerializeField] Ease ease;

    /// <summary>
    /// object set parent null, jumps down to groundLevelY
    /// new rotation
    /// </summary>
    public void JumpDown()
    {
        Vector3 posNext = transform.position;
        posNext.y = groundLevelY;
        posNext.z += zOffset;
        transform.DOJump(posNext, jumpPower, jumpCount, jumpDuration).SetEase(ease);

        Vector3 rotEuler = transform.eulerAngles;
        rotEuler.x = 180f;
        transform.DORotate(rotEuler, jumpDuration / 2f);

    }

    /// <summary>
    ///  removing this object and get a small money mesh from pool
    ///  moves up to UI, updating the money count on UI
    /// </summary>
    public override void OnCollect()
    {
        // implementation later on
        print("money collected");
        gameObject.SetActive(false);

    }
}
