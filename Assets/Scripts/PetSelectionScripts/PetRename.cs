using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PetRename : MonoBehaviour
{
    [SerializeField] TextDialogBox renameDialog;
    [SerializeField] PetDataSO petDatabase;
    [SerializeField] TextMeshProUGUI petNameText;

    public void ShowRenameDialog()
    {
        renameDialog.ShowDialogue("Enter new name for your pet", () =>
        {
            Rename();
        }, () =>
        {
        });
    }

    void Rename()
    {
        Debug.Log("Player tried to rename pet to " + renameDialog.textField.text);
        petDatabase.SetCurrentPetName(renameDialog.textField.text);
        petNameText.text = renameDialog.textField.text;
    }
}
