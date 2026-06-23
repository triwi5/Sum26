using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "ARPG/Ability Data", order = 0)]
public class AbilityData : ScriptableObject
{
    [Header("Identity")] 
    public string abilityName = "New Ability";
    [TextArea(2, 4)] 
    public string description;
    public Sprite icon;
    
    [Header("Stats")] 
    public float damage = 25f;
    public float radius = 3f;
    public float cooldown = 0.5f;

    [Header("Visuals")] 
    public GameObject vfxPrefab;
    public AudioClip sfx;

    [Header("Targeting")]
    public LayerMask targetLayers;

}
