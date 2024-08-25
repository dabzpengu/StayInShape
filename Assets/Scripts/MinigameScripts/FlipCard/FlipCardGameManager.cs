using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class FlipCardGameManager : MonoBehaviour
{
    [SerializeField]
    int numCards,cardsPerRow;
    [SerializeField]
    GameObject flipCardPrefab, cardHolderHorizontalPrefab;
    [SerializeField]
    Sprite[] cardSprites;
    [SerializeField]
    Sprite backSprite;
    [SerializeField]
    GameObject reloadBtn, hintBtn,timer, popupBar;
    [SerializeField]
    TMPro.TextMeshProUGUI moveCounter;
    [SerializeField]
    CurrencySO currency;
    [SerializeField]
    PlayerDataSO playerDataSO;
    [SerializeField]
    SaveManagerSO saveManager;
    [SerializeField]
    MinigameSO flipCardMinigameSO;
    [SerializeField]
    String[] formIds;
    [SerializeField]
    AudioClip[] audioClips; // 0 -> win sound, 1 -> hint sound (hint sound not impl)

    public float memorizeTime;
    public int moveCount;
    public int currentCardCount;

    List<GameObject> horizontalCardHolders;
    List<FlipCardScript> gameCards;
    List<FlipCardScript> selectedCards;
    List<FlipCardScript> hintCards;


    Sequence setupSequence;
    int wrongSinceMatch = 0;
    int hintThisRound = 0;
    bool isHintActive;
    float timeSinceMatch = 0f;
    float timeThisRound;
    bool inputFrozen = false;
    private System.Random rng;
    bool popupActive = false;
    AudioSource audioSource;
    TMPro.TextMeshProUGUI timerText;
    string[] stats = new string[5]; //0: number of moves 1: number of pairs of cards 2: time taken from first move
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        horizontalCardHolders = new List<GameObject>();
        gameCards = new List<FlipCardScript>();
        selectedCards = new List<FlipCardScript>();
        hintCards = new List<FlipCardScript>();
        rng = new System.Random();
        timeThisRound = 0;
        timerText = timer.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        isHintActive = false;
    }
    private void Update()
    {
        if (gameCards.Count == 0) return;
        timeSinceMatch += Time.deltaTime;
        if (timeSinceMatch >= 30f)
        {
            StartCoroutine(Popup("Having some trouble? Let me help! Here's a pair!"));
            giveHint();
        }
        if (moveCount > 0) timeThisRound += Time.deltaTime;
        timerText.text = "Time taken: " + (int) timeThisRound + "s";
    }
    private void Start()
    {
        //restart game
        reloadBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            StopAllCoroutines();
            cleanBoard();
            SetupGame(numCards);
        });

        //Hint btn click event
        hintBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            giveHint();
        });
    }

    private IEnumerator freezeInput(float time)
    {
        inputFrozen = true;
        yield return new WaitForSeconds(time);
        inputFrozen = false;
    }
    public void SetupGame(int cardNum)
    {
        timeSinceMatch = 0;
        setupSequence = DOTween.Sequence();
        numCards = cardNum;
        if (numCards % 2 != 0)
        {
            Debug.Log("Even card number cannot be setup as pairs");
            return;
        }
        else if (numCards <= 12) cardsPerRow = 3;
        else if (numCards > 12 && numCards <= 20) cardsPerRow = 4;
        else if (numCards > 20) cardsPerRow = 5;
        if(numCards >= 24)
        {
            timer.SetActive(true);
            timer.transform.DOScale(0, 0.5f).From().SetEase(Ease.InOutSine);
        }
        else
        {
            timer.SetActive(false);
        }
        List<int> idList = new List<int>();
        for(int i = 0; i < numCards/2; i++)
        {
            idList.Add(i);
            idList.Add(i);
        }
        Shuffle(idList);
        for(int i = 0; i < Mathf.Ceil((float)numCards/cardsPerRow); i++)
        {
            GameObject horizontalCardHolder = Instantiate(cardHolderHorizontalPrefab, this.transform);
            horizontalCardHolders.Add(horizontalCardHolder);
            for(int j = 0; j < cardsPerRow; j++)
            {
                if (idList.Count == 0) break;
                GameObject card = Instantiate(flipCardPrefab, horizontalCardHolder.transform);
                FlipCardScript cardScript = card.GetComponent<FlipCardScript>();
                int cardId = idList[0];
                idList.RemoveAt(0);
                cardScript.Setup(this,cardSprites[cardId],backSprite,cardId,memorizeTime);
                gameCards.Add(cardScript);
            }
        }
        currentCardCount = numCards;
        foreach(FlipCardScript card in gameCards)
        {
            setupSequence.Append(card.transform.DOScale(0, 0.1f).From().SetEase(Ease.InOutSine));
        }
        StartCoroutine(freezeInput(memorizeTime));
    }
    public void checkPair()
    {
        if (selectedCards.Count != 2) return;
        updateMoveCounter(moveCount + 1);
        if (selectedCards[0].id == selectedCards[1].id) // if match
        {
            foreach (FlipCardScript card in selectedCards)
            {
                card.MatchedCard();
                gameCards.Remove(card);
                if (hintCards.Contains(card))
                {
                    isHintActive = false;
                    hintCards.Remove(card);
                }
            }
            selectedCards.Clear();
            currentCardCount -= 2;
            wrongSinceMatch = 0;
            timeSinceMatch = 0;
        }
        else
        {
            foreach (FlipCardScript card in selectedCards) card.WrongClick();
            selectedCards.Clear();
            wrongSinceMatch += 1;
            if (wrongSinceMatch == numCards * 0.5) giveHint();
        }
        if(currentCardCount == 0) StartCoroutine(completedGame());
        
    }
    public void onCardClick(FlipCardScript clickedCard)
    {
        if (inputFrozen || selectedCards.Count == 2) return;
        if (hintCards.Count !=0 && !hintCards.Contains(clickedCard)) return;
        selectedCards.Add(clickedCard);
        clickedCard.ValidClick();
    }

    IEnumerator completedGame()
    {
        currency.AddAmount(flipCardMinigameSO.reward);
        saveManager.Save();
        Send();
        audioSource.clip = audioClips[0];
        audioSource.Play();
        StartCoroutine(Popup("Well done! Here comes the next set!"));
        yield return new WaitForSeconds(2);
        cleanBoard();
        yield return new WaitForSeconds(3);
        numCards = Math.Min(numCards + 2, 24);
        SetupGame(numCards);
    }
    private void cleanBoard()
    {
        updateMoveCounter(0);
        gameCards.Clear();
        foreach (GameObject cardHolderObject in horizontalCardHolders) Destroy(cardHolderObject);
        timeThisRound = 0;
        hintThisRound = 0;
    }

    public void giveHint()
    {
        if (isHintActive) return;
        hintThisRound++;
        if (currentCardCount < 3) return;
        int hintPairId = gameCards[UnityEngine.Random.Range(0, gameCards.Count)].id;
        hintCards.Clear();
        foreach (FlipCardScript card in gameCards)
        {
            if (card.id == hintPairId)
            {
                card.transform.DOScale(1.1f,1f).SetLoops(20, LoopType.Yoyo);
                card.backImage.DOColor(Color.magenta, 1f).SetLoops(20, LoopType.Yoyo);
                hintCards.Add(card);
            }
        }
        isHintActive = true;
        timeSinceMatch = 0f;
        wrongSinceMatch = 0;
    }

    private void Send()
    {
        stats[0] = playerDataSO.playerName;
        stats[1] = moveCount.ToString();
        stats[2] = (numCards / 2).ToString();
        stats[3] = timeThisRound.ToString();
        stats[4] = hintThisRound.ToString();
        GoogleFormsPoster.Post(stats, formIds, "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfrjtROA8dOWKqTl-LelBso3IrgeRn6p4mvVl-BSKJACKxfFg/formResponse");
    }

    private void updateMoveCounter(int moveCount)
    {
        this.moveCount = moveCount;
        moveCounter.text = moveCount.ToString();
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
    private void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
