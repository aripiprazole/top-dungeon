using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Mover : Fighter
{
    protected BoxCollider2D BoxCollider;
    protected Vector3 MoveDelta;
    protected RaycastHit2D Hit;
    protected float XSpeed = 0.75f;
    protected float YSpeed = 0.75f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        BoxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        var moveDelta = new Vector3(input.x * XSpeed, input.y * YSpeed, 0);
        var transformLocal = transform;
        
        transformLocal.localScale = moveDelta.x switch
        {
            // Swap the sprite direction
            > 0 => Vector3.one,
            < 0 => new Vector3(-1, 1, 1),
            _ => transformLocal.localScale
        };
        
        // Setup push effect
        moveDelta += PushDirection;
        PushDirection = Vector3.Lerp(PushDirection, Vector3.zero, pushRecoverySpeed);

        // Check if can move by casting a box in the direction that the actor wants to move
        // If the box collider is null, it means that the way is clean and open to move in
        Hit = Physics2D.BoxCast(
            transformLocal.position,
            BoxCollider.size,
            0,
            new Vector2(moveDelta.x, 0),
            Math.Abs(moveDelta.x * Time.deltaTime),
            LayerMask.GetMask("Actor", "Blocking")
        );
        if (Hit.collider == null) transformLocal.Translate(moveDelta.x * Time.deltaTime, 0, 0);

        Hit = Physics2D.BoxCast(
            transformLocal.position,
            BoxCollider.size,
            0,
            new Vector2(0, moveDelta.y),
            Math.Abs(moveDelta.y * Time.deltaTime),
            LayerMask.GetMask("Actor", "Blocking")
        );
        if (Hit.collider == null)
            transformLocal.Translate(0, moveDelta.y * Time.deltaTime, 0);
    }
}