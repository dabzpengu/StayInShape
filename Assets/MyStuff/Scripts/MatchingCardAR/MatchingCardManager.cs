using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;


public class MatchingCardManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI selectedCardUI;
    [SerializeField] TextMeshProUGUI scoreUI;
    private AudioSource audioSource;
    [SerializeField] AudioClip selectClip;
    [SerializeField] AudioClip matchClip;
    [SerializeField] CardDB db;
    [SerializeField] PlayerDataSO player;
    [SerializeField] SaveManagerSO saveManager;

    private CardLogic selectedCard;
    private int nCardsLeft;
    private int currReward;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        scoreUI.text = "0";
        currReward = 0;
    }

    private void RewardPlayer(int reward)
    {
        player.SetFertilizer(player.GetFertilizer() + reward);
        player.SetWater(player.GetWater() + reward);
        Debug.Log(String.Format("You have earned {0} points! Current fertilizer is {1} and water is {2}", 
            reward, player.GetFertilizer(), player.GetWater()));
    }

    private void AddReward(int add)
    {
        currReward += add;
        scoreUI.text = currReward.ToString();
    }

    private void CompleteGame()
    {
        RewardPlayer(currReward);
        saveManager.Save();
    }

    public void SetupGame(Transform parentTransform, int spawnRange, float height, int nCards)
    {
        currReward = 0;
        nCardsLeft = nCards;
        Transform[] myCards = GetCards(parentTransform, nCards);
        RandomiseCards(myCards, nCards);
        ArrangeCards(myCards, spawnRange, height, nCards);
    }

    private void RandomiseCards(Transform[] cards, int nCards)
    {
        if (nCards % 2 != 0)
        {
            throw new System.Exception("Number of cards cannot be an odd number!");
        }
        for (int i = 0; i < nCards/2; i++)
        {
            MatchingCardSO cardData = db.getCardData(i);
            CardLogic cardA = cards[i].gameObject.GetComponent<CardLogic>();
            CardLogic cardB = cards[nCards - 1 - i].gameObject.GetComponent<CardLogic>();
            cardA.SetCard(cardData);
            cardB.SetCard(cardData);
        }
    }

    private Transform[] GetCards(Transform parentTransform, int nCards)
    {
        Transform[] cards = new Transform[nCards];
        int i = 0;
        foreach (Transform child in parentTransform)
        {
            if (child.TryGetComponent<CardLogic>(out CardLogic _))
            {
                cards[i] = child;
                i++;
            }
        }
        
        return cards;
    }

    private Vector3[] CalculateSpawnPositions(int nCards, int spawnRange, float height)
    {
        Vector3[] spawnPositions = new Vector3[nCards];
        float angleStep = 360f / nCards;
        float x, z;

        for (int i = 0; i < nCards; i++)
        {
            float angleDegrees = i * angleStep;
            float angleRadians = angleDegrees * Mathf.Deg2Rad;

            x = Mathf.Cos(angleRadians) * spawnRange;
            z = Mathf.Sin(angleRadians) * spawnRange;
            Vector3 newSpawnPosition = new Vector3(x, height, z);
            spawnPositions[i] = newSpawnPosition;
        }

        return spawnPositions;
    }

    private void shuffleCards(Transform[] cards)
    {
        System.Random random = new System.Random();
        int n = cards.Length;
        for (int i = n - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);  // Pick a random index from 0 to i
                                            // Swap array[i] with array[j]
            Transform temp = cards[i];
            cards[i] = cards[j];
            cards[j] = temp;
        }
    }
    private void PositionCards(Transform[] cards, Vector3[] spawnPositions)
    {
        float yRotation = 90;
        float rotateStep = 360 / cards.Length;
        shuffleCards(cards);

        int i = 0;
        foreach (Transform card in cards)
        {
            card.localPosition = spawnPositions[i];
            card.transform.Rotate(0, yRotation, 0);
            yRotation += rotateStep;
            i += 1;
        }
    }

    private void ArrangeCards(Transform[] myCards, int spawnRange, float height, int nCards)
    {
        Vector3[] spawnPositions = CalculateSpawnPositions(nCards, spawnRange, height);
        PositionCards(myCards, spawnPositions);
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }


    private void MatchCards(CardLogic card1, CardLogic card2)
    {
        Unselect();
        Destroy(card1.gameObject);
        Destroy(card2.gameObject);
        PlaySound(matchClip);
        nCardsLeft -= 2;
        AddReward(1);
        if (nCardsLeft == 0)
        {
            CompleteGame();
        }
    }

    public void SelectCard(CardLogic card)
    {
        if (selectedCard != null)
        {
            if (selectedCard.IsMatching(card)) {
                Debug.Log("IsMatching found!");
                MatchCards(selectedCard, card);
            } else
            {
                Debug.Log("Unable to match!");
            }
        }
        else if (card != selectedCard)
        {
            selectedCard = card;
            selectedCardUI.text = selectedCard.plantName;
            selectedCard.Select();
            PlaySound(selectClip);
        }
    }

    public void Unselect()
    {
        if (selectedCard)
        {
            selectedCard.Deselect();
            selectedCard = null;
            selectedCardUI.text = "";
        }
    }
}
