using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Cue Event Channel")]
public class CueEventChannel : ScriptableObject
{
    public UnityAction<CueSO> OnAnimationCueRequested;

    public void RaiseEvent(CueSO cue)
    {
        if (OnAnimationCueRequested != null)
        {
            OnAnimationCueRequested.Invoke(cue);
        }
        else
        {
            Debug.Log("A " + cue.GetType().ToString() + " cue was requested, but noboday picked up.");
        }
    }
}