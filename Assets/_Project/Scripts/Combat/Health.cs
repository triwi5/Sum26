using UnityEngine;
using System;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;

    private float currentHealth;
    
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;
    public float HealthPercent => currentHealth / maxHealth;

    public event Action<float, Vector3> OnDamaged;
    public event Action OnDied;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount, Vector3 hitPoint)
    {
        if (currentHealth <= 0f) return;

        currentHealth -= amount;
        
        OnDamaged?.Invoke(amount, hitPoint);

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
