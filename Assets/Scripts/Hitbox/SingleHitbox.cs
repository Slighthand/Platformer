using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleHitbox : Hitbox
{
    
    private List<Transform> HitTargets = new List<Transform>();

    public override event HitHandler OnHit;

    protected override void Update() {
        if (!IsActive) return;
        UpdateColliders();
        
        foreach (Collider2D collider in colliders) {
            if (!HitTargets.Contains(collider.transform)) {
                OnHit?.Invoke(collider);
                HitTargets.Add(collider.transform);
            }
        }
    }

    public override void Deactivate() {
        base.Deactivate();
        HitTargets.Clear();
    }

}
