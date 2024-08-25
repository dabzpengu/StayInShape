using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Item Equip Cue")]
public class ItemEquipCue : CueSO
{
    public GameObject itemPrefab;
    public ItemSlot slot;
}
