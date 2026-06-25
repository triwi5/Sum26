using UnityEngine;

[RequireComponent(typeof(Health))]
public class HitPauseTrigger : MonoBehaviour
{
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
        if (info.hitPauseDuration > 0f)
        {
            HitPause.Request(info.hitPauseDuration);
        }
    }
}