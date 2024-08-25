using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetSound : CueListener
{
    [SerializeField]
    AudioSource source;

    protected override void Invoke(CueSO cue)
    {
        AudioCueSO audioCue = (AudioCueSO) cue;

        int clipIndex = Random.Range(0, audioCue.clips.Length);

        AudioClip clip = audioCue.clips[clipIndex];

        source.clip = clip;
        source.Play();

        Debug.Log("Playing sound " + clip.name);
    }
}
