using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoweringBrush : MonoBehaviour
{
    [SerializeField]
    GameObject trail;
    [SerializeField]
    CueEventChannel animationCueChannel;
    [SerializeField]
    AnimationCueSO bathAnimation;
    [SerializeField]
    AnimationCueSO dryAnimation;
    [SerializeField]
    GameObject setupEmpty;

    Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    /// <summary>
    /// Parents the brush to the trail
    /// </summary>
    /// <returns>
    /// If trail was successfully located
    /// </returns>
    public void StartShowering()
    {
        PlayDryAnimation();
    }

    /// <summary>
    /// Reset brush parent and position
    /// </summary>
    /// <returns>
    /// If setup empty successfully located
    /// </returns>
    public void ResetPosition()
    {
        if (setupEmpty != null)
        {
            transform.parent = null;
            transform.parent = setupEmpty.transform;
            transform.position = initialPosition;
        } else
        {
            transform.parent = null;
            transform.position = initialPosition;
        }
    }

    void PlayBathAnimation()
    {
        animationCueChannel.RaiseEvent(bathAnimation);
    }

    void PlayDryAnimation()
    {
        animationCueChannel.RaiseEvent(dryAnimation);
    }
}
