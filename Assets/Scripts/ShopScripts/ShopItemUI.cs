using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public ItemSO currentItem;
    [SerializeField]
    Image image;
    [SerializeField]
    TMPro.TextMeshProUGUI nameText, costText;

    public void Setup(ItemSO itemSO)
    {
        currentItem = itemSO;
        UpdateUI();
    }

    public void UpdateUI()
    {
        image.sprite = currentItem.sprite;
        nameText.text = currentItem.itemName;

        if (currentItem is InteractiveItemSO && ((InteractiveItemSO)currentItem).purchased)
        {
            costText.text = "Purchased!";
        } else
        {
            costText.text = currentItem.cost + " points";
        }
    }
}
