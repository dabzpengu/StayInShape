using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainSceneUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI orderButtonText;
    [SerializeField]
    private Cuer orderCuer;
    [SerializeField]
    Cuer audioCuer;
    [SerializeField]
    Cuer barkAudioCuer;
    [SerializeField]
    TextMeshProUGUI barkButtonText;

    private CueSO[] animationCueList;
    private int animationIndex;

    public void AssignPet(PetSO pet)
    {
        animationCueList = pet.animationCueList;

        animationIndex = 0;

        UpdateOrderButton();

        barkAudioCuer.currentCue = pet.barkAudioCue;
        barkButtonText.text = "Tap for " + pet.barkAudioCue.cueName;
    }

    public void NextAnimation()
    {
        animationIndex -= 1;
        if (animationIndex < 0)
        {
            animationIndex = animationCueList.Length - 1;
        }

        UpdateOrderButton();
    }

    public void PreviousAnimation()
    {
        animationIndex += 1;
        if (animationIndex >= animationCueList.Length)
        {
            animationIndex = 0;
        }

        UpdateOrderButton();
    }

    private void UpdateOrderButton()
    {
        if (animationCueList.Length > 0)
        {
            orderButtonText.text = animationCueList[animationIndex].cueName;
            orderCuer.currentCue = animationCueList[animationIndex];
            if (orderCuer.currentCue is AudioAnimationCue)
            {
                audioCuer.currentCue = ((AudioAnimationCue)orderCuer.currentCue).audioCue;
            } else
            {
                audioCuer.currentCue = null;
            }
        } else
        {
            orderButtonText.text = "No Orders";
        }
    }
}
