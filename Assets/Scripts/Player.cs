using UnityEngine;

public class Player : Mover
{
    private SpriteRenderer _spriteRenderer;

    protected override void Start()
    {
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SwapSprite(int currentSelection)
    {
        _spriteRenderer.sprite = GameManager.Instance.playerSprites[currentSelection];
    }
    
    private void FixedUpdate()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x, y, 0));
    }
}