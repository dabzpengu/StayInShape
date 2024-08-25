using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAutoEquip : Cuer
{
    [SerializeField]
    EquippedItemsSO equippedItems;

    void Start()
    {
        StartCoroutine(WaitThenEquip());
    }

    IEnumerator WaitThenEquip()
    {
        yield return new WaitForSeconds(1.0f);

        EquipItems();
    }

    void EquipItems()
    {
        foreach (ItemEquipCue itemEquipCue in equippedItems.itemsToEquip)
        {
            if (itemEquipCue == null)
            {
                continue;
            }
            Debug.Log("Auto equipping " + itemEquipCue.name);
            currentCue = itemEquipCue;
            Cue();
        }
    }
}
