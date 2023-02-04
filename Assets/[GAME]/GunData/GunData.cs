using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Gun Data", menuName ="Gun Data")]
public class GunData : ScriptableObject
{
    [Header("Gun Stats")]
    public new string name;
    public int attackDamage;
    public float range;

    [Header("Stickman Range")]
    public int rangeMin;
    public int rangeMax;
}
