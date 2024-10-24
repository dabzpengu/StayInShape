using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SnapGameManager : MonoBehaviour
{
    [SerializeField]
    SnapCardScript playerCard, deckCard;
    [SerializeField]
    Sprite[] cardSprites;
    [SerializeField]
    Sprite backSprite;
    [SerializeField]
    int[,] level = new int[5,2] { {15,3 },{10,4 },{14,6 },{18,8 },{ 20, 10 } }; // Right number represents the number of matches needed, left number represents the number of spare cards
    [SerializeField]
    GameObject tutorialPrompt, popupBar;
    [SerializeField]
    TMPro.TextMeshProUGUI nMatchesLeft;
    [SerializeField]
    TMPro.TextMeshProUGUI backButton;
    [SerializeField]
    TMPro.TextMeshProUGUI timerValue;
    [SerializeField]
    TMPro.TextMeshProUGUI correctText;
    [SerializeField]
    TMPro.TextMeshProUGUI wrongText;

    [SerializeField]
    CurrencySO currency;
    [SerializeField]
    PlayerDataSO playerDataSO;
    [SerializeField]
    SaveManagerSO saveManager;
    [SerializeField]
    MinigameSO SnapMinigameSO;
    [SerializeField]
    String[] formIds;
    [SerializeField]
    AudioClip successSound;
    [SerializeField]
    AudioClip failSound;
    [SerializeField] GameObject drawBut;
    [SerializeField] GameObject buyBut;

    public List<int> deckList;
    public int currentDeckCard; // Deck (RHS)
    public int currentIndex; // Indicates the current index (number of cards we have drawn) so far. If our index matches one of the decks, then we have the same cards.
    public int nCorrects = 0;
    public int nWrongs = 0;
    public int intervalToPlayGame = 5;
    public static float timePerSnap = 5;
    private float timeLeft;
    int currentLevelId;
    string[] stats = new string[5];
    bool lockout;
    bool deckDrawn; // If the deck card (RHS) is drawn
    public bool popupActive;
    int totalMoves;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentLevelId = 0;
        deckDrawn = false;
        deckList = new List<int>();
        lockout = true;
        SetupGame();
        toggleTutorial();
    }
    private void Update()
    {
        if (deckDrawn && !lockout)
        {
            subtractTimer(Time.deltaTime);
            if (timeLeft < 0)
            {
                StartCoroutine(Popup("You did not snap in time!"));
                incrementWrong();
            }
        }
    }

    private void setTimer(float val)
    {
        timeLeft = val;
        timerValue.text = timeLeft.ToString("F2");
    }

    private void subtractTimer(float val)
    {
        timeLeft -= val;
        timerValue.text = timeLeft.ToString("F2");
    }

    private void incrementWrong()
    {
        audioSource.clip = failSound;
        audioSource.Play();
        //currentIndex++;
        audioSource.clip = failSound;
        audioSource.Play();
        deckDrawn = false;
        nWrongs++;
        updateTextUI(); // Edge case where UI does not update when the game ends after timer runs out. Otherwise, MoveCardsOut handles UI updates too

        //Coroutine movingCardsOut = StartCoroutine(MoveCardsOut());
        if (nWrongs + nCorrects >= level[currentLevelId, 1])
        {
            CompleteGame();
        } else
        {
            StartCoroutine(MoveCardsOut());
        }
    }

    private void incrementCorrect()
    {
        nCorrects++;
        updateTextUI(); // Edge case where UI does not update when the game ends after we win using Snap.
    }

    private void updateTextUI()
    {
        wrongText.text = nWrongs.ToString();
        correctText.text = nCorrects.ToString();
        nMatchesLeft.text = (level[currentLevelId, 1] - nCorrects - nWrongs).ToString();
    }

    void SetupGame()
    {
        currentIndex = 0;
        totalMoves = 0;
        nCorrects = 0;
        nWrongs = 0;
        deckDrawn = false;
        deckList.Clear();
        while(deckList.Count < level[currentLevelId, 1]) // Fill up deck list with 6 cards (these cards are the matches)
        {
            int num = UnityEngine.Random.Range(0, level[currentLevelId, 0] + level[currentLevelId, 1] - 1); // For first level, it will be 15 + 6 - 1 = 20
            if (deckList.Contains(num)) continue;
            deckList.Add(num); // This number determines how many cards we must flip to eventually "reach" the desired card
            Debug.Log("Deck List has " + num);
        }
        updateTextUI();
    }

    public void Snap() // Snap button
    {
        if (lockout) return;
        totalMoves++;
        if (!deckList.Contains(currentIndex)) // Current index
        {
            StartCoroutine(Popup("Draw the next card when the cards are not the same!"));
            return;
        } else
        {
            incrementCorrect();
            if (deckList.Max() == currentIndex || nWrongs + nCorrects >= level[currentLevelId, 1])
            {
                Debug.Log("Game ended!");
                CompleteGame();
                return;
            }
            currentIndex++;
            updateTextUI();
            StartCoroutine(MoveCardsOut());
            deckDrawn = false;
        }
    }
    public void DrawNext() // Logic for both "Draw" and "Buy" button
    {

        if (lockout) return; // While an animation is happening
        if (deckDrawn) // When draw is pressed, deckDrawn = true
        {
            totalMoves++;
            if (deckList.Contains(currentIndex)) // Prevent drawing when the cards are the same
            {
                Debug.Log(currentIndex);
                StartCoroutine(Popup("Buy when the cards are the same!"));
                return;
            }
            setTimer(timePerSnap);
            currentIndex++;
            updateTextUI();
            StartCoroutine(FlipNewPlayerCard());
        }
        else
        { // When we press draw at the very start of the game to "begin" the game and when snap occurs
            updateTextUI();
            setTimer(timePerSnap);
            StartCoroutine(FlipBothCards());
        }
    }
    public void toggleTutorial()
    {
        if (!tutorialPrompt.activeInHierarchy)
        {
            tutorialPrompt.SetActive(true);
            tutorialPrompt.transform.DOScale(0, 0.5f).From().SetEase(Ease.InOutSine);
        }
        else
        {
            tutorialPrompt.transform.DOScale(0, 0.5f).SetEase(Ease.InOutSine);
            if (lockout) lockout = false;
        }
    }

    void RewardPlayer()
    {
        int reward = (nCorrects - nWrongs) < 0 ? 1 : (nCorrects - nWrongs);
        playerDataSO.SetWater(playerDataSO.GetWater() + reward);
        if (reward > 1)
        {
            StartCoroutine(Popup(String.Format("Well done! With {1} correct and {2} wrongs, you've earned {0} water!", reward, nCorrects, nWrongs)));
        } else
        {
            StartCoroutine(Popup(String.Format("Oh no! You made too many mistakes. you've earned 1 water!", reward, nCorrects, nWrongs)));
        }
    }

    void CompleteGame()
    {
        deckDrawn = false;
        nMatchesLeft.text = "0";
        backButton.text = "Finish";
        RewardPlayer();
        playerDataSO.SetSnapTimer(DateTime.Now.AddMinutes(intervalToPlayGame));
        saveManager.Save();
        //Send();
        audioSource.clip = successSound;
        audioSource.Play();
        if (currentLevelId < level.Length - 1) currentLevelId++;
        drawBut.SetActive(false);
        buyBut.SetActive(false);
        deckDrawn = false;
        setTimer(0);
        //playerCard.FlipCard();
        //deckCard.FlipCard();
        //SetupGame();
    }
    IEnumerator FlipBothCards() // Only used for game setup
    {
        lockout = true;
        int newDeckCard = UnityEngine.Random.Range(0, cardSprites.Length);
        while (newDeckCard == currentDeckCard)
        {
            newDeckCard = UnityEngine.Random.Range(0, cardSprites.Length);
        }
        int newPlayerCard = UnityEngine.Random.Range(0, cardSprites.Length);
        if (deckList.Contains(currentIndex))
        {
            newPlayerCard = newDeckCard;
        }
        else
        {
            while(newPlayerCard == newDeckCard) newPlayerCard = UnityEngine.Random.Range(0, cardSprites.Length);
        }

        playerCard.transform.DOLocalMoveX(0, 0f);
        playerCard.Setup(this, cardSprites[newPlayerCard], backSprite, newPlayerCard);
        playerCard.ResetCard();
        yield return new WaitForSeconds(0.8f);
        
        deckCard.transform.DOLocalMoveX(0, 0f);
        deckCard.Setup(this, cardSprites[newDeckCard], backSprite, newDeckCard);
        deckCard.ResetCard();
        yield return new WaitForSeconds(0.5f);
        currentDeckCard = newDeckCard;
        lockout = false;
        deckDrawn = true;
    }
    IEnumerator FlipNewPlayerCard()
    {
        lockout = true;
        int newPlayerCard = UnityEngine.Random.Range(0, cardSprites.Length);
        if (deckList.Contains(currentIndex))
        {
            newPlayerCard = currentDeckCard;
        }
        else
        {
            while (newPlayerCard == currentDeckCard) newPlayerCard = UnityEngine.Random.Range(0, cardSprites.Length);
        }
        playerCard.transform.DOLocalMoveX(-1000, 0.6f).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(0.8f);
        DOTween.KillAll(playerCard);
        playerCard.transform.DOLocalMoveX(0, 0f);
        playerCard.Setup(this, cardSprites[newPlayerCard], backSprite, newPlayerCard);
        playerCard.ResetCard();
        yield return new WaitForSeconds(0.5f);
        lockout = false;
    }
    IEnumerator MoveCardsOut()
    {
        lockout = true;
        playerCard.transform.DOLocalMoveX(-1000, 0.6f).SetEase(Ease.InOutSine);
        deckCard.transform.DOLocalMoveX(1000, 0.6f).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(0.8f);
        lockout = false;
        if (!(nWrongs + nCorrects >= level[currentLevelId, 1]))
        {
            DrawNext();
        }
    }

    IEnumerator Popup(string text)
    {
        popupActive = true;
        popupBar.SetActive(true);
        DOTween.KillAll(popupBar);
        popupBar.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = text;
        popupBar.transform.localScale = new Vector3(1, 1, 1);
        popupBar.transform.DOScale(0, 0.5f).From();
        yield return new WaitForSeconds(5f);
        popupBar.transform.DOScale(0, 0.5f);
        popupActive = false;
    }
    // Outdated method. Can look at for Google Forms API
    //private void Send()
    //{
    //    stats[0] = playerDataSO.playerName;
    //    stats[1] = totalMoves.ToString();
    //    stats[2] = (currentIndex+1).ToString();
    //    stats[3] = totalTime.ToString();
    //    stats[4] = (totalTime / totalMoves).ToString();
    //    GoogleFormsPoster.Post(stats, formIds, "https://docs.google.com/forms/u/0/d/e/1FAIpQLSen_oSSbQte3sBZbkmh_MGPNMXXyQHG_RFWYqV4l_VKTia62Q/formResponse");
    //}


}
