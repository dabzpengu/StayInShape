using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Database", menuName = "SOs/Items/Item Database")]
public class ItemDataSO : SavableSO
{
    public InteractiveItemSO[] items;

    /// <summary>
    /// Marks the item passed in as purchased
    /// </summary>
    /// <returns>
    /// True if the item was found, else False
    /// </returns>
    public bool Purchase(InteractiveItemSO itemToPurchase)
    {
        foreach (InteractiveItemSO item in items)
        {
            if (item == itemToPurchase)
            {
                item.purchased = true;
                return true;
            }
        }

        return false;
    }

    public override string ToJson()
    {
        List<bool> purchasedList = new List<bool>();

        foreach(InteractiveItemSO item in items)
        {
            purchasedList.Add(item.purchased); 
        }

        SaveObject saveObject = new SaveObject
        {
            purchased = purchasedList.ToArray()
        };

        string saveString = JsonUtility.ToJson(saveObject);
        return saveString;
    }

    public override void LoadFromString(string saveString)
    {
        SaveObject loadedObject = JsonUtility.FromJson<SaveObject>(saveString);
        for (int i = 0; i < items.Length; i++)
        {
            if (i >= loadedObject.purchased.Length)
            {
                items[i].purchased = false;
            } else
            {
                items[i].purchased = loadedObject.purchased[i];
            }
        }
    }

    private class SaveObject
    {
        public bool[] purchased;
    }
}
