using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    public string[] dungeonNames;

    protected override void OnCollide(Collider2D target)
    {
        if (target.name != "Player") return;

        var dungeonScene = dungeonNames[Random.Range(0, dungeonNames.Length)];
        SceneManager.LoadScene(dungeonScene);
        DontDestroyOnLoad(target);

        target.transform.position = Vector3.zero;
    }
}