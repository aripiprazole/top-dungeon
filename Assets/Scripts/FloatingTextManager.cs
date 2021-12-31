using System;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private readonly List<FloatingText> _floatingTexts = new();

    private void Update()
    {
        foreach (var floatingText in _floatingTexts)
        {
            floatingText.UpdateFloatingText();
        }
    }

    public void Show(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        Debug.Assert(Camera.main != null, "Camera.main != null");

        var floatingText = GetFloatingText();

        floatingText.Value.text = message;
        floatingText.Value.fontSize = fontSize;
        floatingText.Value.color = color;
        floatingText.GameObject.transform.position = Camera.main.WorldToScreenPoint(position);
        floatingText.Motion = motion;
        floatingText.Duration = duration;

        floatingText.Show();
    }

    private FloatingText GetFloatingText()
    {
        var text = _floatingTexts.Find(text => !text.Active);

        if (text != null) return text;

        text = new FloatingText(Instantiate(textPrefab, textContainer.transform, true));
        _floatingTexts.Add(text);

        return text;
    }
}