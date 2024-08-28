using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

[CreateAssetMenu(fileName = "MinigameTimer", menuName = "SOs/MinigameTimer SO")]
public class MinigameTimerSO : SavableSO
{
    public String openTime;
    private String timeFormat = "yyyyMMdd";

    public override string ToJson()
    {
        SaveObject saveObject = new SaveObject
        {
            openTime = this.openTime,
        };
        string saveString = JsonUtility.ToJson(saveObject);
        return saveString;
    }

    public override void LoadFromString(string saveString)
    {
        SaveObject loadedObject = JsonUtility.FromJson<SaveObject>(saveString);
        openTime = loadedObject.openTime;
    }

    private class SaveObject
    {
        public String openTime;
    }

    public void SetOpenTime(DateTime openDateTime)
    {
        this.openTime = openDateTime.ToString(timeFormat);
    }

    public void SetNextOpenTimeFromNow()
    {
        SetOpenTime(DateTime.Now.AddDays(1));
    }

    public Boolean canPlayNow()
    {
        DateTime openDateTime = DateTime.ParseExact(openTime, timeFormat, null);
        DateTime nowDateTime = DateTime.Now;
        Boolean ans = nowDateTime >= openDateTime;
        return ans;
    }

    public String getInterpretableDateTime()
    {
        DateTime dt = DateTime.ParseExact(openTime, timeFormat, null);
        return dt.ToString("MMMM dd, yyyy");
    }
}
