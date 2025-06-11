using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : InvincibleHealth, IKnockbackable
{
    Rigidbody2D physics;

    protected override void Awake() {
        base.Awake();
        physics = GetComponent<Rigidbody2D>();
    }

    public override void Defeat() {
        StartCoroutine(DefeatCoroutine());
    }

    public void Knockback(Vector2 force) {
        physics.velocity = force;
    }

    void ResetPlayer() {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        CurrentHealth = MaxHealth;
    }

    IEnumerator DefeatCoroutine() {
    //     yield return GameManager.Instance.LoadSavepointRoutine(GameManager.Instance.CurrentSavepoint);
        yield return null;
        ResetPlayer();
    }

}
