using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cue animation to be played. Cue name must match animation state names found in the pet animator exactly.
/// </summary>
[CreateAssetMenu(menuName = "Events/AnimationCue")]
public class AnimationCueSO : CueSO
{
    /// <summary>
    /// Delay before the animation is played
    /// </summary>
    public float waitTime;
    public GameObject prop;
    public Vector3 propSpawnLocation;
    public AnimationClip propAnimation;
}
