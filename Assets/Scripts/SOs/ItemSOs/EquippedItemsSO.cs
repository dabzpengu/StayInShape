using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Database", menuName = "SOs/Items/Equipped Items Tracker")]
public class EquippedItemsSO : SavableSO
{
    public ItemEquipCue[] itemsToEquip;
    /// <summary>
    /// Channel to listen equip cues from
    /// </summary>
    public CueEventChannel cueEventChannel;
    public ItemEquipCueDataSO itemEquipCueDatabase;

    protected void OnEnable()
    {
        cueEventChannel.OnAnimationCueRequested += Invoke;
    }

    protected void OnDestroy()
    {
        cueEventChannel.OnAnimationCueRequested -= Invoke;
    }

    /// <summary>
    /// Logs the equipping cue in this class so it can be saved later
    /// </summary>
    protected void Invoke(CueSO cue)
    {
        ItemEquipCue itemEquip = (ItemEquipCue)cue;

        itemsToEquip = itemsToEquip ?? new ItemEquipCue[Enum.GetNames(typeof(ItemSlot)).Length];
        itemsToEquip[(int)itemEquip.slot] = itemEquip;
    }

    public override void LoadFromString(string saveString)
    {
        itemsToEquip = new ItemEquipCue[Enum.GetNames(typeof(ItemSlot)).Length];

        SaveObject loadedObject = JsonUtility.FromJson<SaveObject>(saveString);

        try
        {
            for (int i = 0; i < itemsToEquip.Length; i++)
            {
                int equipCueIndex = loadedObject.equipped[i];
                itemsToEquip[i] = itemEquipCueDatabase.itemEquipCues[equipCueIndex];
            }
        } catch (IndexOutOfRangeException e)
        {
            Debug.LogError("Save data contains illegal index for item equip cue, treat as if no item was equipped" + e.Message);
        }
    }

    public override string ToJson()
    {
        SaveObject saveObject = new SaveObject();

        int[] equipped = new int[Enum.GetNames(typeof(ItemSlot)).Length];
        List<ItemEquipCue> cueList = new List<ItemEquipCue>(itemEquipCueDatabase.itemEquipCues);

        foreach (ItemEquipCue cue in itemsToEquip)
        {
            int index = cueList.IndexOf(cue);
            equipped[(int)cue.slot] = index;
        }

        saveObject.equipped = equipped;

        string saveString = JsonUtility.ToJson(saveObject);
        return saveString;
    }

    private class SaveObject
    {
        public int[] equipped;
    }
}
