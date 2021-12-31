using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Weapon : Collidable
{
    // Damage
    public int damagePoint = 1;
    public float pushForce = 5.0f;

    // Upgrade
    public int weaponLevel;
    private SpriteRenderer _spriteRenderer;

    // Swing
    public float cooldown = 0.5f;
    private float _lastSwing;
    private Animator _anim;
    
    private static readonly int Anim = Animator.StringToHash("Swing");

    protected override void Start()
    {
        base.Start();

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (!Input.GetKeyDown(KeyCode.Space)) return;
        if (Time.time - _lastSwing < cooldown) return;

        _lastSwing = Time.time;
        Swing();
    }

    protected override void OnCollide(Collider2D target)
    {
        if (!target.CompareTag("Fighter")) return;
        if (target.name == "Player") return;

        var damage = new Damage(transform.position, damagePoint, pushForce);
        target.SendMessage("ReceiveDamage", damage);
    }

    private void Swing()
    {
        _anim.SetTrigger(Anim);
    }
}