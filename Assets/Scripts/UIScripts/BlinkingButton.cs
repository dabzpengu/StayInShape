using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingButton : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    float waitTime = 4.0f;
    [SerializeField]
    float fadeStep = 0.001f;
    [SerializeField]
    float showTime = 2.0f;
    [SerializeField]
    Image hintBackground;
    [SerializeField]
    float maxBackgroundAlpha = 0.5f;

    private void Start()
    {
        StartCoroutine(Disappear());
    }

    IEnumerator Disappear()
    {
        text.alpha = 0;
        ChangeImageAlpha(hintBackground, text.alpha, maxBackgroundAlpha);

        yield return new WaitForSeconds(waitTime);

        while(text.alpha < 1.0f)
        {
            text.alpha += fadeStep;
            ChangeImageAlpha(hintBackground, text.alpha, maxBackgroundAlpha);
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(showTime);

        while (text.alpha > 0.0f)
        {
            text.alpha -= fadeStep;
            ChangeImageAlpha(hintBackground, text.alpha, maxBackgroundAlpha);
            yield return new WaitForFixedUpdate();
        }

        StartCoroutine(Disappear());
    }

    public void Reset()
    {
        StopAllCoroutines();
        StartCoroutine(Disappear());
    }

    private void ChangeImageAlpha(Image image, float a, float maxAlpha)
    {
        Color color = image.color;
        color = new Color(color.r, color.g, color.b, a * maxAlpha);
        image.color = color;
    }
}
