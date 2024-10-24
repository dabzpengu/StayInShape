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

    private IntegerField fertiliserValue;
    private IntegerField waterValue;

    private VisualElement resourceTracker;
    private VisualElement pickedItem;
    public Sprite originalSprite;
    public Sprite newSprite;
    public Sprite waterSprite;
    public Sprite fertilizerSprite;
    public Sprite trowelSprite;
    public Sprite chilliSprite;
    public Sprite eggplantSprite;
    public Sprite loofaSprite;
    public Sprite papayaSprite;
    public Sprite kalamansiSprite;
    public Sprite sweetPotatoSprite;
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

        fertiliserValue = _document.rootVisualElement.Q("fertiliserValue") as IntegerField;
        waterValue = _document.rootVisualElement.Q("waterValue") as IntegerField;


        pickedItem = _document.rootVisualElement.Q("PickedItem") as VisualElement;

        //resourceTracker = _document.rootVisualElement.Q("ResourceTracker") as VisualElement;
        //resourceTracker.RegisterCallback<ClickEvent>(OnResourceTrackerClick);

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
        //resourceTracker.UnregisterCallback<ClickEvent>(OnResourceTrackerClick);
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
    public void UpdatePickedItem(int item)
    {
        if(item == 1)
        {
            Debug.Log("ITS WATER CUHHH");
            pickedItem.style.backgroundImage = new StyleBackground(waterSprite);
        }
        else if(item == 2)
        {
            Debug.Log("FAERTILISERRR");
            pickedItem.style.backgroundImage = new StyleBackground(fertilizerSprite);
        }
        else if(item == 3)
        {
            Debug.Log("TROOOOWELLLL");
            pickedItem.style.backgroundImage = new StyleBackground(trowelSprite);
        }
        else if(item == 4)
        {
            Debug.Log("CHILLIIIIIIIII");
            pickedItem.style.backgroundImage = new StyleBackground(chilliSprite);
        }
        else if (item == 5)
        {
            Debug.Log("EGGPLANT");
            pickedItem.style.backgroundImage = new StyleBackground(eggplantSprite);
        }
        else if(item == 6)
        {
            Debug.Log("LOOOOOFAAAA");
            pickedItem.style.backgroundImage= new StyleBackground(loofaSprite);
        }
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

        SceneManager.LoadScene("CareBookScene");
    }

    private void OnShopClick(ClickEvent evt)
    {
        Debug.Log("You pressed Shop Button");

        SceneManager.LoadScene("ShopScene");
    }

    /**
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
    **/

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
    
    public void setWaterText(int value)
    {
        waterValue.value = value;
    }

    public void setFertiliserText(int value)
    {
        fertiliserValue.value = value;
    }
    private void OnAllButtonsClick(ClickEvent evt)
    {
        _audioSource.Play();
    }
}
