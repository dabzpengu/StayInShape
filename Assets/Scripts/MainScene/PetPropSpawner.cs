using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetPropSpawner : CueListener
{
    protected override void Invoke(CueSO cue)
    {
        AnimationCueSO animationCue = (AnimationCueSO)cue;

        if (animationCue.prop != null)
        {
            SpawnProp(animationCue);
        }
    }

    void SpawnProp(AnimationCueSO animationCue)
    {
        GameObject propInstance = Instantiate(animationCue.prop);
        propInstance.transform.position = animationCue.propSpawnLocation;
        propInstance.transform.rotation = Quaternion.Euler(0, 180, 0);

        ParticleSystem propParticle = animationCue.prop.GetComponent<ParticleSystem>();

        if (animationCue.propAnimation != null)
        {
            Destroy(propInstance, animationCue.propAnimation.length);
        } else if (propParticle != null)
        {
            Destroy(propInstance, propParticle.main.duration);
        } else
        {
            Destroy(propInstance, 5);
        }
    }
}
