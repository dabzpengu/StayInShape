using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameListManager : MonoBehaviour
{

    [SerializeField]
    GameCardUI[] gameCardUIs;
    [SerializeField]
    MinigameSO[] minigameSOs;
    [SerializeField]
    SceneDataSO sceneDatabase;
    [SerializeField]
    TransitionController transitionController;

    private void Awake()
    {
        int i = 0;
        while (i < minigameSOs.Length && i < gameCardUIs.Length)
        {
            gameCardUIs[i].Setup(minigameSOs[i],sceneDatabase,transitionController);
            i++;
        }
    }
}
