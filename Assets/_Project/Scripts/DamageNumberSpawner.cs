using UnityEngine;

[RequireComponent(typeof(Health))]
public class DamageNumberSpawner : MonoBehaviour
{
    [SerializeField] private DamageNumber damageNumberPrefab;
    [SerializeField] private Vector3 spawnOffset = new Vector3(0f, 1.5f, 0f);
    
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        health.OnDamaged += HandleDamaged;
    }

    private void OnDisable()
    {
        health.OnDamaged -= HandleDamaged;
    }

    private void HandleDamaged(DamageInfo info)
    {
        if (damageNumberPrefab == null) return;

        DamageNumber popup = Instantiate(damageNumberPrefab);
        popup.Show(info.amount, transform.position + spawnOffset);
    }
}
