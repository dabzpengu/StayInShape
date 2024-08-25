using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class FlipCardScript : MonoBehaviour
{
    [SerializeField]
    GameObject cardBack;
    [SerializeField]
    bool cardBackIsActive;
    [SerializeField]
    float rotSpeed;
    public Image frontImage, backImage;
    public int id;

    bool currentlyFlipping;
    bool clicked = false;
    FlipCardGameManager flipCardGameManager;
    AudioSource audioSource;
    InputManager inputManager;
    RectTransform rect;

    void Awake()
    {
        rect = this.GetComponent<RectTransform>();
        inputManager = InputManager.instance;
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        inputManager.OnStartTouch += CardClicked;
    }
    private void OnDisable()
    {
        inputManager.OnStartTouch -= CardClicked;
    }
    public void Setup(FlipCardGameManager manager, Sprite frontSprite, Sprite backSprite, int id, float memorizeTime)
    {
        StartCoroutine(tempMute(memorizeTime + 0.5f));
        flipCardGameManager = manager;
        this.id = id;
        frontImage.sprite = frontSprite;
        backImage.sprite = backSprite;
        cardBack.SetActive(cardBackIsActive);
        StartCoroutine(CalculateFlip(memorizeTime));
    }
    private void CardClicked(Vector2 pos, float time)
    {
        if (rect.rect.Contains(rect.InverseTransformPoint(pos)) && !clicked)
        {
            flipCardGameManager.onCardClick(this);
        }
        
    }
    public void ValidClick()
    {
        clicked = !clicked;
        StartCoroutine(CalculateFlip(0));
    }
    void Flip()
    {
        audioSource.Play();
        cardBackIsActive = !cardBackIsActive;
        cardBack.SetActive(cardBackIsActive);
    }

    public IEnumerator CalculateFlip(float delay)
    {
        yield return new WaitForSeconds(delay);
        while (currentlyFlipping) yield return new WaitForSeconds(0.01f); //prevent overlap of flip animation
        currentlyFlipping = true;
        int timer = 0;
        for (int i = 0; i < 180/rotSpeed; i++)
        {
            yield return new WaitForSeconds(0.01f);
            transform.Rotate(new Vector3(0, rotSpeed, 0));
            timer++;
            if(timer == 90/rotSpeed || timer == -90/rotSpeed)
            {
                Flip();
            }
        }
        flipCardGameManager.checkPair();
        currentlyFlipping = false;
        yield break;
    }

    private IEnumerator ShrinkCard()
    {
        transform.DOScale(0, 0.5f).SetEase(Ease.InOutSine);
        yield return null;
    }

    public void MatchedCard()
    {
        DOTween.Kill(gameObject.transform);
        StartCoroutine(ShrinkCard());
    }

    public void WrongClick()
    {
        clicked = !clicked;
        StartCoroutine(CalculateFlip(0.5f));
    }

    private IEnumerator tempMute(float muteTime)
    {
        audioSource.volume = 0;
        yield return new WaitForSeconds(muteTime);
        audioSource.volume = 0.15f;
    }
}
