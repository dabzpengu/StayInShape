using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "Currency", menuName = "SOs/Currency SO")]
public class CurrencySO : SavableSO
{
    public int amount = 1000;

    public override string ToJson()
    {
        SaveObject saveObject = new SaveObject
        {
            amount = this.amount,
        };

        string saveString = JsonUtility.ToJson(saveObject);
        //Debug.Log("Generated save string for " + this + ": " + saveString);
        return saveString;
    }

    public override void LoadFromString(string saveString)
    {
        SaveObject loadedObject = JsonUtility.FromJson<SaveObject>(saveString);
        amount = loadedObject.amount;
    }

    private class SaveObject
    {
        public int amount;
    }

    public void AddAmount(int amount)
    {
        this.amount += amount;
    }

    public void SubtractAmount(int amount)
    {
        this.amount -= amount;
    }
}
