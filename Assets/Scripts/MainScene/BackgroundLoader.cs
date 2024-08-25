using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundLoader : MonoBehaviour
{
    [SerializeField] Image backgroundRenderer;
    [SerializeField] BackgroundImageDataSO backgroundData;

    void Start()
    {
        backgroundRenderer.sprite = backgroundData.GetCurrentBackground();
    }
}
