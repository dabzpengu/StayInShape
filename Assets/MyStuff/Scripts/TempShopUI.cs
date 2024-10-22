using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using System;

public class TempShopUI : MonoBehaviour
{
    [SerializeField] private Button buyChilliButton;
    [SerializeField] private Button buyLoofaButton;
    [SerializeField] private Button buyEggplantButton;
    [SerializeField] private Button backButton;
    [SerializeField] private PlayerDataSO player;
    [SerializeField] private SaveManagerSO saveManager;

    private void Start()
    {
        buyChilliButton.onClick.AddListener(BuyChilli);
        buyLoofaButton.onClick.AddListener(BuyLoofa);
        buyEggplantButton.onClick.AddListener(BuyEggplant);
        backButton.onClick.AddListener(BackButton);
    }

    private void Awake()
    {
        saveManager.Load();
    }

    public void BuyChilli()
    {
        player.SetChilliCrop(1);
        saveManager.Save();
    }

    public void BuyEggplant()
    {
        if (player.GetChilliCrop() >= 3)
        {
            player.SetChilliCrop(-3);
            player.SetEggplantCrop(1);
            saveManager.Save();
        }
        else
        {
            Debug.Log("Not enough");
        }
    }

    public void BuyLoofa()
    {
        if(player.GetChilliCrop() >= 3 && player.GetEggplantCrop() >= 3)
        {
            player.SetChilliCrop(-3);
            player.SetEggplantCrop(-3);
            player.SetLoofaCrop(1);
            saveManager.Save();
        }
        else
        {
            Debug.Log("Not enough");
        }
    }

    public void BackButton()
    {
        SceneManager.LoadScene("GardenScene");
    }
}
