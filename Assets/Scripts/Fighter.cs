using UnityEngine;

public class Fighter : MonoBehaviour
{
    private const float ImmuneTime = 1.0f;
    
    public int health = 10;
    public int maxHealth = 10;
    public float pushRecoverySpeed = 0.2f;

    // Immunity
    private float _lastImmune;

    // Push
    protected Vector3 PushDirection;

    protected virtual void ReceiveDamage(Damage damage)
    {
        var position = transform.position;

        if (Time.time - _lastImmune < ImmuneTime) return;

        health -= damage.Amount;
        _lastImmune = Time.time;

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