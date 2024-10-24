using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using System.Xml.Serialization;

public class ShopUIEvents : MonoBehaviour
{
    private UIDocument _document;

    private Button _button1;
    private Button _button2;
    private Button _button3;
    private Button _button4;
    private Button _button5;
    private Button _button6;
    private Button _button7;

    private IntegerField chilliStock;
    private IntegerField eggplantStock;
    private IntegerField loofaStock;
    private IntegerField sweetpotatoStock;
    private IntegerField papayaStock;
    private IntegerField kalamansiStock;

    private List<Button> _menuButtons = new List<Button>();

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _document = GetComponent<UIDocument>();

        chilliStock = _document.rootVisualElement.Q("chilliStock") as IntegerField;
        eggplantStock = _document.rootVisualElement.Q("eggplantStock") as IntegerField; 
        loofaStock = _document.rootVisualElement.Q("loofaStock") as IntegerField;
        sweetpotatoStock = _document.rootVisualElement.Q("sweetPotatoStock") as IntegerField;
        papayaStock = _document.rootVisualElement.Q("papayaStock") as IntegerField;
        kalamansiStock = _document.rootVisualElement.Q("kalamansiStock") as IntegerField;


        _button1 = _document.rootVisualElement.Q("BackButton") as Button;
        _button1.RegisterCallback<ClickEvent>(OnBackButtonClick);

        _button2 = _document.rootVisualElement.Q("ChilliButton") as Button;
        _button2.RegisterCallback<ClickEvent>(OnBuyChilli);

        _button3 = _document.rootVisualElement.Q("EggplantButton") as Button;
        _button3.RegisterCallback<ClickEvent>(OnBuyEggplant);

        _button4 = _document.rootVisualElement.Q("LoofaButton") as Button;
        _button4.RegisterCallback<ClickEvent>(OnBuyLoofa);

        _button5 = _document.rootVisualElement.Q("SweetPotatoButton") as Button;
        _button5.RegisterCallback<ClickEvent>(OnBuySweetPotato);

        _button6 = _document.rootVisualElement.Q("CalamansiButton") as Button;
        _button6.RegisterCallback<ClickEvent>(OnBuyCalamansi);

        _button7 = _document.rootVisualElement.Q("PapayaButton") as Button;
        _button7.RegisterCallback<ClickEvent>(OnBuyPapaya);

        _menuButtons = _document.rootVisualElement.Query<Button>().ToList();

        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnDisable()
    {
        _button1.UnregisterCallback<ClickEvent>(OnBackButtonClick);

        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnBackButtonClick(ClickEvent evt)
    {
        Debug.Log("You pressed Back Button");

        SceneManager.LoadScene("GardenSceneJia");
    }

    private void OnBuyChilli(ClickEvent evt)
    {
        Debug.Log("Buying Chilli");
        ShopManager.instance.AddChilli();

    }

    private void OnBuyEggplant(ClickEvent evt)
    {
        Debug.Log("Buying Eggplant");
        ShopManager.instance.AddEggplant();
    }

    private void OnBuyLoofa(ClickEvent evt)
    {
        Debug.Log("Buying Loofa");
        ShopManager.instance.AddLoofa();
    }

    private void OnBuySweetPotato(ClickEvent evt)
    {
        Debug.Log("Buying sweet potato");
        ShopManager.instance.AddSweetPotato();
    }

    private void OnBuyCalamansi(ClickEvent evt)
    {
        Debug.Log("Buying calamansi");
        ShopManager.instance.AddKalamansi();
    }

    private void OnBuyPapaya(ClickEvent evt)
    {
        Debug.Log("Buying papaya");
        ShopManager.instance.AddPapaya();
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        _audioSource.Play();
    }

    public void SetChilliStockValue(int value)
    {
        chilliStock.value = value;
    }
    public void SetEggplantStockValue(int value)
    {
        eggplantStock.value = value;
    }
    public void SetLoofaStockValue(int value)
    {
        loofaStock.value = value;
    }
    public void SetSweetPotatoStockValue(int value)
    {
        sweetpotatoStock.value = value;
    }
    public void SetPapayaValue(int value)
    {
        papayaStock.value = value;
    }
    public void SetKalamansiStockValue(int value)
    {
        kalamansiStock.value = value;
    }
}
