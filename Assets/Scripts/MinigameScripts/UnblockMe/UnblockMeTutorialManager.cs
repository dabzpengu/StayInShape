using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UnblockMeTutorialManager : MonoBehaviour
{
    [SerializeField]
    int startingLevel;
    [SerializeField]
    UnblockMeGameController gameManager;
    [SerializeField]
    GameObject tutorialPromptObject;
    [SerializeField]
    GameObject[] tutorialBoxes;

    static public bool tutorialCleared = false;

    void Start()
    {
        
        tutorialPromptObject.transform.DOScale(0, 0.8f).From();

    }

    IEnumerator tutorialStart()
    {
        showBox(1, true);
        gameManager.SetNewLevel(startingLevel);
        yield return new WaitForSeconds(5f);
        gameManager.GenerateHint();
        showBox(1, false);
        showBox(2, true);
        while (!tutorialCleared) yield return 0;
        showBox(2, false);
        showBox(3, true);
        yield return new WaitForSeconds(3f);
        showBox(3, false);
        showBox(4, true);
        yield return new WaitForSeconds(10f);
        showBox(4, false);
    }

    public void startTutorial()
    {
        StartCoroutine(tutorialStart());
        showBox(0, false);
    }

    public void skipTutorial()
    {
        showBox(0, false);
        gameManager.SetNewLevel(startingLevel);
    }

    private void showBox(int boxNum, bool show)
    {
        if (boxNum == 0)
        {
            if(!show) tutorialPromptObject.transform.DOScale(0, 0.8f).SetEase(Ease.InOutSine).OnComplete(() => tutorialPromptObject.SetActive(show));
            
        }
        else
        {
            if (show)
            {
                tutorialBoxes[boxNum - 1].SetActive(show);
                tutorialBoxes[boxNum - 1].transform.DOScale(0, 0.8f).From().SetEase(Ease.InOutSine).OnComplete(() => tutorialBoxes[boxNum - 1].transform.DOScale(0.95f, 0.7f).SetLoops(-1, LoopType.Yoyo));
            }
            else
            {
                tutorialBoxes[boxNum - 1].transform.DOScale(0, 0.8f).SetEase(Ease.InOutSine).OnComplete(() => tutorialBoxes[boxNum - 1].SetActive(show));
            }
        }
    }
}
