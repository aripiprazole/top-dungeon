using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int pesosAmount = 5;
    
    protected override void OnCollect()
    {
        if (collected) return;
        collected = true;

        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = emptyChest;

        GameManager.Instance.pesos += pesosAmount;
        
        Debug.LogFormat("Grant {0} pesos!", pesosAmount);
    }
}