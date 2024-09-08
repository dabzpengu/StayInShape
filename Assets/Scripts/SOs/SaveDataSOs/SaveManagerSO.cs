using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(fileName = "Save Manager", menuName = "Saving/Save Manager SO")]
public class SaveManagerSO : ScriptableObject
{
    string SAVE_LOCATION;
    [SerializeField]
    SavableSO[] savables;

    private void OnEnable()
    {
        SAVE_LOCATION = Application.persistentDataPath + "/Saves/";

        if (!Directory.Exists(SAVE_LOCATION))
        {
            Directory.CreateDirectory(SAVE_LOCATION);
        }

        Load();
    }

    public void Save()
    {
        SaveObject saveObject = new SaveObject();
        saveObject.saveStrings = new List<string>();

        foreach (SavableSO savable in savables)
        {
            string itemJson = savable.ToJson();
            Debug.Log("Saving Json string: " + itemJson);
            saveObject.saveStrings.Add(itemJson);
        }

        string json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(SAVE_LOCATION + "save.JSON", json);
    }

    public void Load()
    {
        if (hasSaveFile())
        {
            Debug.Log("SAVE LOCATION" + SAVE_LOCATION);
            try
            {
                string json = File.ReadAllText(SAVE_LOCATION + "save.JSON");

                SaveObject saveObject = JsonUtility.FromJson<SaveObject>(json);
                for (int i = 0; i < saveObject.saveStrings.Count; i++)
                {
                    string itemJson = saveObject.saveStrings[i];

                    Debug.Log("Loading Json string: " + itemJson);

                    savables[i].LoadFromString(itemJson);
                    Debug.Log(i);
                }
            } catch (NullReferenceException)
            {
                Debug.Log("Save file item count mismatch, aborting load");
            }
        } else
        {
            Debug.Log("No save found");
        }
    }

    public Boolean hasSaveFile()
    {
        return File.Exists(Application.persistentDataPath + "/Saves/" + "save.JSON");
    }

    private class SaveObject
    {
        public List<string> saveStrings;
    }
}
