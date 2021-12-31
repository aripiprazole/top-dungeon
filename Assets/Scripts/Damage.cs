using UnityEngine;

public struct Damage
{
    public readonly Vector3 Origin;
    public readonly int Amount;
    public readonly float PushForce;

    public Damage(Vector3 origin, int amount, float pushForce)
    {
        Origin = origin;
        Amount = amount;
        PushForce = pushForce;
    }
}