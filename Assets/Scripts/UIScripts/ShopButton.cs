using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    [SerializeField]
    CurrencySO currencySO;

    public void Buy()
    {
        ItemSO item = GetComponent<ShopItemUI>().currentItem;

        if (!(item is InteractiveItemSO))
        {
            Debug.LogWarning("Tried to purchase a non interactive item SO");
            return;
        }

        InteractiveItemSO interactiveItem = (InteractiveItemSO) item;

        if (item.cost > currencySO.amount)
        {
            Debug.Log("No enough money");
            return;
        }

        if (interactiveItem.purchased)
        {
            Debug.Log("Tried to purchase an item that is already purchased");
            return;
        }

        currencySO.SubtractAmount(item.cost);
        interactiveItem.purchased = true;
    }
}
