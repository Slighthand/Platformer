using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MusicClip {
    
    public enum ClipType {
        Silence,
        Play, 
        Loop, 
        PlayOnce
    }

    public ClipType Type;

    public float duration;
    public float Duration { 
        get { return (Type == ClipType.PlayOnce) ? Clip.length : duration; }
        set { duration = value; }
    }
    public AudioClip Clip;
    public bool FadeInFlag;
    public bool FadeOutFlag;

    public MusicClip(float duration) { // silent type
        Type = ClipType.Silence;
        Duration = duration;
    }

    public MusicClip(AudioClip clip, float duration, bool fadeInFlag=true, bool fadeOutFlag=true) { // silent type
        Type = ClipType.Play;

        Clip = clip;
        Duration = duration;
        FadeInFlag = fadeInFlag;
        FadeOutFlag = fadeOutFlag;
    }

}
