using UnityEngine;
using System;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;

    private float currentHealth;
    
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;
    public float HealthPercent => currentHealth / maxHealth;

    public event Action<DamageInfo> OnDamaged;
    public event Action OnDied;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(DamageInfo info)
    {
        if (currentHealth <= 0f) return;

        currentHealth -= info.amount;
        
        OnDamaged?.Invoke(info);

        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            OnDied?.Invoke();
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
