using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using static Wabubby.Extensions;

public class SFXSystem : Singleton<SFXSystem>
{
    public AudioSource SFXSource;
    public float LowPitchRange = 0.9f;
	public float HighPitchRange = 1.1f;
    public float LowVolumeRange = .9f;
    public float HighVolumeRange = 1.1f;

	public Transform TemporarySoundSourcePrefab;

    public AudioClip test1;

    public void Test(InputAction.CallbackContext callbackContext) {
        if (callbackContext.performed) {
        	StartCoroutine(TestCoroutine());
        }
    }

	public IEnumerator TestCoroutine() {
		PlayVariatedSFXAtPoint(test1, new Vector3(0, 0, -10));
		yield return GetWait(1);
		PlayVariatedSFXAtPoint(test1, new Vector3(-10, 0, -10));
		yield return GetWait(1);
		PlayVariatedSFXAtPoint(test1, new Vector3(10, 0, -10));
		yield return GetWait(1);
	}

	private AudioSource CreateTemporarySoundSource(AudioClip clip, Vector3 position, Vector3 velocity) {
		Transform tssp = Instantiate(TemporarySoundSourcePrefab, position, new Quaternion()); // TSSSP!
		TemporarySoundSource tss = tssp.GetComponent<TemporarySoundSource>(); // TSS!
		tss.velocity = velocity;

		AudioSource ass = tssp.GetComponent<AudioSource>(); // ASS!
		ass.clip = clip;
		ass.Play();
		tss.StartSelfDestruct();

		return ass; // get that ass out of here i hope no one ever reads this code
	}

	public void PlayVariatedSFXAtMovingPoint(AudioClip clip, Vector3 position, Vector3 velocity){
		float randomPitch = Random.Range(LowPitchRange, HighPitchRange);
        float randomVolume = Random.Range(LowVolumeRange, HighVolumeRange);
		
		AudioSource ass = CreateTemporarySoundSource(clip, position, new Vector3(0, 0, 0));
		ass.volume = randomVolume;
		ass.pitch = randomPitch;
		ass.outputAudioMixerGroup = SFXSource.outputAudioMixerGroup;
	}

    public void PlayVariatedSFXAtPoint(AudioClip clip, Vector3 position){
		PlayVariatedSFXAtMovingPoint(clip, position, new Vector3(0, 0, 0));
	}

    public void PlayVariatedSFX(AudioClip clip){
		float randomPitch = Random.Range(LowPitchRange, HighPitchRange);
        float randomVolume = Random.Range(LowVolumeRange, HighVolumeRange);

		SFXSource.pitch = randomPitch;
		SFXSource.PlayOneShot(clip, randomVolume);
	}

    public void RandomSoundEffect(params AudioClip[] clips)
	{
		int randomIndex = Random.Range(0, clips.Length);
		float randomPitch = Random.Range(LowPitchRange, HighPitchRange);

		SFXSource.pitch = randomPitch;
		SFXSource.clip = clips[randomIndex];
		SFXSource.Play();
	}
}
