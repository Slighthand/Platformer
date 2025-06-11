using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSoundEffects : MonoBehaviour
{
    
    public bool Mute;

    public void PlayClip(AudioClip clip) {
        if (!Mute) {
            SFXSystem.Instance.PlayVariatedSFX(clip);
        }
        
    }

}
