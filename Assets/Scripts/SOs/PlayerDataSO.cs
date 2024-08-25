using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;


[CreateAssetMenu(fileName = "Currency", menuName = "SOs/PlayerDataSO")]
public class PlayerDataSO : SavableSO
{
    public string playerName = "DEFAULTUSERNAME";
    public DateTime lastSurvey;

    const string DATETIME_FORMAT = "MM/dd/yyyy HH:mm";
    public void ResetSurveyTime()
    {
        lastSurvey = DateTime.Now;
    }
    public bool IsNextSurveyAvailable()
    {
        return (DateTime.Now - lastSurvey).TotalSeconds > 86400;
    }
    public override string ToJson()
    {
        SaveObject saveObject = new SaveObject
        {
            playerName = this.playerName,
            lastSurvey = lastSurvey.ToString(DATETIME_FORMAT),
        };

        string saveString = JsonUtility.ToJson(saveObject);
        //Debug.Log("Generated save string for " + this + ": " + saveString);
        return saveString;
    }

    public override void LoadFromString(string saveString)
    {
        SaveObject loadedObject = JsonUtility.FromJson<SaveObject>(saveString);
        lastSurvey = DateTime.ParseExact(loadedObject.lastSurvey, DATETIME_FORMAT, CultureInfo.InvariantCulture);
    }

    private class SaveObject
    {
        public string playerName;
        public string lastSurvey;
    }
}
