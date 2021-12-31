using System;
using UnityEngine;

public class Enemy : Mover
{
    public int xpValue = 1;

    // Logic
    public float triggerLength = 1;
    public float chaseLength = 5;
    private bool _chasing;
    private Transform _playerTransform;
    private Vector3 _startingPosition;
    private EnemyHitbox _hitbox;

    protected override void Start()
    {
        base.Start();

        _startingPosition = transform.position;
        _playerTransform = GameManager.Instance.player.transform;
        _hitbox = transform.GetChild(0).GetComponent<EnemyHitbox>();
    }

    private void Update()
    {
        if (Vector3.Distance(_playerTransform.position, _startingPosition) < chaseLength)
        {
            if (Vector3.Distance(_playerTransform.position, _startingPosition) < triggerLength)
            {
                _chasing = true;
                
                if (!_hitbox.collidingWithPlayer)
                {
                    UpdateMotor((_playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(_startingPosition - transform.position);
            }
        }
        else
        {
            UpdateMotor(_startingPosition - transform.position);
        }
    }

    protected override void Die()
    {
        GameManager.Instance.xp += xpValue;
        GameManager.Instance.ShowText($"+{xpValue} exp", 25, Color.magenta, transform.position, Vector3.up * 40, 0.5f);

        Destroy(gameObject);
    }
}