using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDamager : MonoBehaviour
{
    public DamageInfo DamageInfo;

    public delegate void DamageHandler(); public DamageHandler OnDamage;

    [HideInInspector, ShowInDebugInspector] public Hitbox Hitbox;

    void Awake() {
        Hitbox = GetComponent<Hitbox>();
        // if (transform.parent.TryGetComponent(out PhysicsController physicsController)){
        //     physics = physicsController;
        // }
    }

    void OnEnable() { Hitbox.OnHit += Hit; }
    void OnDisable() { Hitbox.OnHit -= Hit; }

    protected virtual void Hit(Collider2D collider) {
        if (collider.TryGetComponent(out Health health)) {
            if (!health.canDamage) return;

            HitHealth(health);
            HitKnockback(collider.gameObject);
            HitStun(collider.gameObject);

            OnDamage?.Invoke();
            DamageEffects(health.transform);
        }
    }

    protected virtual void HitHealth(Health health) {
        health.Damage(DamageInfo.Damage, this, transform.parent);
    }

    protected virtual void HitKnockback(GameObject gameObject) {
        if (DamageInfo.KnockbackPower == 0f) return;
        
        if (gameObject.TryGetComponent(out IKnockbackable victim)) {
            float facingDirection = -Mathf.Sign(transform.parent.position.x - gameObject.transform.position.x); // transform.lossyScale.x < 0 ? -1 : 1;
            float angle = DamageInfo.KnockBackAngle*Mathf.Deg2Rad;
            Vector2 angleDirection = new Vector2(Mathf.Cos(angle)*facingDirection, Mathf.Sin(angle));
            
            victim.Knockback(DamageInfo.KnockbackPower * angleDirection);
        }
    }
    
    protected virtual void HitStun(GameObject gameObject)
    {
        if (DamageInfo.StunTime == 0f) return;

        if (gameObject.TryGetComponent(out IStunnable victim)) victim.Stun(DamageInfo.StunTime);
    }

    protected void DamageEffects(Transform other) {
        if (DamageInfo.particleFab != null) {
            float facingDirection = -Mathf.Sign(transform.position.x - gameObject.transform.position.x); // transform.lossyScale.x < 0 ? -1 : 1;
            float angle = DamageInfo.KnockBackAngle*Mathf.Deg2Rad;

            GameObject hitParticle = Instantiate(DamageInfo.particleFab);
            hitParticle.transform.position = transform.position;
            hitParticle.transform.rotation = Quaternion.Euler(0, 0, -angle+90);
        }

        CameraEffects.Instance.Hitstop(DamageInfo.hitStopDuration);
        CameraEffects.Instance.ScreenShakeUnscaledTime(DamageInfo.shakiness*10000f, DamageInfo.hitStopDuration);
    }
}
