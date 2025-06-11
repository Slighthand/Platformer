using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : Singleton<VolumeManager> {

    public AudioMixer Mixer;
    public float Scale = 10f;
    public float MinVolume = -10f;
    public float MaxVolume = 10f;
    [SerializeField] private FilteredVolume[] InputVolumes;
    public Dictionary<string, FilteredVolume> FilteredMixerVolumes;

    protected void Awake() {
        FilteredMixerVolumes = new Dictionary<string, FilteredVolume>();
        foreach(FilteredVolume fv in InputVolumes) {
            FilteredMixerVolumes.Add(fv.Name, fv);
        }
    }

    private void UpdateVolume(FilteredVolume fv) {
        Mixer.SetFloat(fv.Field, Mathf.Log10(fv.Val) * Scale);
    }

    public void SetVolume(FilteredVolume fv, float value) {
        fv.Val = Mathf.Max(MinVolume, Mathf.Min(MaxVolume, value));
        UpdateVolume(fv);
    }

    public void SetVolume(string name, float value) {
        SetVolume(FilteredMixerVolumes[name], value);
    }

    public void AddVolume(string name, float value) {
        Debug.Log($"{FilteredMixerVolumes[name].Field} {value}");

        SetVolume(FilteredMixerVolumes[name], FilteredMixerVolumes[name].Val + value);
    }

}

[System.Serializable]
public class FilteredVolume {
    public string Name;
    public string Field;
    public float Val;
}
