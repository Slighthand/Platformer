using System.Collections;
using UnityEngine;
using static Wabubby.Extensions;
using static UnityEngine.Mathf;
using System;
using UnityEngine.Tilemaps;
using TMPro;

public class PlayerAttack : MonoBehaviour {

    [Space, Header("ATTACK")]
    [SerializeField] AttackInfo attackInfo;
    [SerializeField, Range(0, 1)] float attackInvincibilityTime = 1;

    [Space, Header("Bomb")]
    [SerializeField] Rigidbody2D bombFab;
    [SerializeField] Tilemap groundTilemap;
    [SerializeField] float bombCount = 5;
    [SerializeField] TextMeshProUGUI BombText;

    [NonSerialized, ShowInDebugInspector] public AttackState state;
    [NonSerialized, ShowInDebugInspector] public int direction = 0;
    [NonSerialized, ShowInDebugInspector] public bool lockDirection;
    float lagTime = 0f;
    bool lagged => Time.time < lagTime;
    PlayerInputCoordinator input;

    PlayerHealth health;
    PlayerMovement movement;

    void Start() {
        input = transform.root.GetComponent<PlayerInputCoordinator>();
        health = GetComponent<PlayerHealth>();
        movement = GetComponent<PlayerMovement>();
        BombText.text = bombCount.ToString();
    }

    void OnDisable() {
        StopAllCoroutines();
        state = AttackState.None;
        attackInfo.hitbox.Deactivate();
    }


    protected void Update() {
        if (!lagged) lockDirection = false;
        if (!lockDirection && input.Movement.x != 0) direction = input.Movement.x > 0 ? 1 : -1;


        if (!lagged && input.Attack) {
            Attack(attackInfo);
        }

        if (!lagged && input.BombAction.WasPressedThisFrame() && bombCount > 0) {
            Rigidbody2D bomba = Instantiate(bombFab);
            bomba.GetComponent<Bomb>().targetTilemap = groundTilemap;
            bomba.transform.position = transform.position;
            bombCount--;
            BombText.text = bombCount.ToString();
        }
    }

    void Attack(AttackInfo attackInfo) { StartCoroutine(AttackCoroutine(attackInfo)); }
    
    IEnumerator AttackCoroutine(AttackInfo attackInfo) {
        // print("yeet");
        state= AttackState.Attack;
        lagTime = Time.time + attackInfo.duration + attackInfo.endLag;
        attackInfo.hitbox.Activate();
        lockDirection = attackInfo.lockDirection;

        health.StartInvincibilityTime(attackInvincibilityTime);
        yield return GetWait(attackInfo.duration);

        attackInfo.hitbox.Deactivate();
        state = AttackState.None;
    }
    

}

public enum AttackState {
    None,
    Attack,
}


[Serializable]
public class AttackInfo
{
    public Hitbox hitbox;
    [Range(0f, 1f)] public float duration;
    [Range(0f, 0.5f)] public float endLag;
    public bool lockDirection; 
}