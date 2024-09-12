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
    private Button _button3;
    private Button _button4;

    private List<Button> _menuButtons = new List<Button>();

    private AudioSource _audioSource;

    [SerializeField] PlayerDataSO player;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _document = GetComponent<UIDocument>();

        _button1 = _document.rootVisualElement.Q("MyGardenButton") as Button;
        _button1.RegisterCallback<ClickEvent>(OnMyGardenClick);

        _button2 = _document.rootVisualElement.Q("CollectResourceButton") as Button;
        _button2.RegisterCallback<ClickEvent>(OnCollectResourceClick);

        _button3 = _document.rootVisualElement.Q("MatchCardButton") as Button;
        _button3.RegisterCallback<ClickEvent>(OnMatchCardClick);

        _button4 = _document.rootVisualElement.Q("SnapGameButton") as Button;
        _button4.RegisterCallback<ClickEvent>(OnSnapGameClick);

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
        _button3.UnregisterCallback<ClickEvent>(OnMatchCardClick);
        _button4.UnregisterCallback<ClickEvent>(OnSnapGameClick);

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

    private void OnMatchCardClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Match Card Game Button");
        if (player.CanPlayMatchingCard())
        {
           SceneManager.LoadScene("MatchingCardScene");
        }
    }

    private void OnSnapGameClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Snap Game Button");
        if (player.CanPlaySnap())
        {
            SceneManager.LoadScene("SnapScene");
        }
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        _audioSource.Play();
    }
}
