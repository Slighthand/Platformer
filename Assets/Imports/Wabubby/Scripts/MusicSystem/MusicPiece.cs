using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MusicPiece
{

    public string Name;
    public MusicTransition StartingClips;
    public MusicClip Clip;
    public MusicTransition EndingClips;
    

    public MusicPiece(string name, MusicTransition defaultStart, MusicClip clip, MusicTransition defaultEnd) {
        Name = name;
        StartingClips = defaultStart;
        Clip = clip;
        EndingClips = defaultEnd;
    }

    public void Play(MusicSystem musicSystem) {
        MusicClip[] clips = new MusicClip[StartingClips.MusicClips.Length + 1 + EndingClips.MusicClips.Length];

        int i = 0;
        foreach (MusicClip musicClip in StartingClips.MusicClips) { clips[i] = musicClip; i += 1; }
        clips[i] = Clip; i += 1;
        foreach (MusicClip musicClip in EndingClips.MusicClips) { clips[i] = musicClip; i += 1; }


        musicSystem.PlayClips(this, clips);
    }


    public void Queue(MusicSystem musicSystem) {
        MusicClip[] clips = new MusicClip[StartingClips.MusicClips.Length + 1 + EndingClips.MusicClips.Length];

        int i = 0;
        foreach (MusicClip musicClip in StartingClips.MusicClips) { clips[i] = musicClip; i += 1; }
        clips[i] = Clip; i += 1;
        foreach (MusicClip musicClip in EndingClips.MusicClips) { clips[i] = musicClip; i += 1; }


        musicSystem.QueueClips(this, clips);
    }

}
