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

    private VisualElement resourceTracker;
    public Sprite originalSprite;
    public Sprite newSprite;
    public Sprite waterSprite;
    public Sprite fertilizerSprite;
    public Sprite chilliSprite;
    public Sprite loofaSprite;
    private bool isOriginal = true;

    private VisualElement popUp;
    private bool isActive = true;

    private List<Button> _menuButtons = new List<Button>();

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _document = GetComponent<UIDocument>();

        _button1 = _document.rootVisualElement.Q("BackButton") as Button;
        _button1.RegisterCallback<ClickEvent>(OnBackButtonClick);


        _button2 = _document.rootVisualElement.Q("TakePhotoButton") as Button;
        _button2.RegisterCallback<ClickEvent>(OnTakePhotoClick);

        
        _button3 = _document.rootVisualElement.Q("CareBookButton") as Button;
        _button3.RegisterCallback<ClickEvent>(OnCareBookClick);

        _button4 = _document.rootVisualElement.Q("ShopButton") as Button;
        _button4.RegisterCallback<ClickEvent>(OnShopClick);

        _button5 = _document.rootVisualElement.Q("PickItem") as Button;
        _button5.RegisterCallback<ClickEvent>(OnPickItem);

        resourceTracker = _document.rootVisualElement.Q("ResourceTracker") as VisualElement;
        resourceTracker.RegisterCallback<ClickEvent>(OnResourceTrackerClick);

        popUp = _document.rootVisualElement.Q("PopUp") as VisualElement;
        popUp.RegisterCallback<ClickEvent>(OnPopUpClick);
        StartCoroutine(HidePopUpAfterDelay(5f));

        _menuButtons = _document.rootVisualElement.Query<Button>().ToList();

        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnDisable()
    {
        _button1.UnregisterCallback<ClickEvent>(OnBackButtonClick);
        _button2.UnregisterCallback<ClickEvent>(OnTakePhotoClick);
        _button3.UnregisterCallback<ClickEvent>(OnCareBookClick);
        _button4.UnregisterCallback<ClickEvent>(OnShopClick);
        _button5.UnregisterCallback<ClickEvent>(OnPickItem);
        resourceTracker.UnregisterCallback<ClickEvent>(OnResourceTrackerClick);
        popUp.UnregisterCallback<ClickEvent>(OnPopUpClick);

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

    private void OnPickItem(ClickEvent evt)
    {
        Debug.Log("You are using an equipped item");
        UpdatePickButton(null);
    }

    private void UpdatePickButton(GameObject item)
    {
        _button5.style.backgroundImage = chilliSprite.texture;
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

    private void OnResourceTrackerClick(ClickEvent evt)
    {
         Debug.Log("You pressed Resource Tracker");

        if (isOriginal)
        {
            ChangeSprite(newSprite, 86f, 46f);
        } else {
            ChangeSprite(originalSprite, 30f, 18f);
        }

        isOriginal = !isOriginal;
    }

     private void ChangeSprite(Sprite sprite, float widthPercent, float heightPercent)
    {
        if (sprite != null)
        {
            resourceTracker.style.backgroundImage = new StyleBackground(sprite);

            resourceTracker.style.width = new Length(widthPercent, LengthUnit.Percent);;
            resourceTracker.style.height = new Length(heightPercent, LengthUnit.Percent);;
        }
    }

    private void OnPopUpClick(ClickEvent evt)
    {
        if (isActive)
        {
            popUp.style.display = DisplayStyle.None;
            isActive = false;
        }
    }

    private IEnumerator HidePopUpAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (isActive)
        {
            popUp.style.display = DisplayStyle.None;
            isActive = false;
        }
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        _audioSource.Play();
    }
}
