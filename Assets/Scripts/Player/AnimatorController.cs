using System.Collections;
using UnityEngine;
using static Wabubby.Extensions;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected string anim;
    protected float animNormal => animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    protected float animTime => animator.GetCurrentAnimatorStateInfo(0).length * animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    protected float animLength => animator.GetCurrentAnimatorStateInfo(0).length;

    protected virtual void Awake() {
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        Play("idle");
    }

    protected virtual void OnDisable() { Play("idle"); }

    protected void Play(string newAnim) {
        animator.enabled = true;
        if (anim == newAnim) return;
        anim = newAnim;
        animator.Play(anim);
    }

    protected void PlayOnce(string newAnim) {
        string oldAnim = anim;
        Play(newAnim);
        PlayAfter(oldAnim);
    }

    protected void PlayAfter(string anim) {
        StartCoroutine(PlayAfterCoroutine(anim));
    }

    IEnumerator PlayAfterCoroutine(string anim) {
        yield return null;
        PlayIn(anim, animLength);
    }
    
    protected void PlayIn(string state, float t) {
        StartCoroutine(PlayInCoroutine(state, t));
    }

    IEnumerator PlayInCoroutine(string state, float t) {
        yield return GetWait(t);
        Play(state);
    }

    void DestroyObject() {
        Destroy(gameObject);
    }

}
