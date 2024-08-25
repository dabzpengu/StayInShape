using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEquipper : CueListener
{
    /// <summary>
    /// Transforms to place items on. Please make sure the index of each transform matches the order in ItemSlot enum.
    /// </summary>
    [SerializeField]
    Transform[] equipTransforms;

    GameObject[] itemInstances;
    GameObject[] itemPrefabReferences;

    protected new void OnEnable()
    {
        base.OnEnable();
        itemInstances = new GameObject[Enum.GetNames(typeof(ItemSlot)).Length];
        itemPrefabReferences = new GameObject[Enum.GetNames(typeof(ItemSlot)).Length];
    }

    protected override void Invoke(CueSO cue)
    {
        ItemEquipCue equipCue = (ItemEquipCue)cue;
        int slotIndex = (int)equipCue.slot;

        if (itemInstances[slotIndex] != null)
        {
            Destroy(itemInstances[slotIndex]);

            if (itemPrefabReferences[slotIndex] == equipCue.itemPrefab)
            {
                Debug.Log("Unequipping from slot " + equipCue.slot);
                return;
            }
        }

        itemPrefabReferences[slotIndex] = equipCue.itemPrefab;

        itemInstances[slotIndex] = Instantiate(equipCue.itemPrefab);
        itemInstances[slotIndex].transform.parent = equipTransforms[slotIndex];
        itemInstances[slotIndex].transform.localPosition = Vector3.zero;
        itemInstances[slotIndex].transform.localRotation = Quaternion.identity;
        itemInstances[slotIndex].transform.localScale = Vector3.one;
    } 
}
