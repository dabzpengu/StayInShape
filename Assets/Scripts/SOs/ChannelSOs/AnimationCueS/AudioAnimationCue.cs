using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Animation with Audio Cue")]
public class AudioAnimationCue : AnimationCueSO
{
    public AudioCueSO audioCue;
    public float audioStartDelay;
}
