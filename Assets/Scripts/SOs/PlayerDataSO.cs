using System;
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
    private int steps = 200;
    private int exp = 0;
    private int chillicrop = 3;
    private int loofacrop = 0;
    private int eggplantcrop = 0;
    private int sweetpotatocrop = 0;
    private int papayacrop = 0;
    private int kalamansicrop = 0;
    private List<PlantData> plants = new List<PlantData>();
    private String snapTimer = DateTime.Now.AddDays(-1).ToString(DATETIME_FORMAT); // When the player can play the Snap minigame again
    private String matchingCardTimer = DateTime.Now.AddDays(-1).ToString(DATETIME_FORMAT); // When the player can play the matching cards minigame again
    private String chickenInvaderTimer = DateTime.Now.AddDays(-1).ToString(DATETIME_FORMAT); // When the player can play the chicken invaders minigame again

    const string DATETIME_FORMAT = "MM/dd/yyyy HH:mm";
    const string DATETIME_FORMAT_PLANT = "MM/dd/yyyy HH:mm:ss";
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
    public int GetChilliCrop()
    {
        return chillicrop;
    }

    public void SetChilliCrop(int newCropValue)
    {
        this.chillicrop += newCropValue;
    }

    public int GetLoofaCrop()
    {
        return loofacrop;
    }

    public void SetLoofaCrop(int newCropValue)
    {
        this.loofacrop += newCropValue;
    }

    public int GetEggplantCrop()
    {
        return eggplantcrop;
    }

    public void SetEggplantCrop(int newCropValue)
    {
        this.eggplantcrop += newCropValue;
    }

    public int GetSweetPotatoCrop()
    {
        return sweetpotatocrop;
    }

    public void SetSweetPotatoCrop(int newCropValue)
    {
        this.sweetpotatocrop += newCropValue;
    }

    public int GetPapayaCrop()
    {
        return papayacrop;
    }

    public void SetPapayaCrop(int newCropValue)
    {
        this.papayacrop += newCropValue;
    }

    public int GetKalamansiCrop()
    {
        return kalamansicrop;
    }

    public void SetKalamansiCrop(int newCropValue)
    {
        this.kalamansicrop += newCropValue;
    }
    public List<PlantData> GetPlants()
    {
        return plants;
    }


    public void SetPlant(PlantData plantData)
    {
        plants.Add(plantData);
    }

    public void ClearList()
    {
        this.plants = new List<PlantData>();
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

    // Chicken Invaders
    public String GetChickenInvaderTimer() { return chickenInvaderTimer; }

    public void SetChickenInvaderTimer(DateTime dt)
    {
        this.chickenInvaderTimer = dt.ToString(DATETIME_FORMAT);
    }

    public Boolean CanPlayChickenInvaders()
    {
        DateTime openDateTime = DateTime.ParseExact(this.chickenInvaderTimer, DATETIME_FORMAT, null);
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
            chillicrop = this.chillicrop,
            loofacrop = this.loofacrop,
            eggplantcrop = this.eggplantcrop,
            sweetpotatocrop = this.sweetpotatocrop,
            papayacrop = this.papayacrop,
            kalamansicrop = this.kalamansicrop,
            plants = this.plants,
            snapTimer = this.snapTimer,
            matchingCardTimer = this.matchingCardTimer,
            chickenInvaderTimer = this.chickenInvaderTimer,
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
        loofacrop = loadedObject.loofacrop;
        chillicrop = loadedObject.chillicrop;
        eggplantcrop = loadedObject.eggplantcrop;
        sweetpotatocrop = loadedObject.sweetpotatocrop;
        papayacrop= loadedObject.papayacrop;
        kalamansicrop= loadedObject.kalamansicrop;
        plants = loadedObject.plants;
        snapTimer = loadedObject.snapTimer;
        matchingCardTimer = loadedObject.matchingCardTimer;
        chickenInvaderTimer = loadedObject.chickenInvaderTimer;

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
        public int chillicrop;
        public int loofacrop;
        public int eggplantcrop;
        public int sweetpotatocrop;
        public int papayacrop;
        public int kalamansicrop;
        public List<PlantData> plants;
        public String snapTimer;
        public String matchingCardTimer;
        public String chickenInvaderTimer;
    }
}
