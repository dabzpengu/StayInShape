using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "SOs/Interactive Item")]
public class InteractiveItemSO : ItemSO
{
    public bool purchased;
    public ItemEquipCue equipCue;
}
