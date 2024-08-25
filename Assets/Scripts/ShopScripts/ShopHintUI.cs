using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopHintUI : MonoBehaviour
{
    [SerializeField] PetDataSO petDatabase;
    [SerializeField] TextMeshProUGUI hintText;

    private void Awake()
    {
        hintText.text = "Use your points to buy something nice for " + petDatabase.GetCurrentPetName() + "!";
    }
}
