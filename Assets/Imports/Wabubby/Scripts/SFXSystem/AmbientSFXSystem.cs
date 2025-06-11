using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Wabubby.Extensions;

public class AmbientSFXSystem : Singleton<AmbientSFXSystem>
{
    [System.Serializable]
    public struct SFXCollection {
        public string Name;
        public float Delay;
        public float DelayRange;
        public List<AudioClip> clips;
    }

    public AudioSource AmbienceSFXSource;

    public SFXCollection[] AmbientSFXCollections;

    public Dictionary<SFXCollection, Coroutine> AmbienceCorountines;

    private void Awake() {
        AmbienceCorountines = new Dictionary<SFXCollection, Coroutine>(AmbientSFXCollections.Length);

        foreach (SFXCollection ambientSFXColleciton in AmbientSFXCollections) {
            AmbienceCorountines[ambientSFXColleciton] = StartCoroutine(PlayAmbience(ambientSFXColleciton));
        }
    }

    private IEnumerator PlayAmbience(SFXCollection ambientSFXColleciton) {
        for (;;) {
            yield return GetWait(Mathf.Max(ambientSFXColleciton.Delay + Random.Range(-ambientSFXColleciton.DelayRange, ambientSFXColleciton.DelayRange), 0.1f));
            AmbienceSFXSource.PlayOneShot(ambientSFXColleciton.clips[Random.Range(0, ambientSFXColleciton.clips.Count)]);
        }
    } 

}
