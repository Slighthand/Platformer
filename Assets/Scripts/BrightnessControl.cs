using UnityEngine;
using UnityEngine.UI;

public class BrightnessControl : MonoBehaviour
{
    public Slider brightness;
    public CanvasGroup CanvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        float brightnesss = PlayerPrefs.GetFloat("brightness", 1f);
        brightness.value = brightnesss;
        SetBrightness(brightnesss);

    }

    public void SetBrightness(float value)
    {
        CanvasGroup.alpha = value;
        PlayerPrefs.SetFloat("brightness", value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
