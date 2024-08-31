using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyUI : CueListener
{
    [SerializeField] TextMeshProUGUI currencyText;
    [SerializeField] CurrencySO currencySO;

    void Awake()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        currencyText.text = currencySO.amount.ToString() + " Points";
    }

    protected override void Invoke(CueSO cue)
    {
        UpdateText();
    }
}
