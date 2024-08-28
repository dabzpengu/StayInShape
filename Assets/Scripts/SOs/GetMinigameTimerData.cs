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
        textUI.text = so.getInterpretableDateTime();
    }
}
