using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    public Text levelText;
    public Text healthText;
    public Text pesosText;
    public Text upgradeCostText;
    public Text xpText;

    public Image currentCharacterSprite;
    public Image weaponSprite;
    public RectTransform xpBar;
    private int _currentCharacterSelection;

    public void OnArrowClick(bool direction)
    {
        if (direction) _currentCharacterSelection++;
        else _currentCharacterSelection--;

        if (_currentCharacterSelection >= GameManager.Instance.playerSprites.Count) _currentCharacterSelection = 0;
        if (_currentCharacterSelection <= 0) _currentCharacterSelection = GameManager.Instance.playerSprites.Count - 1;
        
        OnSelectionChanged();
    }
    private void OnSelectionChanged()
    {
        currentCharacterSprite.sprite = GameManager.Instance.playerSprites[_currentCharacterSelection];
    }

    public void OnUpgradeClick()
    {
        
    }

    public void UpdateMenu()
    {
        var gameManager = GameManager.Instance;
        
        // weapon
        weaponSprite.sprite = gameManager.weaponSprites[0];
        upgradeCostText.text = "todo";

        // information
        healthText.text = $"{gameManager.player.health} / {gameManager.player.maxHealth}";
        pesosText.text = gameManager.pesos.ToString();
        levelText.text = "todo";
        
        // exp
        xpText.text = "todo";
        xpBar.localScale = new Vector3(0.5f, 0, 0);
    }
}