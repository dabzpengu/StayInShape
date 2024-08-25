using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeText : MonoBehaviour
{
    [SerializeField]
    MenuManager menuManager;
    [SerializeField]
    PlayerDataSO playerDataSO;
    [SerializeField]
    TransitionController transitionController;
    void Start()
    {
        GetComponentInChildren<TMPro.TextMeshProUGUI>().DOFade(0, 1).From().SetEase(Ease.InOutSine);
        StartCoroutine(NextMenu());
    }


    IEnumerator NextMenu()
    {
        yield return new WaitForSeconds(2f);
        GetComponentInChildren<TMPro.TextMeshProUGUI>().DOFade(0, 1).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(2f);
        if (!playerDataSO.IsNextSurveyAvailable())
        {
            transitionController.exitScene("PetSelectionScene");
        }
        else
        {
            menuManager.OpenMenu("LoginMenu");
        }

    }
}
