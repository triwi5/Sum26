using UnityEngine;

public abstract class AbilityData : ScriptableObject
{
    [Header("Identity")] 
    public string abilityName = "New Ability";
    [TextArea(2, 4)] 
    public string description;
    public Sprite icon;
    
    [Header("Stats")] 
    public float damage = 25f;
    public float cooldown = 0.5f;

    [Header("Visuals")] 
    public GameObject vfxPrefab;
    public AudioClip sfx;

    [Header("Targeting")]
    public LayerMask targetLayers;

    public abstract void Execute(AbilityContext context);

    protected float GetFinalDamage(AbilityContext context)
    {
        float final = damage;
        if (context.stats != null)
        {
            final *= context.stats.GetDamageMultiplier();
        }

        return final;
    }

    protected void ApplyDamage(Collider hit, AbilityContext context)
    {
        IDamageable damageable = hit.GetComponent<IDamageable>();
        if (damageable==null) damageable = hit.GetComponentInParent<IDamageable>();
        if (damageable == null) return;

        DamageInfo info = new DamageInfo(GetFinalDamage(context), hit.transform.position);
        damageable.TakeDamage(info);
    }

    protected void SpawnFeedback(Vector3 position)
    {
        if (vfxPrefab != null) Instantiate(vfxPrefab, position, Quaternion.identity);
        if (sfx != null) AudioSource.PlayClipAtPoint(sfx, position);
    }

}
