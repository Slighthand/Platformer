using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {
    public string MixerName;
    private Slider slider;

    private void Start() {
        slider = GetComponent<Slider>();
        slider.minValue = VolumeManager.Instance.MinVolume;
        slider.maxValue = VolumeManager.Instance.MaxVolume;
    }

    private void Update() {
        slider.value = VolumeManager.Instance.FilteredMixerVolumes[MixerName].Val;
    }

}
