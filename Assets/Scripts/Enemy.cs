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

    private ContactFilter2D _filter;
    private BoxCollider2D _boxCollider;
    private readonly Collider2D[] _hits = new Collider2D[10];

    private bool _collidingWithPlayer;

    protected override void Start()
    {
        base.Start();

        _startingPosition = transform.position;
        _playerTransform = GameManager.Instance.player.transform;
        _boxCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Vector3.Distance(_playerTransform.position, _startingPosition) < chaseLength)
        {
            if (Vector3.Distance(_playerTransform.position, _startingPosition) < triggerLength)
            {
                _chasing = true;
                
                if (!_collidingWithPlayer)
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

        // Collision work
        _collidingWithPlayer = false;
        _boxCollider.OverlapCollider(_filter, _hits);
        for (var i = 0; i < _hits.Length; i++)
        {
            if (_hits[i] == null) continue;

            if (_hits[i].CompareTag("Fighter") && _hits[i].name == "Player")
                _collidingWithPlayer = true;

            _hits[i] = null;
        }
    }

    protected override void Die()
    {
        GameManager.Instance.xp += xpValue;
        GameManager.Instance.ShowText($"+{xpValue} exp", 25, Color.magenta, transform.position, Vector3.up * 40, 0.5f);

        Destroy(gameObject);
    }
}