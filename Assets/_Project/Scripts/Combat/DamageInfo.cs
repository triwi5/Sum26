using UnityEngine;

public struct DamageInfo
{
    public float amount;
    public Vector3 hitPoint;
    public float hitPauseDuration;

    public DamageInfo(float amount, Vector3 hitPoint, float hitPauseDuration = 0f)
    {
        this.amount = amount;
        this.hitPoint = hitPoint;
        this.hitPauseDuration = hitPauseDuration;
    }
}
