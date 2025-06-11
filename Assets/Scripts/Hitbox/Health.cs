using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] protected float maxHealth;
    public virtual float MaxHealth
    {
        get => maxHealth;
        set {
            maxHealth = Mathf.Max(0f, value);
            OnMaxHealthUpdate?.Invoke(value);
            OnHealthUpdate?.Invoke();
        }
    }

    [SerializeField] protected float currentHealth;
    public virtual float CurrentHealth 
    {
        get => currentHealth;
        set {
            if (value < currentHealth && !canDamage) return;

            float newHealth = Mathf.Clamp(value, 0f, MaxHealth);
            if (newHealth > currentHealth) { currentHealth = newHealth; OnHealthUp?.Invoke(newHealth); }
            if (newHealth < currentHealth) { currentHealth = newHealth; OnHealthDown?.Invoke(newHealth); }
            
            OnHealthUpdate?.Invoke();
            if (isDefeated) {
                OnDefeat?.Invoke();
                Defeat();
            };
        }
    }

    [NonSerialized, ShowInDebugInspector] public bool canDamage = true;

    private bool isDefeated => currentHealth == 0;

    public delegate void HealthUpdateHandler(); public virtual event HealthUpdateHandler OnHealthUpdate;
    public delegate void MaxHealthUpdateHandler(float maxHealth); public virtual event MaxHealthUpdateHandler OnMaxHealthUpdate;
    public delegate void HealthDownHandler(float health); public virtual event HealthDownHandler OnHealthDown;
    public delegate void HealthUpHandler(float health); public virtual event HealthUpHandler OnHealthUp;
    public delegate void DefeatHandler(); public virtual event DefeatHandler OnDefeat;
    
    public virtual void Damage(float dmg, IDamager source, Transform owner, Collider2D collider=null) {
        CurrentHealth -= dmg;
    }

    virtual protected void Awake() {
        currentHealth = maxHealth;
    }

    public virtual void Defeat() {
        Destroy(gameObject);
    }
}
