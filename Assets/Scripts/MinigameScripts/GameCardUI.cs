using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCardUI : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI gameNameText, gameDescriptionText, gameRewardText;
    MinigameSO game;
    SceneDataSO sceneDatabase;
    public void Setup(MinigameSO game, SceneDataSO sceneDatabase,TransitionController transitionController)
    {
        this.game = game;
        this.sceneDatabase = sceneDatabase;
        gameObject.GetComponent<Button>().onClick.AddListener(delegate
        {
            transitionController.exitScene(game.gameSceneName);
        });
        updateUI();
    }

    void updateUI()
    {
        gameNameText.text = game.gameName;
        gameRewardText.text = game.reward.ToString() + " points";
        gameDescriptionText.text = game.description;

    }
}
