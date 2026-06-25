using UnityEngine;

[CreateAssetMenu(fileName="NewConeAbility", menuName="ARPG/Abilities/ConeAbility", order = 0)]
public class ConeAbilityData : AbilityData
{
    [Header("Cone Shape")] public float range = 4f;
    [Range(0f, 180f)] public float coneAngle = 60f;

    public override void Execute(AbilityContext context)
    {
        Collider[] candidates = Physics.OverlapSphere(context.origin, range, targetLayers);
        float halfAngle = coneAngle * 0.5f;

        foreach (Collider hit in candidates)
        {
            Vector3 toTarget = hit.transform.position - context.origin;
            toTarget.y = 0f;
            toTarget.Normalize();

            float angle = Vector3.Angle(context.forward, toTarget);
            if (angle <= halfAngle)
            {
                ApplyDamage(hit, context);
            }
        }

        if (vfxPrefab != null)
        {
            GameObject vfx = Instantiate(vfxPrefab, context.origin, Quaternion.LookRotation(context.forward));
            vfx.transform.SetParent(context.caster);
            if (vfx.TryGetComponent<SlashEffect>(out SlashEffect slash))
            {
                slash.Play(coneAngle, range);
            }
        }

        if (sfx != null) AudioSource.PlayClipAtPoint(sfx, context.origin);
    }
}
