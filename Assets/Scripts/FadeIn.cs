using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    public TextMeshProUGUI fadeInText;

    public float duration = 3.0f; // Total time for fade in

    void Start()
    {
        fadeInText.CrossFadeAlpha(0, 0, false); // Start with alpha 0
        StartCoroutine(FadeInOverTime());
    }

    IEnumerator FadeInOverTime()
    {
        fadeInText.CrossFadeAlpha(1, duration, false); // Fade in
        yield return new WaitForSeconds(duration);
    }
}
