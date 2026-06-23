using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private float damageMultiplier = 1f;

    public float GetDamageMultiplier()
    {
        return damageMultiplier;
    }
}
