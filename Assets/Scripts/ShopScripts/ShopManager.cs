using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    ShopItemUI[] shopItemPrefabs;
    [SerializeField]
    ItemDataSO itemDatabase;

    private void Awake()
    {
        int i = 0;
        while(i < shopItemPrefabs.Length && i < itemDatabase.items.Length)
        {
            shopItemPrefabs[i].Setup(itemDatabase.items[i]);
            i++;
        }
    }
}
