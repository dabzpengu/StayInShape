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
    private int fertilizer = 0;
    private int water = 0;
    private int steps = 0;
    private int exp = 0;

    const string DATETIME_FORMAT = "MM/dd/yyyy HH:mm";
    public void ResetSurveyTime()
    {
        lastSurvey = DateTime.Now;
    }
    public bool IsNextSurveyAvailable()
    {
        return (DateTime.Now - lastSurvey).TotalSeconds > 86400;
    }
    public int GetFertilizer() 
    {
        return fertilizer;
    }
    public void SetFertilizer(int newFertilizerValue)
    {
        this.fertilizer = newFertilizerValue;
    }

    public int GetWater() 
    {
        return water;
    }
    public void SetWater(int newWaterValue)
    {
        this.water = newWaterValue;
    }

    public int GetSteps() 
    {
        return steps;
    }
    public void SetSteps(int newStepCount)
    {
        this.steps = newStepCount;
    }

    public int GetExp()
    {
        return exp;
    }
    public void SetExp(int newExpValue)
    {
        this.exp = newExpValue;
    }
    public override string ToJson()
    {
        SaveObject saveObject = new SaveObject
        {
            playerName = this.playerName,
            lastSurvey = lastSurvey.ToString(DATETIME_FORMAT),
            fertilizer = this.fertilizer,
            water = this.water,
            steps = this.steps,
            exp = this.exp,
        };

        string saveString = JsonUtility.ToJson(saveObject);
        //Debug.Log("Generated save string for " + this + ": " + saveString);
        return saveString;
    }

    // Currently unchanged from the original PlayerClassSO
    public override void LoadFromString(string saveString)
    {
        SaveObject loadedObject = JsonUtility.FromJson<SaveObject>(saveString);
        lastSurvey = DateTime.ParseExact(loadedObject.lastSurvey, DATETIME_FORMAT, CultureInfo.InvariantCulture);
    }

    private class SaveObject
    {
        public string playerName;
        public string lastSurvey;
        public int fertilizer;
        public int water;
        public int steps;
        public int exp;  
    }
}
