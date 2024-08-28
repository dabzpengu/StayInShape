using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCurrencyData : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI textUI;
    [SerializeField]
    CurrencySO so;

    private void Awake()
    {
        textUI.text = so.amount.ToString();
    }
}
