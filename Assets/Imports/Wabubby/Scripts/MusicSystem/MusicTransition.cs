using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MusicTransition 
{
    // a variety of transition indicators. fade outs, silence, or heck, even a clip
    // fade ins could be easily included but are not - almost all games i have played have a composed beginning - why wouldn't you. talent? yeah. D:

    [SerializeField]
    public MusicClip[] MusicClips;

    public MusicTransition(MusicClip[] musicClips) {
        MusicClips = musicClips;
    }

}
