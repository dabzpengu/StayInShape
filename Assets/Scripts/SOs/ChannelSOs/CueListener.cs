using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CueListener : MonoBehaviour
{
    /// <summary>
    /// Channel to listen animation cues from
    /// </summary>
    [SerializeField]
    public CueEventChannel cueEventChannel;

    protected void OnEnable()
    {
        cueEventChannel.OnAnimationCueRequested += Invoke;
    }

    protected void OnDestroy()
    {
        cueEventChannel.OnAnimationCueRequested -= Invoke;
    }

    /// <summary>
    /// Plays animation state found in the prefab animator. Make sure you have set up exit times for different animations properly
    /// </summary>
    protected abstract void Invoke(CueSO cue);
}
