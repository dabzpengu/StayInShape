using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BackgroundDatabase", menuName = "SOs/Background Database SO")]
public class BackgroundImageDataSO : SavableSO
{
    public Sprite[] backgrounds;
    public int currentBackgroundIndex;

    public Sprite GetCurrentBackground()
    {
        return backgrounds[currentBackgroundIndex];
    }

    public Sprite GetNextBackground()
    {
        currentBackgroundIndex++;
        if (currentBackgroundIndex >= backgrounds.Length)
        {
            currentBackgroundIndex = 0;
        }
        return backgrounds[currentBackgroundIndex];
    }

    public Sprite GetPreviousBackground()
    {
        currentBackgroundIndex--;
        if (currentBackgroundIndex < 0)
        {
            currentBackgroundIndex = backgrounds.Length - 1;
        }
        return backgrounds[currentBackgroundIndex];
    }

    public override string ToJson()
    {
        SaveObject saveObject = new SaveObject
        {
            backgroundIndex = currentBackgroundIndex,
        };

        string saveString = JsonUtility.ToJson(saveObject);
        return saveString;
    }

    public override void LoadFromString(string saveString)
    {
        SaveObject loadedObject = JsonUtility.FromJson<SaveObject>(saveString);
        currentBackgroundIndex = loadedObject.backgroundIndex;
    }

    private class SaveObject
    {
        public int backgroundIndex;
    }
}
