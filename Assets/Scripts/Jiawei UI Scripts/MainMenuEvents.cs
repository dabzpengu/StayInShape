using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;

    private Button _button1;
    private Button _button2;
    private Button _button3;
    private Button _button4;

    private List<Button> _menuButtons = new List<Button>();

    private AudioSource _audioSource;

    public static event Action onGardenButtonClicked;
    [SerializeField] PlayerDataSO player;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _document = GetComponent<UIDocument>();

        _button1 = _document.rootVisualElement.Q("MyGardenButton") as Button;
        _button1.RegisterCallback<ClickEvent>(OnMyGardenClick);

        _button2 = _document.rootVisualElement.Q("CollectResourcesButton") as Button;
        _button2.RegisterCallback<ClickEvent>(OnCollectResourcesClick);

        _button3 = _document.rootVisualElement.Q("MyStepsButton") as Button;
        _button3.RegisterCallback<ClickEvent>(OnMyStepsClick);

        _button4 = _document.rootVisualElement.Q("SettingsButton") as Button;
        _button4.RegisterCallback<ClickEvent>(OnSettingsClick);

        _menuButtons = _document.rootVisualElement.Query<Button>().ToList();

        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnDisable()
    {
        _button1.UnregisterCallback<ClickEvent>(OnMyGardenClick);
        _button2.UnregisterCallback<ClickEvent>(OnCollectResourcesClick);
        _button3.UnregisterCallback<ClickEvent>(OnMyStepsClick);
        _button4.UnregisterCallback<ClickEvent>(OnSettingsClick);

        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnMyGardenClick(ClickEvent evt)
    {
        Debug.Log("You pressed the My Garden Button");
        onGardenButtonClicked?.Invoke();
        SceneManager.LoadScene("GardenSceneJia");
    }

    private void OnCollectResourcesClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Collect Resources Button");

        SceneManager.LoadScene("ResourceCollectionSceneJia");
    }

    private void OnMyStepsClick(ClickEvent evt)
    {
        Debug.Log("You pressed the My Steps Button");
        
        SceneManager.LoadScene("MyStepsScene");
    }

    private void OnSettingsClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Settings Button");
        // if (player.CanPlaySnap())
        // {
        //     SceneManager.LoadScene("SnapScene");
        // }
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        _audioSource.Play();
    }
}
