using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;


public class MatchingCardManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI status;
    [SerializeField] TextMeshProUGUI instructions;
    [SerializeField] AudioClip selectClip;
    [SerializeField] AudioClip matchCorrectClip;
    [SerializeField] AudioClip matchWrongClip;
    [SerializeField] AudioClip winGameClip;
    [SerializeField] CardDB db;
    [SerializeField] PlayerDataSO player;
    [SerializeField] SaveManagerSO saveManager;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject L_ui;
    [SerializeField] ARTrackedImageManager trackedImageManager;


    public int timeToDisplayText = 3;
    public int intervalToPlayGame = 5;
    public int reward = 6;
    public int heightOfCards = 4;
    public int nCards = 8;
    public int spawnRange = 5;
    private CardLogic selectedCard;
    private int nCardsLeft;
    private int currReward;
    private AudioSource audioSource;
    private Transform parentTransform;
    private MatchingCardsPrefab gamePrefab;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currReward = 0;
    }

    private IEnumerator DisplayTextCoroutine(string message, float duration)
    {
        status.text = message;
        yield return new WaitForSeconds(duration);
        status.text = "";
    }

    private void DisplayText(string text)
    {
        StartCoroutine(DisplayTextCoroutine(text, timeToDisplayText));
    }

    private void RewardPlayer(int reward)
    {
        PlaySound(winGameClip);
        player.SetFertilizer(player.GetFertilizer() + reward);
        player.SetWater(player.GetWater() + reward);
        DisplayText(String.Format("You have earned {0} fertilizers and water!", 
            reward));
    }

    private void AddReward(int add, string plantName) // TODO: Find a way to utilise currReward
    {
        currReward += add;
        DisplayText(String.Format("You have matched a pair of {0} cards!", plantName));
    }

    private void CompleteGame()
    {
        RewardPlayer(reward);
        player.SetMatchingCardTimer(DateTime.Now.AddMinutes(intervalToPlayGame));
        instructions.text = "You have won the game. Tap on the back button to go to the home screen.\n Next time to play is " + player.GetMatchingCardTimer();
        saveManager.Save();
    }

    private void SpawnCards()
    {
        // Spawn cards first
        for (int i = 0; i < nCards; i++)
        {
            GameObject instance = Instantiate(cardPrefab, Vector3.zero, transform.rotation);
            instance.gameObject.name = "Card " + i.ToString();
            instance.transform.SetParent(parentTransform);
            instance.transform.rotation = parentTransform.rotation;
        }
    }

    private void SpawnStartButton()
    {
        startButton.SetActive(true);
    }

    public void SetupGame(Transform pTransform, MatchingCardsPrefab callee)
    {
        gamePrefab = callee;
        instructions.text = "Don't see anything? Try moving closer or further to the QR code.";
        status.text = "To properly spawn AR, move your phone so that the blue and red lines fit inside the L on your screen.\nOnce ready, press START GAME.";
        currReward = 0;
        nCardsLeft = nCards;
        parentTransform = pTransform;
        L_ui.SetActive(true);
        SpawnStartButton();
        //Transform[] myCards = GetCards(pTransform, nCards);
        //RandomiseCards(myCards, nCards);
        //ArrangeCards(myCards, spawnRange, nCards);
    }

    public void StartGame()
    {
        L_ui.SetActive(false);
        status.text = "Look up, the cards are in front of you. Good Luck!";
        instructions.text = "Match every card to another similar card!\r\nYou can select a card by tapping on them!";
        startButton.SetActive(false);
        trackedImageManager.enabled = false;
        SpawnCards();
        Transform[] myCards = GetCards(parentTransform, nCards);
        RandomiseCards(myCards, nCards);
        ArrangeCards(myCards, spawnRange, nCards);
        gamePrefab.StartGame();
    }

    private void RandomiseCards(Transform[] cards, int nCards)
    {
        if (nCards % 2 != 0)
        {
            throw new System.Exception("Number of cards cannot be an odd number!");
        }

        MatchingCardSO[] list = db.getRandomCardList();

        for (int i = 0; i < nCards/2; i++)
        {
            MatchingCardSO cardData = list[i];
            CardLogic cardA = cards[i].gameObject.GetComponent<CardLogic>();
            CardLogic cardB = cards[nCards - 1 - i].gameObject.GetComponent<CardLogic>();
            cardA.SetCard(cardData, true);
            cardB.SetCard(cardData, false);
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

    private Vector3[] CalculateSpawnPositions(int nCards, int spawnRange)
    {
        Vector3[] spawnPositions = new Vector3[nCards];
        int z_offset = 3;
        float angleStep = 360f / nCards;
        float x, z;

        for (int i = 0; i < nCards; i++)
        {
            float angleDegrees = i * angleStep;
            float angleRadians = angleDegrees * Mathf.Deg2Rad;
            Vector3 newSpawnPosition;
            if (i % 2 == 0)
            {
                x = Mathf.Cos(angleRadians) * spawnRange;
                //z = Mathf.Sin(angleRadians) * spawnRange;
                newSpawnPosition = new Vector3(i * spawnRange * 0.25f - 4, heightOfCards, z_offset);
            } else
            {
                x = Mathf.Cos(angleRadians) * spawnRange;
                //z = Mathf.Sin(angleRadians) * spawnRange;
                newSpawnPosition = new Vector3((i-1) * spawnRange * 0.25f - 4, heightOfCards * 1.5f, z_offset);
            }
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
        float yRotation = 90; // Trial and error Tested value
        float rotateStep = 0; // 360 / cards.Length;
        shuffleCards(cards);

        int i = 0;
        foreach (Transform card in cards)
        {
            card.localPosition = spawnPositions[i];
            card.transform.Rotate(0, 0, 0);
            //yRotation -= rotateStep; // Trial and error Tested value
            i += 1;
        }
    }

    private void ArrangeCards(Transform[] myCards, int spawnRange, int nCards)
    {
        Vector3[] spawnPositions = CalculateSpawnPositions(nCards, spawnRange);
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
        PlaySound(matchCorrectClip);
        nCardsLeft -= 2;
        AddReward(1, card1.plantName);
        if (nCardsLeft == 0)
        {
            CompleteGame();
        }
    }

    public void SelectCard(CardLogic card)
    {
        if (selectedCard != null && selectedCard != card)
        {
            if (selectedCard.IsMatching(card)) {
                Debug.Log("IsMatching found!");
                MatchCards(selectedCard, card);
            } else
            {
                PlaySound(matchWrongClip); // Can play this if we can overcome infinite looping
                Debug.Log("Unable to match!");
            }
        }
        else if (selectedCard == null || card != selectedCard)
        {
            selectedCard = card;
            if (selectedCard.isImage)
            {
                DisplayText("You have selected " + selectedCard.plantName);
            } else
            {
                DisplayText("You have selected the trivia card: " + selectedCard.description);
            }
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
            DisplayText("Unselected!");
            PlaySound(selectClip);
        }
    }
}
