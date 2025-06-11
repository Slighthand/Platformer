using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using static Wabubby.Extensions;

public class MusicSystem : Singleton<MusicSystem>
{

    public int DefaultMusicVolume = -20;
    public MusicPiece test1;
    public bool PlayOnAwake;

    [HideInInspector] public AudioMixer MasterMixer;
    public AudioSource MusicSource;
    public AudioSource FadeMusicSource;

    private int PrimaryMusicSourceIndex = 1;
    private Queue<MusicClip> MusicClipQueue = new Queue<MusicClip>();
    private MusicPiece CurrentMusicPiece;
    private MusicClip CurrentMusicClip;

    public MusicPieceFactory clips;

	protected void Awake() {
        MasterMixer = MusicSource.outputAudioMixerGroup.audioMixer;

        // clips.MusicPieces[0].Play(this);

        foreach (MusicPiece musicPiece in clips.MusicPieces) {
            musicPiece.Queue(this);
        }

        PlayNextClip();
    }




    private void Start() {
        MasterMixer.SetFloat($"Music{1}Volume", DefaultMusicVolume);
        MasterMixer.SetFloat($"Music{2}Volume", DefaultMusicVolume);
    }

    
    public void PlayClips(MusicPiece ownerPiece, MusicClip[] musicClips) { // music piece's implementation has complete physics over what is enqueued
        StopAllCoroutines();
        
        CurrentMusicPiece = ownerPiece;
 
        MusicClipQueue.Clear();
        foreach (MusicClip clip in musicClips) { MusicClipQueue.Enqueue(clip); }
        
        PlayNextClip();
    }

    public void QueueClips(MusicPiece ownerPiece, MusicClip[] musicClips) { // music piece's implementation has complete physics over what is enqueued
        foreach (MusicClip clip in musicClips) { MusicClipQueue.Enqueue(clip); }
    }


    public void FadeOut(MusicClip musicClip, float time, double duration=0.7d) {
        // swap two source references. new music will now be played in the second source.
        AudioSource temp = FadeMusicSource;
        FadeMusicSource = MusicSource;
        MusicSource = temp;

        PrimaryMusicSourceIndex = PrimaryMusicSourceIndex == 1 ? 2: 1; // flip the primary source index, for correct music volume param
        int SecondaryMusicSourceIndex = PrimaryMusicSourceIndex == 1 ? 2: 1;
        // whatever poor sap in the future decides to deciper this. i pity you.

        MasterMixer.SetFloat($"Music{1}Volume", DefaultMusicVolume); // reset both because yes, hopefully no static
        MasterMixer.SetFloat($"Music{2}Volume", DefaultMusicVolume);

        StartCoroutine(FadeMixerGroup.StartFadeCoroutine(MasterMixer, $"Music{SecondaryMusicSourceIndex}Volume", 5, -80f));
        FadeMusicSource.SetScheduledEndTime(AudioSettings.dspTime + 5);
    }

	public void PlayNextClip() {
        if (MusicClipQueue.Count == 0) { return; }

        if (MusicClipQueue.Peek().FadeInFlag) {
            // fade out the old song silmutaneously
            
            AudioSource temp = FadeMusicSource;
            FadeMusicSource = MusicSource;
            MusicSource = FadeMusicSource;

            FadeOut(CurrentMusicClip, MusicSource.time);
        }

        CurrentMusicClip = MusicClipQueue.Peek();
        MusicClipQueue.Dequeue();

        if (CurrentMusicClip.Type == MusicClip.ClipType.Silence) {
            MusicSource.Pause();
        } else if (CurrentMusicClip.Type == MusicClip.ClipType.Play) {
            MusicSource.loop = true;
            MusicSource.clip = CurrentMusicClip.Clip;
            MusicSource.Play();
        } else if (CurrentMusicClip.Type == MusicClip.ClipType.Loop) {
            MusicSource.loop = true;
            MusicSource.clip = CurrentMusicClip.Clip;
            MusicSource.Play();
            MusicClipQueue.Clear();
        } else if (CurrentMusicClip.Type == MusicClip.ClipType.PlayOnce) {
            MusicSource.loop = false;
            MusicSource.clip = CurrentMusicClip.Clip;
            MusicSource.Play();
        }
        
        PlayNextClipLater(CurrentMusicClip.Duration);
	}

    public void PlayNextClipLater(float delay) {
        StartCoroutine(PlayNextClipScheduledCorountine(delay));
    }

    private IEnumerator PlayNextClipScheduledCorountine(float delay) {
        yield return GetWait(delay);

        PlayNextClip();
    }

}