using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAnimationCueListener : MonoBehaviour
{
    /// <summary>
    /// Channel to listen material animation cues from
    /// </summary>
    [SerializeField]
    protected CueEventChannel materialAnimationCueEventChannel;
    [SerializeField]
    protected Material material;
    [SerializeField]
    protected Renderer renderer;

    protected void OnEnable()
    {
        materialAnimationCueEventChannel.OnAnimationCueRequested += SetFloat;
    }

    protected void OnDestroy()
    {
        materialAnimationCueEventChannel.OnAnimationCueRequested -= SetFloat;
    }

    void SetFloat(CueSO cue)
    {
        material.SetFloat(cue.cueName, ((MaterialAnimationCue)cue).value);
        renderer.material = material;
    }
}
