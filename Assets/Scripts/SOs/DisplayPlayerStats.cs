using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPlayerStats : MonoBehaviour
{
    [SerializeField]
    SaveManagerSO saveManager;
    [SerializeField]
    PlayerDataSO playerData;
    [SerializeField]
    TMPro.TextMeshProUGUI fertilizerUI;
    [SerializeField]
    TMPro.TextMeshProUGUI waterUI;
    [SerializeField]
    TMPro.TextMeshProUGUI expUI;
    [SerializeField]
    TMPro.TextMeshProUGUI stepsUI;
    [SerializeField]
    TMPro.TextMeshProUGUI snapUI;

    // Start is called before the first frame update
    void Awake()
    {
        if (saveManager.hasSaveFile())
        {
            saveManager.Load();
        }
        fertilizerUI.text = playerData.GetFertilizer().ToString();
        waterUI.text = playerData.GetWater().ToString();
        expUI.text = playerData.GetExp().ToString();
        stepsUI.text = playerData.GetSteps().ToString();
        snapUI.text = playerData.CanPlaySnap() ? "Can play now" : "Can only play after " + playerData.GetSnapTimer();
    }
}
