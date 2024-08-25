using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI titleText;
    [SerializeField]
    Button buttonYes;
    [SerializeField]
    Button buttonNo;
    [SerializeField]
    AudioClip closeSound;

    public void ShowDialogue(string title, Action yesAction, Action noAction)
    {
        gameObject.SetActive(true);
        titleText.text = title;
        //buttonYes.onClick.RemoveAllListeners();
        buttonYes.onClick.AddListener(() =>
        {
            Hide();
            yesAction();
        });
        //buttonNo.onClick.RemoveAllListeners();
        buttonNo.onClick.AddListener(() =>
        {
            Hide();
            noAction();
        });
    }

    void Hide()
    {
        GameObject audioSourceObject = new GameObject();
        audioSourceObject.transform.parent = null;
        AudioSource source = audioSourceObject.AddComponent<AudioSource>();
        source.clip = closeSound;
        source.Play();
        Destroy(audioSourceObject, closeSound.length);

        gameObject.SetActive(false);
    }
}
