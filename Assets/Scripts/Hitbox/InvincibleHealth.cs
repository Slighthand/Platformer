using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Wabubby.Extensions;

public class InvincibleHealth : Health
{

    [SerializeField] float iFrameDuration;

    float canDamageTime;
    
    public override void Damage(float dmg, IDamager source, Transform owner, Collider2D collider=null) {
        base.Damage(dmg, source, owner, collider);

        canDamageTime = Time.time + iFrameDuration;

        StartInvincibilityTime(iFrameDuration);
    }

    public void StartInvincibilityTime(float duration) {
        StartCoroutine(InvincibilityCoroutine(duration));
    }

    void OnEnable()
    {
        canDamage = true;        
    }

    public IEnumerator InvincibilityCoroutine(float duration) {
        canDamage = false;
        yield return GetWait(duration);

        if (Time.time >= canDamageTime -0.02f) canDamage = true;
    }

}
