using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Networking;

public class SurveyManager : MonoBehaviour
{
    [SerializeField]
    Button nextButton, doneButton;
    [SerializeField]
    GameObject[] surveyQuestions;
    [SerializeField]
    PlayerDataSO playerDataSO;

    int currentQuestion;
    public string playerName;
    public string[] surveyAns;
    public string[] formIds;
    private void Awake()
    {
        currentQuestion = 0;
        surveyAns = new string[surveyQuestions.Length];

    }

public void saveQuestionData()
    {
        surveyAns[currentQuestion] = "";
        foreach (Toggle selection in surveyQuestions[currentQuestion].GetComponentsInChildren<Toggle>())
        {
            if (selection.isOn)
            {
                TMPro.TMP_InputField hasInput = selection.GetComponentInChildren<TMP_InputField>();
                if (hasInput)
                {
                    surveyAns[currentQuestion] += "Others: " + hasInput.text;
                }
                else
                {
                    surveyAns[currentQuestion] += selection.GetComponentInChildren<TMPro.TextMeshProUGUI>().text;
                    if (currentQuestion == 1 || currentQuestion == 2) surveyAns[currentQuestion] += ", ";
                }
                if (currentQuestion == 0)
                {
                    surveyAns[currentQuestion] += " : " + surveyQuestions[currentQuestion].GetComponentInChildren<TMP_InputField>().text;
                }
            }
        }
    }

    public void nextQuestion()
    {
        if(currentQuestion == surveyQuestions.Length - 1)
        {
            print("no more questions");
            return;
        }
        GameObject surveyObject = surveyQuestions[currentQuestion].gameObject;
        surveyObject.SetActive(false);
        currentQuestion++;
        StartCoroutine(loadQuestion(currentQuestion));
    }

    IEnumerator loadQuestion(int id)
    {
        surveyQuestions[id].SetActive(true);
        surveyQuestions[id].transform.DOScale(0, 0.5f).From();
        
        if(id == surveyQuestions.Length - 1)
        {
            nextButton.transform.DOScale(0, 0.5f).OnComplete(()=> nextButton.gameObject.SetActive(false));
            doneButton.gameObject.SetActive(true);
            doneButton.transform.DOScale(0, 0.5f).From();
        }
        yield return 0;
    }

    public void changePlayerName(string name)
    {
        this.playerName = name;
    }

    public void Send()
    {
        playerDataSO.playerName = playerName;
        GoogleFormsPoster.Post(new string[] {playerName, surveyAns[0], surveyAns[1], surveyAns[2] }, formIds, "https://docs.google.com/forms/u/0/d/e/1FAIpQLScICoq4_DFxnty7-qiolQ0gHV5xjRGTQQBvhxtyn9qF2dvx_g/formResponse");
    }
}
