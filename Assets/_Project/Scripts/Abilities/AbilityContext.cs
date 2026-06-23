using UnityEngine;

public struct AbilityContext
{
    public Transform caster;                // who cast this
    public CharacterStats stats;            // caster stats
    public Vector3 origin;                  // cast origin
    public Vector3 forward;                 // cast direction

    public AbilityContext(Transform caster, CharacterStats stats)
    {
        this.caster = caster;
        this.stats = stats;
        this.origin = caster.position;
        this.forward = caster.forward;
    }
}
