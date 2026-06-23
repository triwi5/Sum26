using UnityEngine;

public class AbilityRunner : MonoBehaviour
{
    [SerializeField] private AbilityData ability;
    [SerializeField] private CharacterStats casterStats;

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

        AbilityContext context = new AbilityContext(transform, casterStats);
        ability.Execute(context);
        
        cooldownTimer = ability.cooldown;
        return true;
    }
    
}
