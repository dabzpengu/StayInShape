using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    [SerializeField]
    SceneDataSO sceneDatabase;
    [SerializeField]
    AudioClip clapperSound;
    [SerializeField]
    bool playOnEntry;
    AudioSource audioSource;
    Animator anim;

    private void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
        audioSource.clip = clapperSound;
        if (playOnEntry) audioSource.Play();
        anim = this.GetComponent<Animator>();
    }
    public void exitScene(string nextSceneName)
    {
        anim.SetTrigger("ExitScene");
        StartCoroutine(delayedExit(nextSceneName));
    }

    IEnumerator delayedExit(string sceneName)
    {
        audioSource.clip = clapperSound;
        audioSource.Play();
        yield return new WaitForSeconds(3f);
        sceneDatabase.LoadScene(sceneName);
    }
}
