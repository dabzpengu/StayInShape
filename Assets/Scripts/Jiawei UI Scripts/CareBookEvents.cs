using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System;

public class CareBookEvents : MonoBehaviour
{
    private UIDocument document;

    private Button button1;
    private Button button2;
    private Button button3;
    private Button button4;
    private Button button5;
    private Button button6;
    private Button button7;

    private List<Button> menuButtons = new List<Button>();

    private AudioSource audioSource;

    private Sprite spriteToUse;
    public Sprite chilliSprite;
    public Sprite luffaSprite;
    public Sprite calamansiSprite;
    public Sprite sweetPotatoSprite;
    public Sprite eggplantSprite;
    public Sprite papayaSprite;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        document = GetComponent<UIDocument>();

        button1 = document.rootVisualElement.Q("ReturnButton") as Button;
        button1.RegisterCallback<ClickEvent>(OnReturnClick);

        button2 = document.rootVisualElement.Q("ChilliButton") as Button;
        button2.RegisterCallback<ClickEvent>(OnChilliClick);

        button3 = document.rootVisualElement.Q("LuffaButton") as Button;
        button3.RegisterCallback<ClickEvent>(OnLuffaClick);

        button4 = document.rootVisualElement.Q("CalamansiButton") as Button;
        button4.RegisterCallback<ClickEvent>(OnCalamansiClick);

        button5 = document.rootVisualElement.Q("EggplantButton") as Button;
        button5.RegisterCallback<ClickEvent>(OnEggplantClick);

        button6 = document.rootVisualElement.Q("SweetPotatoButton") as Button;
        button6.RegisterCallback<ClickEvent>(OnSweetPotatoClick);

        button7 = document.rootVisualElement.Q("PapayaButton") as Button;
        button7.RegisterCallback<ClickEvent>(OnPapayaClick);

        menuButtons = document.rootVisualElement.Query<Button>().ToList();

        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnDisable()
    {
        button1.UnregisterCallback<ClickEvent>(OnReturnClick);
        button2.UnregisterCallback<ClickEvent>(OnChilliClick);
        button3.UnregisterCallback<ClickEvent>(OnLuffaClick);
        button4.UnregisterCallback<ClickEvent>(OnCalamansiClick);
        button5.UnregisterCallback<ClickEvent>(OnEggplantClick);
        button6.UnregisterCallback<ClickEvent>(OnSweetPotatoClick);
        button7.UnregisterCallback<ClickEvent>(OnPapayaClick);

        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnReturnClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Return Button");

        SceneManager.LoadScene("GardenSceneJia");
    }

    private void OnChilliClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Chilli Button");
        
        ChangeSceneWithSprite("PlantInstructionsScene", chilliSprite);
    }

    private void OnLuffaClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Luffa Button");
        
        ChangeSceneWithSprite("PlantInstructionsScene", luffaSprite);
    }

    private void OnCalamansiClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Calamansi Button");
        
        ChangeSceneWithSprite("PlantInstructionsScene", calamansiSprite);
    }

    private void OnEggplantClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Eggplant Button");
        
        ChangeSceneWithSprite("PlantInstructionsScene", eggplantSprite);
    }

    private void OnSweetPotatoClick(ClickEvent evt)
    {
        Debug.Log("You pressed the SweetPotato Button");
        
        ChangeSceneWithSprite("PlantInstructionsScene", sweetPotatoSprite);
    }

    private void OnPapayaClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Papaya Button");

        ChangeSceneWithSprite("PlantInstructionsScene", papayaSprite);
    }

    private void ChangeSceneWithSprite(string sceneName, Sprite sprite)
    {
        spriteToUse = sprite;
        Debug.Log(spriteToUse);

        SceneManager.sceneLoaded += OnSceneLoaded;

        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UIDocument root = FindObjectOfType<UIDocument>();

        VisualElement plantDetails = root.rootVisualElement.Q("PlantDetails") as VisualElement;

        if (plantDetails != null && spriteToUse != null)
        {
            plantDetails.style.backgroundImage = new StyleBackground(spriteToUse);
            Debug.Log("Changed");
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;

        spriteToUse = null;
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        audioSource.Play();
    }
}
