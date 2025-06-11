using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetedHitbox : Hitbox
{
    
    public Transform Target;

    public override event HitHandler OnHit;

    protected override void Update() {
        if (!IsActive) return;
        UpdateColliders();
        
        foreach (Collider2D collider in colliders) {
            if (collider.transform == Target) OnHit?.Invoke(collider);
        }
        
    }

}
