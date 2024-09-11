using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GardenUIEvents : MonoBehaviour
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

        _button1 = _document.rootVisualElement.Q("BackButton") as Button;
        _button1.RegisterCallback<ClickEvent>(OnBackButtonClick);

        _button2 = _document.rootVisualElement.Q("SSButton") as Button;
        _button2.RegisterCallback<ClickEvent>(OnSSButtonClick);

        _menuButtons = _document.rootVisualElement.Query<Button>().ToList();

        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnDisable()
    {
        _button1.UnregisterCallback<ClickEvent>(OnBackButtonClick);
        _button2.UnregisterCallback<ClickEvent>(OnSSButtonClick);

        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnBackButtonClick(ClickEvent evt)
    {
        Debug.Log("You pressed Back Button");

        SceneManager.LoadScene("HomeScene");
    }

    private void OnSSButtonClick(ClickEvent evt)
    {
        Debug.Log("You pressed SS Button");

        // Function to take a screenshot
        StartCoroutine(TakeScreenshot());
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        _audioSource.Play();
    }

    private IEnumerator TakeScreenshot()
    {
        // Wait till the end of the frame to capture the screenshot
        yield return new WaitForEndOfFrame();

        // Create a unique filename
        string screenshotName = $"screenshot_{System.DateTime.Now:yyyyMMdd_HHmmss}.png";

        // Save the screenshot to the device's persistent data path
        string path = $"{Application.persistentDataPath}/{screenshotName}";

        // Capture the screenshot
        ScreenCapture.CaptureScreenshot(path);

        Debug.Log($"Screenshot saved to: {path}");
    }
}
