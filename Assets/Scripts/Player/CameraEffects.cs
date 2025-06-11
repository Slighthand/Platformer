using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using Wabubby;
using static Wabubby.Extensions;

public class CameraEffects : Singleton<CameraEffects> {

    // public CinemachineVirtualCamera virtualCamera;

    [Header("Control Curves")]
    public AnimationCurve ShakeCurve;

    public void Hitstop(float seconds) {
        if (seconds == 0f) return;
        StartCoroutine(HistopCoroutine(seconds));
    }
    private IEnumerator HistopCoroutine(float seconds) {
        GameSpeedSystem.GameSpeedScale = 0f;
        yield return GetWaitRealtime(seconds);
        GameSpeedSystem.GameSpeedScale = 1f;
    }

    public void ScreenShake(float strength, float duration) { StartCoroutine(ScreenShakeCoroutine(strength, duration)); }
    private IEnumerator ScreenShakeCoroutine(float strength, float duration) {
        if (strength == 0 || duration == 0) { yield break; }

        float t = 0;
        while (t<duration) {
            float _magnitude = ShakeCurve.Evaluate(t / duration) * strength;

            float x = Random.Range(-_magnitude, _magnitude);
            float y = Random.Range(-_magnitude, _magnitude);

            transform.position = new Vector3(x, y, transform.position.z);
            // virtualCamera.GetCinemachineComponent<CinemachineComposer>().m_TrackedObjectOffset = new Vector3(x, y, 0);
            // virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = new Vector3(x, y, 0);

            t += Time.deltaTime;
            yield return null;
        }
    }

    public void ScreenShakeUnscaledTime(float strength, float duration) { StartCoroutine(ScreenShakeCoroutineUnscaled(strength, duration)); }
    private IEnumerator ScreenShakeCoroutineUnscaled(float strength, float duration) {
        if (strength == 0 || duration == 0) { yield break; }

        float t = 0;
        while (t<duration) {
            float _magnitude = ShakeCurve.Evaluate(t / duration) * strength;

            transform.position = new Vector3(Random.Range(-_magnitude, _magnitude), Random.Range(-_magnitude, _magnitude), transform.position.z);

            t += Time.unscaledDeltaTime;
            yield return null;
        }
    }

}
