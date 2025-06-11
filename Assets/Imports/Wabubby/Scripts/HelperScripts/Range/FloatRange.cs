using UnityEngine;
using static UnityEngine.Mathf;

[System.Serializable]
public struct FloatRange {

	public float min, max;

	public float Random {
		get {
			return UnityEngine.Random.Range(min, max);
		}
	}

	public float Clamp(float x) {
		return Mathf.Clamp(x, min, max);
	}
}