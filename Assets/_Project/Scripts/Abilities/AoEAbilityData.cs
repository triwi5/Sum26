using UnityEngine;

[CreateAssetMenu(fileName="NewAoEAbility", menuName="ARPG/Abilities/AoEAbility", order = 0)]
public class AoEAbilityData : AbilityData
{
    [Header("AoE Shape")] 
    public float radius = 3f;

    public override void Execute(AbilityContext context)
    {
        Collider[] hits = Physics.OverlapSphere(context.origin, radius, targetLayers);
        foreach (Collider hit in hits)
        {
            ApplyDamage(hit, context);
        }
        SpawnFeedback(context.origin);
    }
}
