using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;

    [SerializeField] Health health;
    [SerializeField] Slider slider;
    [SerializeField] Image sliderfill;
    [SerializeField] Gradient gradient;

    void OnEnable() { health.OnHealthUpdate += SetHealthSlider; }
    void OnDisable() { health.OnHealthUpdate -= SetHealthSlider; }

    void Start() { SetHealthSlider(); }

    public void SetHealthSlider()
    {
        slider.maxValue = health.MaxHealth;
        slider.value = health.CurrentHealth;

        if (sliderfill != null && gradient != null)
        {
            float value = (float)health.CurrentHealth / health.MaxHealth;
            sliderfill.color = gradient.Evaluate(value);
        }

        if (sliderfill == null)
        {
            SceneManager.LoadScene("EndScreen");
        }
    }

    
    public void AddExtraHeart(int amount)
    {
        MaxHealth += amount;
        CurrentHealth += amount;

        
        health.MaxHealth = MaxHealth;
        health.CurrentHealth = CurrentHealth;
        
    }
}