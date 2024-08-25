using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetDirtiness : MonoBehaviour
{
    [SerializeField]
    PetStatusTrackerSO dirtinessSO;
    [SerializeField]
    float tickRate = 5.0f;
    [SerializeField]
    CueEventChannel materialAnimationCueEventChannel;
    [SerializeField]
    MaterialAnimationCue dirtyCue;
    [SerializeField]
    MaterialAnimationCue cleanCue;
    [SerializeField]
    HighlightAnimator highlightAnimator;

    private void OnEnable()
    {
        StartCoroutine(Tick());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator Tick()
    {
        if (dirtinessSO.CanLevelUp())
        {
            dirtinessSO.level = 1;
        }

        if (dirtinessSO.level == 1)
        {
            materialAnimationCueEventChannel.RaiseEvent(dirtyCue);
            StartCoroutine(highlightAnimator.Play());
        } else
        {
            materialAnimationCueEventChannel.RaiseEvent(cleanCue);
        }

        yield return new WaitForSeconds(tickRate);

        StartCoroutine(Tick());
    }

    public void ForceTick()
    {
        StopAllCoroutines();
        highlightAnimator.ResetColor();

        StartCoroutine(Tick());
    }
}
