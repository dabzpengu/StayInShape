using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMinigame", menuName = "SOs/MinigameSO")]

public class MinigameSO : ScriptableObject
{
    public string gameName;
    public string gameSceneName;
    public int reward;
    public string description;
}
