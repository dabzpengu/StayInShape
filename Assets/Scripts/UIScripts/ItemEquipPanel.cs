using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemEquipPanel : MonoBehaviour
{
    [SerializeField]
    Image itemImage;
    [SerializeField]
    ItemDataSO itemDatabase;
    [SerializeField]
    Cuer cueButton;
    [SerializeField]
    Button nextItemButton;
    [SerializeField]
    Button previousItemButton;
    [SerializeField]
    Button equipButton;
    [SerializeField]
    TextMeshProUGUI noItemsHint;

    List<InteractiveItemSO> items;

    private void OnEnable()
    {
        items = RetrievePurchasedItems();
        ToggleSelectButtons(items.Count > 1);
        if (items.Count > 0)
        {
            ToggleEquipButton(true);
            SelectItem(items[0]);
        } else
        {
            ToggleEquipButton(false);
        }
    }

    List<InteractiveItemSO> RetrievePurchasedItems()
    {
        List<InteractiveItemSO> items = new List<InteractiveItemSO>();

        foreach (InteractiveItemSO item in itemDatabase.items)
        {
            if (item.purchased)
            {
                items.Add(item);
            }
        }

        return items;
    }

    void SelectItem(InteractiveItemSO item)
    {
        itemImage.sprite = item.sprite;
        cueButton.currentCue = item.equipCue;
    }

    void ToggleEquipButton(bool active)
    {
        equipButton.interactable = active;
        noItemsHint.enabled = !active;
    }

    void ToggleSelectButtons(bool active)
    {
        nextItemButton.interactable = active;
        previousItemButton.interactable = active;
    }

    /// <summary>
    /// Select the next purchased item. This funciton has no effect if there are no enough items purchased.
    /// </summary>
    public void NextItem()
    {
        if (items.Count < 2)
        {
            return;
        }

        InteractiveItemSO item = items[0];
        items.RemoveAt(0);
        items.Add(item);

        SelectItem(items[0]);
    }

    /// <summary>
    /// Select the previous purchased item. This funciton has no effect if there are no enough items purchased.
    /// </summary>
    public void PreviousItem()
    {
        if (items.Count < 2)
        {
            return;
        }

        InteractiveItemSO item = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
        items.Insert(0, item);

        SelectItem(items[0]);
    }
}
