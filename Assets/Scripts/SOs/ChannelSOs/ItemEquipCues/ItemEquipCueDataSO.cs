using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Item Equip Cue Database")]
public class ItemEquipCueDataSO : ScriptableObject
{
    public List<ItemEquipCue> itemEquipCues;
}
