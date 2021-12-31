using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int health = 10;
    public int maxHealth = 10;
    public float pushRecoverySpeed = 0.2f;

    // Immunity
    protected float ImmuneTime = 1.0f;
    protected float LastImmune;

    // Push
    protected Vector3 PushDirection;

    protected virtual void ReceiveDamage(Damage damage)
    {
        var position = transform.position;

        if (Time.time - LastImmune < ImmuneTime) return;

        health -= damage.Amount;
        LastImmune = Time.time;

        PushDirection = (position - damage.Origin).normalized * damage.PushForce;

        GameManager.Instance.ShowText($"-{damage.Amount}", 25, Color.red, position, Vector3.zero, 0.5f);

        if (health > 0) return;

        health = 0;
        Die();
    }

    protected virtual void Die()
    {
    }
}