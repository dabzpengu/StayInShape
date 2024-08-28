using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class SnapGameManager : MonoBehaviour
{
    [SerializeField]
    SnapCardScript playerCard, deckCard;
    [SerializeField]
    Sprite[] cardSprites;
    [SerializeField]
    Sprite backSprite;
    [SerializeField]
    int[,] level = new int[5,2] { {6,2 },{10,4 },{14,6 },{18,8 },{ 20, 10 } }; // Right number represents the number of matches needed, left number represents the number of spare cards
    [SerializeField]
    GameObject tutorialPrompt, popupBar;
    [SerializeField]
    TMPro.TextMeshProUGUI remainingText;
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
    MinigameTimerSO minigameTimer;
    [SerializeField]
    String[] formIds;
    [SerializeField]
    AudioClip successSound;

    public List<int> deckList;
    public int currentDeckCard;
    public int currentIndex;
    public int nCorrects = 0;
    public int nWrongs = 0;
    public float totalTime;
    int currentLevelId;
    string[] stats = new string[5];
    Vector3 initialPos;
    bool lockout;
    bool deckDrawn;
    public bool popupActive;
    int totalMoves;
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentLevelId = 0;
        deckDrawn = false;
        initialPos = playerCard.transform.position;
        deckList = new List<int>();
        lockout = true;
        SetupGame();
        toggleTutorial();
        remainingText.text = (level[currentLevelId, 0] + level[currentLevelId, 1] - currentIndex - 1).ToString(); // Unsure how this actually works
    }
    private void Update()
    {
        if (deckDrawn) totalTime += Time.deltaTime;
    }

    void SetupGame()
    {
        currentIndex = 0;
        totalMoves = 0;
        totalTime = 0;
        nCorrects = 0;
        nWrongs = 0;
        deckDrawn = false;
        deckList.Clear();
        while(deckList.Count < level[currentLevelId, 1])
        {
            int num = UnityEngine.Random.Range(0, level[currentLevelId, 0] + level[currentLevelId, 1] - 1); // For first level, it will be 15 + 6 - 1 = 20
            if (deckList.Contains(num)) continue; // Not sure what this part actually does
            deckList.Add(num);
        }
        remainingText.text = (level[currentLevelId, 0] + level[currentLevelId, 1] - currentIndex - 1).ToString(); // Number of items left
        wrongText.text = nWrongs.ToString();
        correctText.text = nCorrects.ToString();
    }

    public void Snap() // Buy button
    {
        if (lockout) return;
        totalMoves++;
        if (!deckList.Contains(currentIndex))
        {
            StartCoroutine(Popup("Draw the next card when the cards are not the same!"));
            nWrongs++;
            wrongText.text = nWrongs.ToString();
            return;
        } else
        {
            nCorrects++;
            correctText.text = nCorrects.ToString();

            if (deckList.Max() == currentIndex)
            {
                Debug.Log("Game ended!");
                CompleteGame();
                return;
            }
            currentIndex++;
            remainingText.text = (level[currentLevelId, 0] + level[currentLevelId, 1] - currentIndex - 1).ToString();
            StartCoroutine(MoveCardsOut());
            deckDrawn = false;
        }
    }
    public void DrawNext()
    {
        if (!minigameTimer.canPlayNow())
        {
            StartCoroutine(Popup(String.Format("Sorry you can only play the game after {0}",
                minigameTimer.getInterpretableDateTime())));
        }
        else
        {
            if (lockout) return;
            if (deckDrawn)
            {
                totalMoves++;
                if (deckList.Contains(currentIndex)) // When the cards are the same
                {
                    StartCoroutine(Popup("Buy when the cards are the same!"));
                    return;
                }
                currentIndex++;
                remainingText.text = (level[currentLevelId, 0] + level[currentLevelId, 1] - currentIndex - 1).ToString();
                StartCoroutine(FlipNewPlayerCard());
            }
            else
            {
                StartCoroutine(FlipBothCards());
                deckDrawn = true;
            }
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

    void CompleteGame()
    {
        int reward = (nCorrects - nWrongs) < 0 ? 0 : (nCorrects - nWrongs);
        currency.AddAmount(reward);
        StartCoroutine(Popup(String.Format("Well done! You've earned {0} points with {1} correct and {2} wrongs! Keep it up!", reward, nCorrects, nWrongs)));
        minigameTimer.SetNextOpenTimeFromNow();
        //currency.AddAmount(SnapMinigameSO.reward);
        saveManager.Save();
        Send();
        audioSource.clip = successSound;
        audioSource.Play();
        if (currentLevelId < level.Length - 1) currentLevelId++;
        playerCard.FlipCard();
        deckCard.FlipCard();
        SetupGame();
    }
    IEnumerator FlipBothCards()
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
        DrawNext();
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
    private void Send()
    {
        stats[0] = playerDataSO.playerName;
        stats[1] = totalMoves.ToString();
        stats[2] = (currentIndex+1).ToString();
        stats[3] = totalTime.ToString();
        stats[4] = (totalTime / totalMoves).ToString();
        GoogleFormsPoster.Post(stats, formIds, "https://docs.google.com/forms/u/0/d/e/1FAIpQLSen_oSSbQte3sBZbkmh_MGPNMXXyQHG_RFWYqV4l_VKTia62Q/formResponse");
    }


}
