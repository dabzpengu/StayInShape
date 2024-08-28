using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMinigameTimerData : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI textUI;
    [SerializeField]
    MinigameTimerSO so;

    private void Awake()
    {
        DateTime dt = DateTime.ParseExact(so.openTime, so.timeFormat, null);
        textUI.text = dt.ToString("MMMM dd, yyyy");
    }
}
