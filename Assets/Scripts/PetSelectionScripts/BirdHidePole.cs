using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdHidePole : MonoBehaviour
{
    SkinnedMeshRenderer render;

    void Start()
    {
        render = GetComponentInChildren<SkinnedMeshRenderer>();
        render.SetBlendShapeWeight(0, 100);
    }
}
