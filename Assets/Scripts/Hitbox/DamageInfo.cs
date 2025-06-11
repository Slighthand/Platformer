using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamageInfo
{
    [Header("Properties")]
    public float Damage = 1;
    [Range(-90, 90)] public float KnockBackAngle = 45f;
    [Range(0, 100)] public float KnockbackPower = 0;
    [Range(0, 100)] public float StunTime = 0;
    public bool knockbackInheritsVelocity;

    [Header("Effects")]
    [Range(0, 1)] public float hitStopDuration;
    [Range(0, 1)] public float shakiness;
    public GameObject particleFab;


    public DamageInfo(int Damage, int KnockbackPower=1) {
        this.Damage = Damage;
        this.KnockbackPower = KnockbackPower;
    }

}
