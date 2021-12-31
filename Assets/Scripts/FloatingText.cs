using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool Active;
    public readonly GameObject GameObject;
    public readonly Text Value;
    public Vector3 Motion;
    public float Duration;
    private float _lastShown;

    public FloatingText(GameObject gameObject)
    {
        GameObject = gameObject;
        Value = gameObject.GetComponent<Text>();
    }

    public void Show()
    {
        Active = true;
        _lastShown = Time.time;
        GameObject.SetActive(true);
    }

    public void Hide()
    {
        Active = false;
        GameObject.SetActive(false);
    }

    public void UpdateFloatingText()
    {
        if (!Active) return;

        if (Time.time - _lastShown > Duration)
            Hide();

        GameObject.transform.position += Motion * Time.deltaTime;
    }
}