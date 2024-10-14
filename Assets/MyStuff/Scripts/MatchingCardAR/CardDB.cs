using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "matchingCardDB", menuName = "SOs/matchingCardsDB")]
public class CardDB : ScriptableObject
{
    public List<MatchingCardSO> matchingCardsList = new List<MatchingCardSO>();

    public MatchingCardSO[] getRandomCardList()
    {
        System.Random rng = new System.Random();
        MatchingCardSO[] mcList = new MatchingCardSO[matchingCardsList.Count];
        matchingCardsList.CopyTo(mcList);

        int n = mcList.Length;

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            MatchingCardSO value = mcList[k];
            mcList[k] = mcList[n];
            mcList[n] = value;
        }
        return mcList;
    }

    public MatchingCardSO getCardData(int id)
    {
        return matchingCardsList[id];
    }
}
