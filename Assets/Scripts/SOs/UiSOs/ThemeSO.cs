using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Theme", menuName = "SOs/UI Theme SO")]
public class ThemeSO : ScriptableObject
{
    [SerializeField]
    public string themeName;
    [SerializeField]
    public Color primaryColor;
    [SerializeField]
    public Color secondaryColor;
    [SerializeField]
    public Color accentColor;
}
