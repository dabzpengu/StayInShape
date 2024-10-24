using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System;

public class PlantInstructionsEvent : MonoBehaviour
{
    private UIDocument document;

    private Button button1;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        document = GetComponent<UIDocument>();

        button1 = document.rootVisualElement.Q("BackButton") as Button;
        button1.RegisterCallback<ClickEvent>(OnBackClick);
    }

    private void OnDisable()
    {
        button1.UnregisterCallback<ClickEvent>(OnBackClick);
    }

    private void OnBackClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Back Button");

        SceneManager.LoadScene("CareBookScene");
        audioSource.Play();
    }
}
