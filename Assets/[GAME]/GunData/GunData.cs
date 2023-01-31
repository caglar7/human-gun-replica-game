using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Gun Data", menuName ="Gun Data")]
public class GunData : ScriptableObject
{
    public new string name;
    public int gunLevel;
    public int attackDamage;
    public float range;
}
