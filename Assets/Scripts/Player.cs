using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Player : Fighter
{
    private BoxCollider2D _boxCollider;
    private Vector3 _moveDelta;
    private RaycastHit2D _hit;

    // Start is called before the first frame update
    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        var transformLocal = transform;
        
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");

        _moveDelta = new Vector3(x, y, 0);

        transformLocal.localScale = _moveDelta.x switch
        {
            // Swap the sprite direction
            > 0 => Vector3.one,
            < 0 => new Vector3(-1, 1, 1),
            _ => transformLocal.localScale
        };

        // Check if can move by casting a box in the direction that the actor wants to move
        // If the box collider is null, it means that the way is clean and open to move in
        _hit = Physics2D.BoxCast(
            transformLocal.position,
            _boxCollider.size,
            0,
            new Vector2(_moveDelta.x, 0 ),
            Math.Abs(_moveDelta.x * Time.deltaTime),
            LayerMask.GetMask("Actor", "Blocking")
        );
        if (_hit.collider == null)
        {
            transformLocal.Translate(_moveDelta.x * Time.deltaTime, 0, 0);
        }
        
        _hit = Physics2D.BoxCast(
            transformLocal.position,
            _boxCollider.size,
            0,
            new Vector2(0, _moveDelta.y),
            Math.Abs(_moveDelta.y * Time.deltaTime),
            LayerMask.GetMask("Actor", "Blocking")
        );
        if (_hit.collider == null)
        {
            transformLocal.Translate(0, _moveDelta.y * Time.deltaTime, 0);
        }
    }
}