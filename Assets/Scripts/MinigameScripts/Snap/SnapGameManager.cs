using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class SnapGameManager : MonoBehaviour
{
    [SerializeField]
    SnapCardScript playerCard, deckCard;
    [SerializeField]
    Sprite[] cardSprites;
    [SerializeField]
    Sprite backSprite;
    [SerializeField]
    int[,] level = new int[5,2] { {15,6 },{18,6 },{24,10 },{28,10 },{ 30, 12 } };
    [SerializeField]
    GameObject tutorialPrompt, popupBar;
    [SerializeField]
    TMPro.TextMeshProUGUI remainingText;


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

    public List<int> deckList;
    public int currentDeckCard;
    public int currentIndex;
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
        remainingText.text = (level[currentLevelId, 0] + level[currentLevelId, 1] - currentIndex - 1).ToString();
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
        deckDrawn = false;
        deckList.Clear();
        while(deckList.Count < level[currentLevelId, 1])
        {
            int num = UnityEngine.Random.Range(0, level[currentLevelId, 0] + level[currentLevelId, 1] - 1);
            if (deckList.Contains(num)) continue;
            deckList.Add(num);
        }
        remainingText.text = (level[currentLevelId, 0] + level[currentLevelId, 1] - currentIndex - 1).ToString();
    }

    public void Snap()
    {
        if (lockout) return;
        totalMoves++;
        if (!deckList.Contains(currentIndex))
        {
            StartCoroutine(Popup("Draw the next card when the cards are not the same!"));
            return;
        } else
        {
            if (deckList.Max() == currentIndex)
            {
                StartCoroutine(Popup("Well done! You've earned some points! Keep it up!"));
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
        if (lockout) return;
        if (deckDrawn)
        {
            totalMoves++;
            if (deckList.Contains(currentIndex))
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
        currency.AddAmount(SnapMinigameSO.reward);
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
