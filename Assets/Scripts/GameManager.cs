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
    public List<int> xpTable;

    // Game references
    public Player player;

    // Logic
    public int pesos;
    public int xp;

    private void SaveState(Scene scene)
    {
        PlayerPrefs.SetInt("Preferred Skin", 0);
        PlayerPrefs.SetInt("Pesos", pesos);
        PlayerPrefs.SetInt("Xp", xp);
        PlayerPrefs.SetInt("Weapon Level", 0);
    }

    private void LoadState(Scene scene, LoadSceneMode mode)
    {
        // Change player skin
        if (PlayerPrefs.HasKey("Pesos")) pesos = PlayerPrefs.GetInt("Pesos");
        if (PlayerPrefs.HasKey("Xp")) xp = PlayerPrefs.GetInt("Xp");
        // Change weapon level
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