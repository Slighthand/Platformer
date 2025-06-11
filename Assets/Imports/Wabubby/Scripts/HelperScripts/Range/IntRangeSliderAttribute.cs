using UnityEngine;

public class IntRangeSliderAttribute : PropertyAttribute {

	public int Min { get; private set; }

	public int Max { get; private set; }

	public IntRangeSliderAttribute (int min, int max) {
		if (max < min) {
			max = min;
		}
		Min = min;
		Max = max;
	}
}