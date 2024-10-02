using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInExtract : MonoBehaviour
{
    public float fadeDuration = 2f;

    public Image background;
    private Color targetColor;
    private Color initialColor;
    private float elapsedTime = 0f;

    void Start()
    {
        initialColor = background.color;
        targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            background.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
        }
        else
        {
            background.color = targetColor;
            enabled = false;
        }
    }
}
