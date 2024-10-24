using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System;

public class ResourceCollectionEvents : MonoBehaviour
{
    private UIDocument document;

    private Button button1;
    private Button button2;
    private Button button3;
    private Button button4;

    private List<Button> menuButtons = new List<Button>();

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        document = GetComponent<UIDocument>();

        button1 = document.rootVisualElement.Q("HomeButton") as Button;
        button1.RegisterCallback<ClickEvent>(OnHomeClick);

        button2 = document.rootVisualElement.Q("SnapPlay") as Button;
        button2.RegisterCallback<ClickEvent>(OnSnapPlayClick);

        button3 = document.rootVisualElement.Q("MatchingPlay") as Button;
        button3.RegisterCallback<ClickEvent>(OnMatchingPlayClick);

        button4 = document.rootVisualElement.Q("ChickenPlay") as Button;
        button4.RegisterCallback<ClickEvent>(OnChickenPlayClick);

        menuButtons = document.rootVisualElement.Query<Button>().ToList();

        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnDisable()
    {
        button1.UnregisterCallback<ClickEvent>(OnHomeClick);
        button2.UnregisterCallback<ClickEvent>(OnSnapPlayClick);
        button3.UnregisterCallback<ClickEvent>(OnMatchingPlayClick);
        button4.UnregisterCallback<ClickEvent>(OnChickenPlayClick);

        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnHomeClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Home Button");

        SceneManager.LoadScene("HomeScene");
    }

    private void OnSnapPlayClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Snap Play Button");

        SceneManager.LoadScene("SnapScene");
    }

    private void OnMatchingPlayClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Matching Play Button");

        SceneManager.LoadScene("MatchingCardScene");
    }

    private void OnChickenPlayClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Chicken Play Button");

        SceneManager.LoadScene("ChickenInvaderScene");
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        audioSource.Play();
    }
}
