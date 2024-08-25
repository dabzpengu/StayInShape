using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightAnimator : MonoBehaviour
{
    [SerializeField]
    Renderer renderer;
    [SerializeField]
    float blinkSpeed = 0.4f;
    [SerializeField]
    int blinkCount = 3;
    [SerializeField]
    int outlineMaterialIndex = 1;

    public IEnumerator Play()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            renderer.materials[outlineMaterialIndex].color = Color.white;

            yield return new WaitForSeconds(blinkSpeed);

            renderer.materials[outlineMaterialIndex].color = Color.black;

            yield return new WaitForSeconds(blinkSpeed);
        }
    }

    public void ResetColor()
    {
        renderer.materials[outlineMaterialIndex].color = Color.black;
    }
}
