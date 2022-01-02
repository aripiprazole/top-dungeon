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
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;

    // Logic
    public int pesos;
    public int xp;

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
        Debug.Log($"Saving weapon {weapon.weaponLevel}");
        PlayerPrefs.SetInt("PreferredSkin", 0);
        PlayerPrefs.SetInt("Pesos", pesos);
        PlayerPrefs.SetInt("Xp", xp);
        PlayerPrefs.SetInt("WeaponLevel", weapon.weaponLevel);
    }

    private void LoadState(Scene scene, LoadSceneMode mode)
    {
        // Change player skin
        if (PlayerPrefs.HasKey("Pesos")) pesos = PlayerPrefs.GetInt("Pesos");
        if (PlayerPrefs.HasKey("Xp")) xp = PlayerPrefs.GetInt("Xp");
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