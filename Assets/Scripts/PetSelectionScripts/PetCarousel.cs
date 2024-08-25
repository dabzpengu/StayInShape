using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PetCarousel : MonoBehaviour
{
    [SerializeField]
    PetDataSO petData;
    [SerializeField]
    GameObject petHolder;
    [SerializeField]
    TMPro.TextMeshProUGUI petNameText;
    [SerializeField]
    SwipeDetection carouselSwipeDetection;
    [SerializeField]
    Button buttonRecolor;

    GameObject currentPetPrefab;
    RectTransform rect;
    List<PetSO> pets;
    int petIndex = 0;

    private void Awake()
    {
        pets = petData.pets;
        petData.currentPet = pets[petIndex];
        UpdatePet();
        rect = this.GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        carouselSwipeDetection.OnSwipe += OnSwipe;
    }
    private void OnDisable()
    {
        carouselSwipeDetection.OnSwipe -= OnSwipe;
    }
    public void NextPet()
    {
        petIndex = (petIndex + 1) % pets.Count;
        petData.currentPet = pets[petIndex];
        UpdatePet();
    }

    public void PreviousPet()
    {
        petIndex = (pets.Count + petIndex - 1) % pets.Count;
        petData.currentPet = pets[petIndex];
        UpdatePet();
    }

    public void UpdatePet()
    {
        if (currentPetPrefab) Destroy(currentPetPrefab);
        currentPetPrefab = Instantiate(petData.currentPet.petPrefab,petHolder.transform);
        currentPetPrefab.AddComponent<PetRotator>();
        if (petData.currentPet.species == PetSpecies.Bird)
        {
            currentPetPrefab.AddComponent<BirdHidePole>();
        }
        petNameText.text = petData.GetPetName(petData.currentPet);
    }

    private void OnGUI()
    {
        Color buttonColor = buttonRecolor.image.color;
        if (petData.currentPet.colors.Length > 1)
        {
            buttonRecolor.enabled = true;
            buttonRecolor.image.color = new Color(buttonColor.r, buttonColor.g, buttonColor.b, 1);
        }
        else
        {
            buttonRecolor.enabled = false;
            buttonRecolor.image.color = new Color(buttonColor.r, buttonColor.g, buttonColor.b, 0);
        }
    }

    public void OnSwipe(Vector2 position, SwipeDetection.DIRECTION direction)
    {
        if(rect.rect.Contains(rect.InverseTransformPoint(position)))
        if (direction == SwipeDetection.DIRECTION.LEFT)
        {
            PreviousPet();
        }
        else if (direction == SwipeDetection.DIRECTION.RIGHT) NextPet();
    }
}
