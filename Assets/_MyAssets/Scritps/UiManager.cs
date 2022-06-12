using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class UiManager : MonoSingleton<UiManager>
{
    [SerializeField] GameObject tutorialCanvas, playingCanvas, finishedCanvas;
    [SerializeField] GameObject finishedCanvasDie, finishedCanvasWon;
    [SerializeField] GameObject mainMenuCanvas;

    [SerializeField] Slider gameProgress;
    [SerializeField] TextMeshProUGUI thisLevelText, nextLevelText;

    [SerializeField] string[] healthDieReasons, wealthDieReasons;
    [SerializeField] string[] wonReasons;

    [SerializeField] TextMeshProUGUI dieReasonText, wonReasonText;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="canvasId">0 = tutorialCanvas, 1 = playingCanvas, 2 = finishedCanvas</param>
    public void EnableCanvas(int canvasId,  bool disableOthers = true)
    {
        if (disableOthers)
        {
            tutorialCanvas.SetActive(false);
            playingCanvas.SetActive(false);
            finishedCanvas.SetActive(false);
        }

        switch (canvasId)
        {
            case 0:
                tutorialCanvas.SetActive(true);
                break;
            case 1:
                playingCanvas.SetActive(true);
                break;
            case 2:
                finishedCanvas.SetActive(true);
                break;
        }
    }

    public void SetLevelProgressSlider(float newVal)
    {
        gameProgress.value = newVal;
    }

    public void SetLevelText(int currentLevel)
    {
        thisLevelText.text = currentLevel.ToString();
        nextLevelText.text = (currentLevel + 1).ToString();
    }

    public void SetDeathReason(DeathReason reason)
    {
        mainMenuCanvas.SetActive(false);

        finishedCanvasDie.SetActive(true);
        switch (reason)
        {
            case DeathReason.sadness:
                dieReasonText.text = healthDieReasons[Random.Range(0, healthDieReasons.Length)];
                break;
            case DeathReason.hunger:
                dieReasonText.text = wealthDieReasons[Random.Range(0, wealthDieReasons.Length)];
                break;
        }
    }


    public void SetWonReason()
    {
        mainMenuCanvas.SetActive(false);

        finishedCanvasWon.SetActive(true);

        dieReasonText.text = wonReasons[Random.Range(0, wonReasons.Length)];
    }
}
