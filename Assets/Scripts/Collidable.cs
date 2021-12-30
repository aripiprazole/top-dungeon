using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Collidable : MonoBehaviour
{
    public ContactFilter2D filter;
    public BoxCollider2D boxCollider;
    private readonly Collider2D[] hits = new Collider2D[10];

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        // Collision work
        boxCollider.OverlapCollider(filter, hits);
        for (var i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null) continue;
            
            OnCollide(hits[i]);

            hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D target)
    {
        Debug.Log(target.name);
    }
}