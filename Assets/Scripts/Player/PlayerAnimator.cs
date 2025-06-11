using UnityEngine;
using static Wabubby.Extensions;

public class PlayerAnimator : AnimatorController
{
    [SerializeField] SpriteRenderer attackSprite;

    PlayerInputCoordinator input;
    Rigidbody2D physics;
    PlayerHealth health;
    PlayerAttack attack;
    PlayerMovement movement;

    // CONTROL VARIABLES
    [System.NonSerialized, ShowInDebugInspector] public int direction;
    float hurtTime = -100f;

    void Awake() {
        physics = GetComponent<Rigidbody2D>();
        input = transform.root.GetComponent<PlayerInputCoordinator>();
        attack = GetComponent<PlayerAttack>();
        movement = GetComponent<PlayerMovement>();

        direction = 1;
        Play("idle");
    }

    void OnEnable() {
        health = GetComponent<PlayerHealth>();
        health.OnHealthDown += OnHurt;
    }

    void OnDisable() {
        // reset the default state of the attack sprite to NOTHING
        attackSprite.sprite = null;
    }


    public void OnHurt(float Health) {
        hurtTime = Time.time;
    }

    void NormalAnim() {
        if (Mathf.Abs(physics.velocity.x) <= 0.01f && Mathf.Abs(physics.velocity.y) <= 0.01f) {
            bool blinking = anim == "blink" && animNormal < 1;
            bool endIdle = anim == "idle" && animNormal>1 && animNormal%1 <= Time.deltaTime && Random.Range(0, 3) == 2;

            if (blinking || endIdle) {
                Play("blink");
            } else {
                Play("idle");
            }
        } else {
            Play("walk");
        }
        
    }

    void LateUpdate() {
        if (Mathf.Abs(physics.velocity.x) > 0.05f ) {
            direction = physics.velocity.x > 0 ? 1 : -1;
        }
        if (attack.state != AttackState.None) {
            direction = attack.direction;
        }
        
        AttackState attackState = attack.state;

        if (Time.time - hurtTime < 0.1f) {
            Play("hurt");
        }
        else if (attackState != AttackState.None) {
            print("attack anim");
            Play("attack");
        }
        else {
            NormalAnim();
        }
        transform.localScale = new Vector3(direction, 1, 1);
    }

}
