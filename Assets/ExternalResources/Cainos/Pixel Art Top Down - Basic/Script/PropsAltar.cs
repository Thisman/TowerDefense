using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsAltar : MonoBehaviour
{
    public List<SpriteRenderer> runes;
    public float lerpSpeed;

    private Color curColor;
    private Color targetColor;

    public void Awake()
    {
        targetColor = runes[0].color;
    }

    public void Update()
    {
        curColor = Color.Lerp(curColor, targetColor, lerpSpeed * Time.deltaTime);

        foreach (var r in runes)
        {
            r.color = curColor;
        }

        if (curColor == targetColor)
        {
            targetColor.a = targetColor.a == 0.0f ? 1.0f : 0.0f;
        }
    }
}
