using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlAnimation : CueListener
{
    [SerializeField]
    protected Animator animator;

    /// <summary>
    /// Also sets rendering layer for the bowl to make sure it overlays when playing other animation
    /// </summary>
    override protected void Invoke(CueSO animation)
    {
        if (animation.name.Contains("Feed"))
        {
            foreach (Transform child in transform)
            {
                child.gameObject.layer = 0;
            }
        } else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.layer = 7;
            }
        }

        StartCoroutine(PlayAnimation(animation));
    }

    IEnumerator PlayAnimation(CueSO animation)
    {
        yield return new WaitForSeconds(((AnimationCueSO)animation).waitTime);

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        animator.Play(animation.cueName);
    }
}
