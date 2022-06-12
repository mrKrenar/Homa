using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class GameController : MonoSingleton<GameController>
{
    [SerializeField] Portal portalPrefab;
    [SerializeField] Sprite testSprite;
    [SerializeField] float finishDistance = 100;

    [SerializeField] GameObject fpsCounterWindow;

    [SerializeField] Transform moneyLevelStart;

    [SerializeField] PortalData[] portalChoices;


    //if 0, health == 0;
    //if 1, wealth == 0 
    float balance = .5f;
    float Balance
    {
        get { return balance; }
        set { 
            balance = value;

            if (balance > .999f)
            {
                CameraController.Instance.ignoreFollowing = true;
                MainCharacterController.Instance.Die(DeathReason.hunger);
            }
            else if (balance < .001f)
            {
                CameraController.Instance.ignoreFollowing = true;
                MainCharacterController.Instance.Die(DeathReason.sadness);
            }
        }
    }

    bool gameStarted;
    public bool GameStarted
    {
        get
        {
            return gameStarted;
        }
        set
        {
            gameStarted = value;
            MainCharacterController.Instance.GameStarted = gameStarted;
            if (gameStarted)
            {
                CharacterAnimationController.Instance.SetAnimation(CharacterAnimationType.running);
                UiManager.Instance.EnableCanvas(1);
            }
        }
    }

    int CurrentLevel
    {
        get
        {
            return PlayerPrefs.GetInt("CurrentLevel", 1);
        }
        set
        {
            PlayerPrefs.SetInt("CurrentLevel", value);
        }
    }

    private void Awake()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
    }

    // Start is called before the first frame update
    void Start()
    {
        moneyLevelStart.GetChild(Random.Range(0, moneyLevelStart.childCount)).gameObject.SetActive(true);

        UiManager.Instance.SetLevelText(CurrentLevel);

        PortalData tmp;

        for (int i = 2; i < 10; i++)
        {
            tmp = portalChoices[Random.Range(0, portalChoices.Length)];

            Instantiate(portalPrefab, Vector3.forward * i * 10, Quaternion.identity).Setup(
                () => {
                    //health chosen
                    Balance += tmp.healthChange;

                    MainCharacterUI.Instance.UpdateHealthSlider(tmp.healthChange);
                    MainCharacterUI.Instance.UpdateWealthSlider(-tmp.healthChange);
                },
                () => {
                    //wealth chosen
                    Balance -= tmp.healthChange;

                    MainCharacterUI.Instance.UpdateHealthSlider(-tmp.healthChange);
                    MainCharacterUI.Instance.UpdateWealthSlider(tmp.healthChange);
                },
                tmp.healthSprite,
                tmp.wealthSprite,
                tmp.healthText,
                tmp.wealthText);
        }
    }

    private void Update()
    {
        UiManager.Instance.SetLevelProgressSlider(MainCharacterController.Instance.transform.position.z / finishDistance);
    }

    public void ToggleFpsCounterWindow()
    {
        fpsCounterWindow.SetActive(!fpsCounterWindow.activeSelf);
    }

    public void NextLevel()
    {
        CurrentLevel++;
        RestartLevel();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

[Serializable]
public class PortalData
{
    public float healthChange;
    public Sprite healthSprite, wealthSprite;
    public string healthText, wealthText;
}