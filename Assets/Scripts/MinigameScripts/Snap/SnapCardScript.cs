using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SnapCardScript : MonoBehaviour
{
    [SerializeField]
    GameObject cardBack;
    [SerializeField]
    bool cardBackIsActive;
    [SerializeField]
    float rotSpeed;
    [SerializeField]
    AudioClip flipSound;

    public Image frontImage, backImage;
    public int id;

    bool currentlyFlipping;
    SnapGameManager snapGameManager;
    AudioSource audioSource;
    RectTransform rect;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = flipSound;
        rect = this.GetComponent<RectTransform>();
    }

    public void Setup(SnapGameManager manager, Sprite frontSprite, Sprite backSprite, int id)
    {
        snapGameManager = manager;
        this.id = id;
        frontImage.sprite = frontSprite;
        backImage.sprite = backSprite;
        cardBackIsActive = true;
        cardBack.SetActive(cardBackIsActive);
        StartCoroutine(CalculateFlip(0));
    }

    public void FlipCard()
    {
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
        for (int i = 0; i < 180 / rotSpeed; i++)
        {
            yield return new WaitForSeconds(0.01f);
            transform.Rotate(new Vector3(0, rotSpeed, 0));
            timer++;
            if (timer == 90 / rotSpeed || timer == -90 / rotSpeed)
            {
                Flip();
            }
        }
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

    public void ResetCard()
    {
        transform.eulerAngles = Vector3.zero;
    }
}
