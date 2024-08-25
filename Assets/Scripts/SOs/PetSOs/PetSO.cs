using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum PetSpecies
{
    Dog,
    Cat,
    Bird
}

[CreateAssetMenu(fileName = "PetSO", menuName = "PetSO")]
public class PetSO : ScriptableObject
{
    public string petName;
    public PetSpecies species;
    public GameObject petPrefab;
    public AnimationCueSO[] animationCueList;
    public AudioCueSO barkAudioCue;
    public PetColorPatternSO[] colors;
}
