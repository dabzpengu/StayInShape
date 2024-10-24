using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    [SerializeField] PlayerDataSO player;
    [SerializeField] SaveManagerSO saveManager;
    [SerializeField] ShopUIEvents shopUIEvents;

    private void Awake()
    {
        if (instance == null)
        {
            Debug.Log("new shop manager");
            instance = this;
            saveManager.Load();
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    private void Update()
    {
        shopUIEvents.SetChilliStockValue(player.GetChilliCrop());
        shopUIEvents.SetEggplantStockValue(player.GetEggplantCrop());
        shopUIEvents.SetLoofaStockValue(player.GetLoofaCrop());
        shopUIEvents.SetSweetPotatoStockValue(player.GetSweetPotatoCrop());
        shopUIEvents.SetPapayaValue(player.GetPapayaCrop());
        shopUIEvents.SetKalamansiStockValue(player.GetKalamansiCrop());
    }

    public void AddChilli()
    {
        if(player.GetWater() >= 3)
        {
            player.SetChilliCrop(1);
            player.SetWater(-3);
            saveManager.Save();
        }
        else
        {
            Debug.Log("Not enough");
        }
    }

    public void AddEggplant()
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

    public void AddLoofa()
    {
        if (player.GetChilliCrop() >= 3 && player.GetEggplantCrop() >= 3 && Mathf.Floor(player.GetExp() / 1000) >= 2)
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

    public void AddSweetPotato()
    {
        if(player.GetChilliCrop() >= 1 && player.GetEggplantCrop() >= 3 && player.GetLoofaCrop() >= 4 && Mathf.Floor(player.GetExp() / 1000) >= 3)
        {
            player.SetChilliCrop(-1);
            player.SetEggplantCrop(-3);
            player.SetLoofaCrop(-4);
            player.SetSweetPotatoCrop(1);
            saveManager.Save();
        }
        else
        {
            Debug.Log("Either not enough or not high enough level");
        }
    }

    public void AddPapaya()
    {
        if(player.GetLoofaCrop() >= 3 && player.GetSweetPotatoCrop() >= 3 && player.GetKalamansiCrop() >= 5 && Mathf.Floor(player.GetExp() / 1000) >= 5)
        {
            player.SetLoofaCrop(-3);
            player.SetSweetPotatoCrop(-3);
            player.SetKalamansiCrop(-5);
            player.SetPapayaCrop(1);
            saveManager.Save();
        }
        else
        {
            Debug.Log("Either not enough or not high enough level");
        }
    }

    public void AddKalamansi()
    {
        if(player.GetEggplantCrop() >= 2 && player.GetLoofaCrop() >= 2 && player.GetSweetPotatoCrop() >= 4 && Mathf.Floor(player.GetExp() / 1000) >= 4)
        {
            player.SetEggplantCrop(-2);
            player.SetLoofaCrop(-2);
            player.SetSweetPotatoCrop(-4);
            player.SetKalamansiCrop(1);
            saveManager.Save();
        }
        else
        {
            Debug.Log("Either not enough or not high enough level");
        }
    }
}
