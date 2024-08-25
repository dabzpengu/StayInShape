using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlipCardTutorialManager : MonoBehaviour
{
    [SerializeField]
    int startingCards;
    [SerializeField]
    FlipCardGameManager gameManager;
    [SerializeField]
    GameObject tutorialPromptObject;
    [SerializeField]
    GameObject[] tutorialBoxes;

    void Start()
    {

        tutorialPromptObject.transform.DOScale(0, 0.8f).From();

    }

    IEnumerator tutorialStart()
    {
        showBox(1, true);
        gameManager.SetupGame(startingCards);
        yield return new WaitForSeconds(gameManager.memorizeTime);
        gameManager.giveHint();
        showBox(1, false);
        showBox(2, true);
        while(gameManager.moveCount == 0) yield return 0;
        showBox(2, false);
        showBox(3, true);
        yield return new WaitForSeconds(3);
        showBox(3, false);
        showBox(4, true);
        while (gameManager.currentCardCount != 0) yield return 0;
        showBox(4, false);
        showBox(5, true);
        yield return new WaitForSeconds(5);
        showBox(5, false);
    }

    public void startTutorial()
    {
        StartCoroutine(tutorialStart());
        showBox(0, false);
    }

    public void skipTutorial()
    {
        showBox(0, false);
        gameManager.SetupGame(12);
    }

    private void showBox(int boxNum,bool show)
    {
        if (boxNum == 0) tutorialPromptObject.transform.DOScale(0, 0.8f).SetEase(Ease.InOutSine).OnComplete(() => tutorialPromptObject.SetActive(show));
        else
        {
            if (show)
            {
                tutorialBoxes[boxNum - 1].SetActive(show);
                tutorialBoxes[boxNum - 1].transform.DOScale(0, 0.8f).From().SetEase(Ease.InOutSine).OnComplete(()=> tutorialBoxes[boxNum - 1].transform.DOScale(0.95f, 0.7f).SetLoops(-1,LoopType.Yoyo));
            }
            else
            {
                tutorialBoxes[boxNum - 1].transform.DOScale(0, 0.8f).SetEase(Ease.InOutSine).OnComplete(()=> tutorialBoxes[boxNum - 1].SetActive(show));
            }
        }
    }
}
