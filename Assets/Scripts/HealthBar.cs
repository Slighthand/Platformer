using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] Slider slider;
    [SerializeField] Image sliderfill;
    [SerializeField] Gradient gradient;

    void OnEnable() { health.OnHealthUpdate += SetHealthSlider; }
    void OnDisable() { health.OnHealthUpdate -= SetHealthSlider; }

    void Start() { SetHealthSlider(); }


    public void SetHealthSlider() {
        slider.value = (float)health.CurrentHealth / health.MaxHealth;
        if (sliderfill != null && gradient != null) sliderfill.color = gradient.Evaluate(slider.value);
    }
}
