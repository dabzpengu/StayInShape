using System;
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
    private int crop = 3;
    private String snapTimer = DateTime.Now.AddDays(-1).ToString(DATETIME_FORMAT); // When the player can play the Snap minigame again
    private String matchingCardTimer = DateTime.Now.AddDays(-1).ToString(DATETIME_FORMAT); // When the player can play the matching cards minigame again

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
        this.exp += newExpValue;
    }
    //garden
    public int GetCrop()
    {
        return crop;
    }

    public void SetCrop(int newCropValue)
    {
        this.crop += newCropValue;
    }
    // Snap
    public String GetSnapTimer() { return snapTimer; }

    public void SetSnapTimer(DateTime dt)
    {
        this.snapTimer = dt.ToString(DATETIME_FORMAT);
    }

    public Boolean CanPlaySnap()
    {
        DateTime openDateTime = DateTime.ParseExact(this.snapTimer, DATETIME_FORMAT, null);
        DateTime nowDateTime = DateTime.Now;
        Boolean ans = nowDateTime >= openDateTime;
        return ans;
    }

    // Matching Card
    public String GetMatchingCardTimer() { return matchingCardTimer; }

    public void SetMatchingCardTimer(DateTime dt)
    {
        this.matchingCardTimer = dt.ToString(DATETIME_FORMAT);
    }

    public Boolean CanPlayMatchingCard()
    {
        DateTime openDateTime = DateTime.ParseExact(this.matchingCardTimer, DATETIME_FORMAT, null);
        DateTime nowDateTime = DateTime.Now;
        Boolean ans = nowDateTime >= openDateTime;
        return ans;
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
            snapTimer = this.snapTimer,
            matchingCardTimer = this.matchingCardTimer,
        };

        string saveString = JsonUtility.ToJson(saveObject);
        //Debug.Log("Generated save string for " + this + ": " + saveString);
        return saveString;
    }

    public override void LoadFromString(string saveString)
    {
        SaveObject loadedObject = JsonUtility.FromJson<SaveObject>(saveString);
        fertilizer = loadedObject.fertilizer;
        water = loadedObject.water;
        steps = loadedObject.steps;
        exp = loadedObject.exp;
        snapTimer = loadedObject.snapTimer;
        matchingCardTimer = loadedObject.matchingCardTimer;

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
        public String snapTimer;
        public String matchingCardTimer;
    }
}
