using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;

    private Button _button1;
    private Button _button2;

    private List<Button> _menuButtons = new List<Button>();

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _document = GetComponent<UIDocument>();

        _button1 = _document.rootVisualElement.Q("MyGardenButton") as Button;
        _button1.RegisterCallback<ClickEvent>(OnMyGardenClick);

        _button2 = _document.rootVisualElement.Q("CollectResourceButton") as Button;
        _button2.RegisterCallback<ClickEvent>(OnCollectResourceClick);

        _menuButtons = _document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnDisable()
    {
        _button1.UnregisterCallback<ClickEvent>(OnMyGardenClick);
        _button2.UnregisterCallback<ClickEvent>(OnCollectResourceClick);

        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnMyGardenClick(ClickEvent evt)
    {
        Debug.Log("You pressed the My Garden Button");

        SceneManager.LoadScene("GardenScene");
    }

    private void OnCollectResourceClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Collect Resource Button");

        SceneManager.LoadScene("ResourceCollectionScene");
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        _audioSource.Play();
    }
}
