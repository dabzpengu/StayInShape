using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedPetButton : MonoBehaviour
{
    [SerializeField]
    CurrencySO currency;
    [SerializeField]
    PetStatusTrackerSO petHungerTracker;
    [SerializeField]
    Cuer animationCuer, audioCuer;
    [SerializeField]
    SaveManagerSO saveManager;

    public void Feed(int cost)
    {
        if (cost > currency.amount)
        {
            Debug.Log("No enough money to feed the pet");
            return;
        }

        currency.SubtractAmount(cost);

        petHungerTracker.Reset();
        animationCuer.Cue();
        audioCuer.Cue();

        saveManager.Save();
    }
}
