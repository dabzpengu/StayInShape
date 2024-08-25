using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SavableSO : ScriptableObject
{
    public abstract string ToJson();
    public abstract void LoadFromString(string saveString);
}
