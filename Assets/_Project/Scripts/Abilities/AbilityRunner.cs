using UnityEngine;

public class AbilityRunner : MonoBehaviour
{
    [SerializeField] private AbilityData ability;

    [Header("References")] [SerializeField]
    private CharacterStats casterStats;

    public AbilityData Ability => ability;
    public float CooldownRemaining => Mathf.Max(0f, cooldownTimer);
    public bool IsReady => cooldownTimer <= 0f;

    private float cooldownTimer;

    private void Awake()
    {
        if (casterStats == null)
        {
            casterStats = GetComponentInParent<CharacterStats>();
        }
    }

    private void Update()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public bool TryCast()
    {
        if (ability == null || !IsReady) return false;

        ExecuteAbility();
        cooldownTimer = ability.cooldown;
        return true;
    }

    private void ExecuteAbility()
    {
        float finalDamage = ability.damage;
        if (casterStats != null)
        {
            finalDamage *= casterStats.GetDamageMultiplier();
        }

        Collider[] hits = Physics.OverlapSphere(transform.position, ability.radius, ability.targetLayers);

        foreach (Collider hit in hits)
        {
            IDamageable damageable = hit.GetComponent<IDamageable>();
            if (damageable == null)
            {
                damageable = hit.GetComponentInParent<IDamageable>();
            }

            if (damageable != null)
            {
                DamageInfo info = new DamageInfo(finalDamage, hit.transform.position);
                damageable.TakeDamage(info);
            }
        }

        if (ability.vfxPrefab != null)
        {
            Instantiate(ability.vfxPrefab, transform.position, Quaternion.identity);
        }

        if (ability.sfx != null)
        {
            AudioSource.PlayClipAtPoint(ability.sfx, transform.position);
        }
        
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ability.radius);
    }
}
