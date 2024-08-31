using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[CreateAssetMenu(fileName = "Pet New Status Data", menuName = "Pet Data/Pet Status Data SO")]
public class PetStatusTrackerSO : SavableSO
{
    [SerializeField]
    public int level = 0;
    [SerializeField]
    float timeToLevelUp = 5.0f;
    public DateTime lastReset;

    const string DATETIME_FORMAT = "MM/dd/yyyy HH:mm";

    private void OnEnable()
    {
        Debug.Log(lastReset);
    }

    public void Reset()
    {
        if (level != 0)
        {
            level--;
        }
        lastReset = DateTime.Now;
    }

    public bool CanLevelUp()
    {
        return (DateTime.Now - lastReset).TotalSeconds > timeToLevelUp;
    }

    public override string ToJson()
    {
        SaveObject saveObject = new SaveObject
        {
            lastReset = lastReset.ToString(DATETIME_FORMAT),
        };

        string saveString = JsonUtility.ToJson(saveObject);
        return saveString;
    }

    public override void LoadFromString(string saveString)
    {
        SaveObject loadedObject = JsonUtility.FromJson<SaveObject>(saveString);
        lastReset = DateTime.ParseExact(loadedObject.lastReset, DATETIME_FORMAT, CultureInfo.InvariantCulture);
    }

    private class SaveObject
    {
        public string lastReset;
    }
}
