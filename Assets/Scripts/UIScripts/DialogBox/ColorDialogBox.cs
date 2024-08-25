using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorDialogBox : DialogBox
{
    public Color[] currentColors;

    [SerializeField]
    Transform colorListTransform;
    [SerializeField]
    GameObject colorPatternPrefab;
    [SerializeField]
    GameObject colorImagePrefab;
    [SerializeField]
    PetDataSO petDatabase;
    [SerializeField]
    PetCarousel petCarousel; // TODO: Remove coupling
    [SerializeField]
    PetColorsTrackerSO petColorTracker;

    void OnEnable()
    {
        LoadColors(petDatabase.currentPet);
    }

    void LoadColors(PetSO petSO)
    {
        if (petDatabase.currentPet == null)
        {
            return;
        }

        foreach (Transform oldColorPreview in colorListTransform.transform)
        {
            Destroy(oldColorPreview.gameObject);
        }

        if (petSO.colors == null || petSO.colors.Length < 2)
        {
            return;
        }

        for (int i = 0; i < petSO.colors.Length; i++)
        {
            PetColorPatternSO colorPattern = petSO.colors[i];
            int colorIndex = i;

            GameObject colorPreviewInstance = Instantiate(colorPatternPrefab);

            Button colorSelectButton = colorPreviewInstance.GetComponent<Button>();
            colorSelectButton.onClick.AddListener(delegate { SetDialogBoxColors(colorPattern.colors); });
            colorSelectButton.onClick.AddListener(delegate { SetPetColors(colorPattern.colors); });
            colorSelectButton.onClick.AddListener(delegate { petColorTracker.UpdateColorIndex(colorIndex); });

            AddImagePreviews(colorPreviewInstance, colorPattern);
            colorPreviewInstance.transform.SetParent(colorListTransform);
            colorPreviewInstance.GetComponent<RectTransform>().sizeDelta = new Vector2(60, 55 * colorPattern.colors.Length + 5);
        }
    }

    void AddImagePreviews(GameObject parent, PetColorPatternSO colorPattern)
    {
        foreach (Color color in colorPattern.colors)
        {
            GameObject imagePreviewInstance = Instantiate(colorImagePrefab);
            imagePreviewInstance.transform.SetParent(parent.transform);
            imagePreviewInstance.GetComponent<Image>().color = color;
        }
    }

    void SetDialogBoxColors(Color[] colors)
    {
        currentColors = colors;
    }

    void SetPetColors(Color[] colors)
    {
        Material petMaterial = petDatabase.currentPet.petPrefab.GetComponentInChildren<Renderer>().sharedMaterial;

        for (int i = 0; i < colors.Length; i++)
        {
            petMaterial.SetColor("_color" + i, colors[i]);
        }

        petCarousel.UpdatePet();
    }
}
