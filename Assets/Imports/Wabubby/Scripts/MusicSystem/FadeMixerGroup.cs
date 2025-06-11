using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
public static class FadeMixerGroup {

    public static IEnumerator StartFadeCoroutine(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume) // StartCorountine only available for Monobehaviorus D: could make a wrapper but lazy that's prolly not good
    {
        float currentTime = 0;
        float currentVol;

        audioMixer.GetFloat(exposedParam, out currentVol);

        currentVol = Mathf.Pow(10, currentVol / 20);

        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);

            Debug.Log(newVol);

            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }
        
        yield break;
    }
}
