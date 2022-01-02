using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> weaponDamagePoints;
    public List<float> weaponPushForces;
    public List<int> xpTable;

    // Game references
    public int currentCharacterSelection;
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;

    // Logic
    public int pesos;
    public int xp;

    public void GrantXp(int value)
    {
        var currentLevel = CalculateCurrentLevel();
        xp += value;

        if (currentLevel < CalculateCurrentLevel())
            player.OnLevelUp();
    }

    public int CalculateCurrentLevel()
    {
        var result = 0;
        var add = 0;
        while (xp >= add)
        {
            add += xpTable[result];
            result++;

            if (result == xpTable.Count) return result;
        }

        return result;
    }

    public int GetXpToLevel(int level)
    {
        var result = 0;
        var localXp = 0;

        while (result < level)
        {
            localXp += xpTable[result];
            result++;
        }

        return localXp;
    }

    public bool TryUpgradeWeapon()
    {
        if (weapon.weaponLevel >= weaponPrices.Count) return false; // max level
        if (weaponPrices[weapon.weaponLevel] > pesos) return false; // price

        pesos -= weaponPrices[weapon.weaponLevel];
        weapon.UpdateWeapon(weapon.weaponLevel + 1);
        return true;
    }

    public void ShowText(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(message, fontSize, color, position, motion, duration);
    }

    private void SaveState(Scene scene)
    {
        PlayerPrefs.SetInt("PreferredSkin", currentCharacterSelection);
        PlayerPrefs.SetInt("Pesos", pesos);
        PlayerPrefs.SetInt("Xp", xp);
        PlayerPrefs.SetInt("WeaponLevel", weapon.weaponLevel);
    }

    private void LoadState(Scene scene, LoadSceneMode mode)
    {
        // Change player skin
        if (PlayerPrefs.HasKey("PreferredSkin"))
        {
            currentCharacterSelection = PlayerPrefs.GetInt("PreferredSkin");
            player.SwapSprite(currentCharacterSelection);
        }

        if (PlayerPrefs.HasKey("Xp"))
        {
            xp = PlayerPrefs.GetInt("Xp");
            var level = CalculateCurrentLevel();
            if (level > 1)
                for (var i = 0; i < level; i++)
                    player.OnLevelUp();
        }

        if (PlayerPrefs.HasKey("Pesos")) pesos = PlayerPrefs.GetInt("Pesos");
        if (PlayerPrefs.HasKey("WeaponLevel")) weapon.UpdateWeapon(PlayerPrefs.GetInt("WeaponLevel"));
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneUnloaded += SaveState;
    }
}