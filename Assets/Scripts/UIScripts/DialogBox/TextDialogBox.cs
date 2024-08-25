using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDialogBox : DialogBox
{
    public TMP_InputField textField;

    private void Awake()
    {
        textField.text = "";
    }
}
