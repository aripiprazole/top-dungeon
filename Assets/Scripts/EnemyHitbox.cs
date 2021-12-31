using UnityEngine;

public class EnemyHitbox : Collidable
{
    public int damagePoint = 1;
    public float pushForce = 5.0f;

    public bool collidingWithPlayer;

    protected override void Update()
    {
        collidingWithPlayer = false;

        base.Update();
    }

    protected override void OnCollide(Collider2D target)
    {
        if (!target.CompareTag("Fighter") || target.name != "Player") return;

        collidingWithPlayer = true;
        var damage = new Damage(transform.position, damagePoint, pushForce);
        target.SendMessage("ReceiveDamage", damage);
    }
}