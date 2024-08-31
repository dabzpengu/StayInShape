using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAnimation : CueListener
{
    [SerializeField]
    protected Animator animator;
    IEnumerator coroutine;
    //[SerializeField]
    //private Renderer renderer;

    //private Material defaultMaterial;

    void Start()
    {

        //defaultMaterial = renderer.material;
    }

    override protected void Invoke(CueSO animation)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = PlayAnimation(animation);
        StartCoroutine(coroutine);

        // TODO: Make dog mouth animation use texture cue instead
        //if (animation.materialReplacement != null)
        //{
        //    StartCoroutine(ReplaceMaterial(animation));
        //}
    }

    IEnumerator PlayAnimation(CueSO animation)
    {
        //yield return new WaitForSeconds(((AnimationCueSO)animation).waitTime);
        yield return new WaitForSeconds(0);

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        Debug.Log("Playing animation " + animation.cueName + " on animator " + animator.name);

        animator.Play(animation.cueName);
    }

    //IEnumerator ReplaceMaterial(AnimationCueSO animation)
    //{
    //    yield return new WaitForSeconds(animation.materialReplaceStartEnd.x);

    //    renderer.material = animation.materialReplacement;

    //    yield return new WaitForSeconds(animation.materialReplaceStartEnd.y);

    //    renderer.material = defaultMaterial;
    //}
}
