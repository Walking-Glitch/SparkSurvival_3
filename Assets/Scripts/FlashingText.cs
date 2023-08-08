using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlashingText : MonoBehaviour
{
    public TextMeshProUGUI flashingText;

    private float cycleDuration = 3.0f; // Total time for one complete fade in and fade out cycle

    void Start()
    {
        StartCoroutine(FadeInOut());
    }

    IEnumerator FadeInOut()
    {
        while (true) // Run the fading loop indefinitely
        {
            flashingText.CrossFadeAlpha(1, cycleDuration / 2, false); // Fade in
            yield return new WaitForSeconds(cycleDuration / 2); // Wait for half the cycle

            flashingText.CrossFadeAlpha(0, cycleDuration / 2, false); // Fade out
            yield return new WaitForSeconds(cycleDuration / 2); // Wait for half the cycle
        }
    }
}