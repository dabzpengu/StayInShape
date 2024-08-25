using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "SOs/Items/Item SO")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public int cost;
    public Sprite sprite;
}
