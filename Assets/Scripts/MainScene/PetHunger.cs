using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetHunger : MonoBehaviour
{
    [SerializeField]
    PetStatusTrackerSO hungerSO;
    [SerializeField]
    float tickRate = 5.0f;
    [SerializeField]
    HighlightAnimator animator;

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
        if (hungerSO.CanLevelUp())
        {
            hungerSO.level = 1;
        }

        if (hungerSO.level == 1)
        {
            StartCoroutine(animator.Play());
        }

        yield return new WaitForSeconds(tickRate);

        StartCoroutine(Tick());
    }

    public void ForceTick()
    {
        StopAllCoroutines();
        animator.ResetColor();
        
        StartCoroutine(Tick());
    }
}
