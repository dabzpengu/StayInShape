using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.XR.ARFoundation;


public class ChickenInvaderManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerUI;
    [SerializeField] TextMeshProUGUI instructionsUI;
    [SerializeField] AudioClip chickenClip;
    [SerializeField] AudioClip winGameClip;
    [SerializeField] PlayerDataSO player;
    [SerializeField] SaveManagerSO saveManager;
    [SerializeField] ARTrackedImageManager trackedImageManager;
    [SerializeField] GameObject spawnButton;
    [SerializeField] GameObject chickenPrefab;
    [SerializeField] GameObject L_ui;
    [SerializeField] GameObject eye_ui;

    public int intervalToPlayGame = 5;
    public int timePerRound = 30;
    public float spawnRange = 5;
    public float timeLeft;
    public bool isGameEnded;
    public int reward = 12;
    public int nChickens = 10;
    public int chickenInterval = 3;

    private Transform target;
    private Transform ground;
    private ChickenInvaderPrefab gamePrefab;
    private Coroutine countdownCoroutine;
    private AudioSource audioSource;
    private Vector3 randomRangeMin;
    private Vector3 randomRangeMax;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        randomRangeMin = new Vector3(-spawnRange, 0, -spawnRange);
        randomRangeMax = new Vector3(spawnRange, 0, spawnRange);
    }

    IEnumerator StartCountdown()
    {
        timeLeft = timePerRound;
        isGameEnded = false;

        while (timeLeft > 0 && !isGameEnded)
        {
            // Update the UI with the remaining time
            timerUI.text = timeLeft.ToString("F1") + "s";

            // Wait for 1 second before updating the timer
            yield return new WaitForSeconds(1f);

            // Reduce the time by 1 second
            timeLeft--;
        }

        WinGame();
    }

    private void RewardPlayer()
    {
        PlaySound(winGameClip);
        player.SetChickenInvaderTimer(DateTime.Now.AddMinutes(intervalToPlayGame));
        player.SetFertilizer(player.GetFertilizer() + reward);
        player.SetWater(player.GetWater() + reward);
        saveManager.Save();
    }

    public void WinGame()
    {
        StopCoroutine(countdownCoroutine);
        instructionsUI.text = "Hurray! You protected all your seeds. You win!";
        timerUI.text = String.Format("You have earned {0} fertilizers and water!\n Next time to play is {1} minutes later", reward, intervalToPlayGame);
        CompleteGame();
    }

    public void LoseGame()
    {
        StopCoroutine(countdownCoroutine);
        reward = 0;
        RewardPlayer();
        instructionsUI.text = "Oh No! A chicken has reached your seeds. You lost.";
        timerUI.text = String.Format("Next time to play is {0} minutes later", intervalToPlayGame);
        CompleteGame();
    }

    private void CompleteGame()
    {
        isGameEnded = true;
        timeLeft = 0;
        RewardPlayer();
        saveManager.Save();
    }

    public void SetupGame(Transform target, Transform ground, ChickenInvaderPrefab prefab)
    {
        L_ui.SetActive(true);
        gamePrefab = prefab;
        instructionsUI.text = "Don't see anything? Try moving closer or further to the QR code.";
        timerUI.text = "To properly spawn AR, move your phone so that the blue and red lines fit inside the L on your screen.\nOnce ready, press START GAME.";
        this.target = target;
        this.ground = ground;
        timeLeft = timePerRound;
        SpawnStartButton();
    }

    public void SpawnStartButton()
    {
        spawnButton.SetActive(true);
    }

    public Vector3 CalculateSpawnPosition(float spawnRange)
    {
        float x, z;
        int i = UnityEngine.Random.Range(0, 360);
        float angleRad = i * Mathf.Deg2Rad;
        x = (Mathf.Cos(angleRad) * spawnRange);
        z = Math.Abs(Mathf.Sin(angleRad) * spawnRange);
        //x = 0;
        //z = spawnRange;
        return new Vector3(x, spawnRange, z);
    }

    public void SpawnInvader(Transform target, Transform ground, Vector3 spawnLoc)
    {
        
        GameObject instance = Instantiate(chickenPrefab, Vector3.zero, ground.rotation);
        instance.gameObject.name = "Chicken";
        instance.transform.SetParent(ground);
        instance.transform.localPosition = spawnLoc;
        Debug.Log(String.Format("Invader spawned at {0} {1} {2}", 
            instance.transform.position.x, instance.transform.position.y, instance.transform.position.z));
        InvaderLogic i = instance.GetComponent<InvaderLogic>();
        if (instance.GetComponent<InvaderLogic>() == null)
        {
            Debug.LogError("Error: 'InvaderLogic' component is missing on 'instance'.");
        } else
        {
            StartCoroutine(i.AttackTarget(target, this));
        }
    }

    IEnumerator spawnChickens(int interval, int nChickens)
    {
        for (int i = 0; i < nChickens; i++) 
        {
            SpawnInvader(target, ground, CalculateSpawnPosition(spawnRange));
            yield return new WaitForSeconds(interval);
        }
    }
    
    public void StartGame()
    {
        eye_ui.SetActive(true);
        L_ui.SetActive(false);
        instructionsUI.text = "Look at the chickens to chase them away before they steal your seeds!";
        timerUI.text = "Game start!";
        gamePrefab.StartGame();
        L_ui.SetActive(false);
        spawnButton.SetActive(false);
        trackedImageManager.enabled = false;
        isGameEnded = false;
        countdownCoroutine = StartCoroutine(StartCountdown());
        StartCoroutine(spawnChickens(chickenInterval, nChickens));
    }
    private void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
