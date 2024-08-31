using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuer : MonoBehaviour
{
    public CueSO currentCue;
    public CueEventChannel cueChannel;

    public void Cue()
    {
        if (currentCue != null)
        {
            cueChannel.RaiseEvent(currentCue);
        } else
        {
            Debug.Log("Cuer cue not set, no effect will be triggered");
        }
    }
}
