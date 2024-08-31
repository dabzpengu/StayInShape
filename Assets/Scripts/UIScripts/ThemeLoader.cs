using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThemeLoader : MonoBehaviour
{
    [SerializeField] ThemeManagerSO themeManager;
    [SerializeField] Image[] primary;
    [SerializeField] Image[] secondary;
    [SerializeField] Image[] accent;

    private void Start()
    {
        foreach (Image image in primary)
        {
            image.color = themeManager.currentTheme.primaryColor;
            foreach (TextMeshProUGUI text in image.GetComponentsInChildren<TextMeshProUGUI>())
            {
                text.color = image.color.r * 0.299 * 255 + image.color.g * 0.587 * 255 + image.color.b * 0.114 * 255 > 150 ? Color.black : Color.white;
            }
        }
        foreach (Image image in secondary)
        {
            image.color = themeManager.currentTheme.secondaryColor;
            foreach (TextMeshProUGUI text in image.GetComponentsInChildren<TextMeshProUGUI>())
            {
                text.color = image.color.r * 0.299 * 255 + image.color.g * 0.587 * 255 + image.color.b * 0.114 * 255 > 150 ? Color.black : Color.white;
            }
        }
        foreach (Image image in accent)
        {
            image.color = themeManager.currentTheme.accentColor;
            foreach (TextMeshProUGUI text in image.GetComponentsInChildren<TextMeshProUGUI>())
            {
                text.color = image.color.r * 0.299 * 255 + image.color.g * 0.587 * 255 + image.color.b * 0.114 * 255 > 150 ? Color.black : Color.white;
            }
        }
    }
}
