using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Theme Manager", menuName = "SOs/Theme Manager SO")]
public class ThemeManagerSO : ScriptableObject
{
    [SerializeField]
    public ThemeSO currentTheme;
    [SerializeField]
    public ThemeSO[] themes;
}
