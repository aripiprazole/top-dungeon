using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Weapon : Collidable
{
    // Damage
    public int damagePoint = 1;
    public float pushForce = 2.0f;
    
    // Upgrade
    public int weaponLevel;
    private SpriteRenderer _spriteRenderer;
    
    // Swing
    public float cooldown = 0.5f;
    private float _lastSwing;

    protected override void Start()
    {
        base.Start();
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        base.Update();

        if (!Input.GetKeyDown(KeyCode.Space)) return;
        if (Time.time - _lastSwing < cooldown) return;
        
        _lastSwing = Time.time;
        Swing();
    }

    private void Swing()
    {
        Debug.Log("Swing");
    }
}