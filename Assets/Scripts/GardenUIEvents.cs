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
    private Button _button3;
    private Button _button4;
    private Button _button5;

    private List<Button> _menuButtons = new List<Button>();

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _document = GetComponent<UIDocument>();

        _button1 = _document.rootVisualElement.Q("BackButton") as Button;
        _button1.RegisterCallback<ClickEvent>(OnBackButtonClick);

        _button2 = _document.rootVisualElement.Q("SettingsButton") as Button;
        _button2.RegisterCallback<ClickEvent>(OnSettingsClick);

        _button3 = _document.rootVisualElement.Q("TakePhotoButton") as Button;
        _button3.RegisterCallback<ClickEvent>(OnTakePhotoClick);
        
        _button4 = _document.rootVisualElement.Q("CareBookButton") as Button;
        _button4.RegisterCallback<ClickEvent>(OnCareBookClick);

        _button5 = _document.rootVisualElement.Q("ShopButton") as Button;
        _button5.RegisterCallback<ClickEvent>(OnShopClick);

        _menuButtons = _document.rootVisualElement.Query<Button>().ToList();

        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnDisable()
    {
        _button1.UnregisterCallback<ClickEvent>(OnBackButtonClick);
        _button2.UnregisterCallback<ClickEvent>(OnSettingsClick);
        _button3.UnregisterCallback<ClickEvent>(OnTakePhotoClick);
        _button4.UnregisterCallback<ClickEvent>(OnCareBookClick);
        _button5.UnregisterCallback<ClickEvent>(OnShopClick);

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

    private void OnSettingsClick(ClickEvent evt)
    {
        Debug.Log("You pressed Settings Button");
    }

    private void OnTakePhotoClick(ClickEvent evt)
    {
        Debug.Log("You pressed Take Photo Button");

        StartCoroutine(TakeScreenshot());
    }

    private IEnumerator TakeScreenshot()
    {
        // Wait until the end of the frame to capture
        yield return new WaitForEndOfFrame();

        // Define the file path and name
        string fileName = "Screenshot_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        string defaultLocation = Application.persistentDataPath + "/" + fileName;
        string desiredFolder = "/storage/emulated/0/DCIM/Screenshots/";
        string desiredSSLocation = desiredFolder + fileName;

        if (!System.IO.Directory.Exists(desiredFolder))
        {
            System.IO.Directory.CreateDirectory(desiredFolder);
        }

        // Capture the screenshot
        ScreenCapture.CaptureScreenshot(fileName);

        // Wait for the file to be saved
        yield return new WaitForSeconds(1);

        // Move the file to the gallery
        System.IO.File.Move(defaultLocation, desiredSSLocation);

        // Refresh the Android gallery to show the new screenshot
        AndroidJavaClass classPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass classUri = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject objIntent = new AndroidJavaObject("android.content.Intent", new object[2] { "android.intent.action.MEDIA_MOUNTED", classUri.CallStatic<AndroidJavaObject>("parse", "file://" + desiredSSLocation) });
        objActivity.Call("sendBroadcast", objIntent);
        }

    private void OnCareBookClick(ClickEvent evt)
    {
        Debug.Log("You pressed Care Book Button");
    }

    private void OnShopClick(ClickEvent evt)
    {
        Debug.Log("You pressed Shop Button");

        SceneManager.LoadScene("ShopScene");
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        _audioSource.Play();
    }
}
