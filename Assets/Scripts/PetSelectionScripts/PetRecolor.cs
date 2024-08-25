using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetRecolor : MonoBehaviour
{
    public Material petMaterial;

    [SerializeField] ColorDialogBox recolorDialog;
    [SerializeField] PetDataSO petDatabase;
    [SerializeField] SaveManagerSO saveManager;
    [SerializeField] Button[] navigationButtons;

    Color[] originalColors;

    public void ShowRecolorDialog()
    {
        NavigationButtonsEnabled(false);

        petMaterial = petDatabase.currentPet.petPrefab.GetComponentInChildren<Renderer>().sharedMaterial;
        originalColors = GetColor(petMaterial);

        recolorDialog.ShowDialogue("Pick a color for your pet", () =>
        {
            saveManager.Save();
            NavigationButtonsEnabled(true);
        }, () =>
        {
            RevertColor();
            NavigationButtonsEnabled(true);
        });
    }

    void NavigationButtonsEnabled(bool state)
    {
        foreach (Button button in navigationButtons)
        {
            button.enabled = state;
        }
    }

    void RevertColor()
    {
        saveManager.Load();
        if (originalColors != null)
        {
            SetColors(originalColors);
        }
    }

    Color[] GetColor(Material petMaterial)
    {
        List<Color> colors = new List<Color>();
        int i = 0;
        while (true)
        {
            Color cur = petMaterial.GetColor("_color" + i);
            if (cur.a != 0.0f) {
                colors.Add(cur);
                i++;
            } else
            {
                break;
            }
        }

        return colors.ToArray();
    }

    void SetColors(Color[] colors)
    {
        for (int i = 0; i < colors.Length; i++)
        {
            petMaterial.SetColor("_color" + i, colors[i]);
        }
    }
}
