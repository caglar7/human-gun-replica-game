using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Gun Data", menuName ="Gun Data")]
public class GunData : ScriptableObject
{
    [Header("Gun Stats")]
    public new string name;
    public int id;
    public int attackDamage;
    public float range;
    public float shootingPeriod;
    public float bulletSpeed;

    [Header("Stickman Range")]
    public int min;
    public int max;
}
