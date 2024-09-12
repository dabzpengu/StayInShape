using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "matchingCardDB", menuName = "SOs/matchingCardsDB")]
public class CardDB : ScriptableObject
{
    public List<MatchingCardSO> matchingCardsList = new List<MatchingCardSO>();

    public MatchingCardSO getCardData(int id)
    {
        return matchingCardsList[id];
    }
}
