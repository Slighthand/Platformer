using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TemporarySoundSource : MonoBehaviour
{
    public Vector3 velocity;
    public AudioSource EffectsSource;

    public void StartSelfDestruct() {
        Destroy(transform.gameObject, EffectsSource.clip.length-EffectsSource.time);
    }

    private void FixedUpdate() {
        transform.position += velocity;
    }
    
}
