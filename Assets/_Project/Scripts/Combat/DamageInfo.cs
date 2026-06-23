using UnityEngine;

public struct DamageInfo
{
    public float amount;
    public Vector3 hitPoint;

    public DamageInfo(float amount, Vector3 hitPoint)
    {
        this.amount = amount;
        this.hitPoint = hitPoint;
    }
}
