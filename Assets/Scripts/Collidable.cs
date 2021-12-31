using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Collidable : MonoBehaviour
{
    public ContactFilter2D filter;
    public BoxCollider2D boxCollider;
    private readonly Collider2D[] _hits = new Collider2D[10];

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        // Collision work
        boxCollider.OverlapCollider(filter, _hits);
        for (var i = 0; i < _hits.Length; i++)
        {
            if (_hits[i] == null) continue;
            
            OnCollide(_hits[i]);

            _hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D target)
    {
        Debug.LogFormat("Collidable.OnCollide({0})", target.name);
    }
}