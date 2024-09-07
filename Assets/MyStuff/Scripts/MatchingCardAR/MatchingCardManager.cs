using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MatchingCardManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI selectedCardUI;
    private AudioSource audioSource;
    [SerializeField] AudioClip selectClip;
    [SerializeField] AudioClip matchClip;
    [SerializeField] CardDB db;


    private CardLogic selectedCard;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupGame(Transform parentTransform, int spawnRange, float height, int nCards)
    {
        Transform[] myCards = GetCards(parentTransform, nCards);
        RandomiseCards(myCards, nCards);
        ArrangeCards(myCards, spawnRange, height, nCards);
    }

    public void RandomiseCards(Transform[] cards, int nCards)
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

    public Transform[] GetCards(Transform parentTransform, int nCards)
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

    public Vector3[] CalculateSpawnPositions(int nCards, int spawnRange, float height)
    {
        Vector3[] spawnPositions = new Vector3[nCards];

        float x = 0, z = 0;

        float unitDistance = (float) spawnRange / (float) nCards;

        Debug.Log("Calculating spawn positions unit distance: " + unitDistance.ToString());

        for (int i = 0; i < nCards; i++)
        {
            Vector3 newSpawnPosition = new Vector3(x, height, z);
            spawnPositions[i] = newSpawnPosition;
            x += unitDistance;
            z += unitDistance;
        }

        return spawnPositions;
    }

    public void RandomisePositions(Transform[] cards, Vector3[] spawnPositions)
    {
        int i = 0;
        foreach (Transform card in cards)
        {
            card.localPosition = spawnPositions[i];
            Debug.Log("Put " + card.name + " at " + spawnPositions[i]);
            i += 1;
        }

        // Random positions code
        //// Randomise the position of the myCards
        //Vector3 randomRangeMin = new Vector3(-spawnRange, height, -spawnRange);
        //Vector3 randomRangeMax = new Vector3(spawnRange, height, spawnRange);

        //foreach (Transform card in myCards)
        //{
        //    Debug.Log(card.name);
        //    if (card.TryGetComponent<CardLogic>(out CardLogic _))
        //    {
        //        // Generate random position within specified range
        //        float randomX = Random.Range(randomRangeMin.x, randomRangeMax.x);
        //        //float randomY = Random.Range(randomRangeMin.y, randomRangeMax.y);
        //        float randomZ = Random.Range(randomRangeMin.z, randomRangeMax.z);

        //        // Set the new position
        //        card.localPosition = new Vector3(randomX, height, randomZ);
        //        Debug.Log(card.gameObject.name + " with position " + card.localPosition.ToString());
        //    }
        //}
    }

    public void ArrangeCards(Transform[] myCards, int spawnRange, float height, int nCards)
    {
        Vector3[] spawnPositions = CalculateSpawnPositions(nCards, spawnRange, height);
        RandomisePositions(myCards, spawnPositions);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }


    public void MatchCards(CardLogic card1, CardLogic card2)
    {
        Unselect();
        Destroy(card1.gameObject);
        Destroy(card2.gameObject);
        PlaySound(matchClip);
    }

    public void SelectCard(CardLogic card)
    {
        if (selectedCard != null)
        {
            if (selectedCard.Match(card)) {
                Debug.Log("Match found!");
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
