using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewScene", menuName = "SOs/SceneSO")]
public class SceneSO : ScriptableObject
{
    [Header("Information")]
    public string sceneName;
    public string shortDescription;

    [Header("Sounds")]
    public AudioClip music;
    [Range(0.0f, 1.0f)]
    public float musicVolume;


}
