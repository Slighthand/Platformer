using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioMixer audioMixer;

    void Start()
    {
        float volume = PlayerPrefs.GetFloat("Volume", 0.75f);
        volumeSlider.value = volume;
        SetVolume(volume);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20); 
        PlayerPrefs.SetFloat("Volume", volume);
    }
}