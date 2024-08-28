using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPlayerStats : MonoBehaviour
{
    [SerializeField]
    SaveManagerSO saveManager;
    [SerializeField]
    CurrencySO currency;
    [SerializeField]
    MinigameTimerSO snapTimer;

    [SerializeField]
    TMPro.TextMeshProUGUI currencyUI;
    [SerializeField]
    TMPro.TextMeshProUGUI snapUI;
    // Start is called before the first frame update
    void Awake()
    {
        if (saveManager.hasSaveFile())
        {
            saveManager.Load();
            currencyUI.text = currency.amount.ToString();
            snapUI.text = snapTimer.getInterpretableDateTime();
        } else
        {
            currency.amount = 0;
            snapTimer.openTime = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
            currencyUI.text = "0";
            snapUI.text = "Open to play";
        }
    }
}
