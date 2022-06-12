using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Portal : MonoBehaviour
{
    UnityAction onOption0Chosen, onOption1Chosen;

    [SerializeField] TriggerDetector option0, option1;
    [SerializeField] SpriteRenderer option0Img, option1Img;
    [SerializeField] TextMeshPro option0Txt, option1Txt;

    private void Start()
    {
        option0.onTriggerEnter += OnOptionChosen;
        option1.onTriggerEnter += OnOptionChosen;

        option0.onTriggerEnter += _ => {
            option0.ignoreTrigger = true;
            option1.ignoreTrigger = true;
        };

        option1.onTriggerEnter += _ => {
            option0.ignoreTrigger = true;
            option1.ignoreTrigger = true;
        };

    }

    private void OnDestroy()
    {
        option0.onTriggerEnter -= OnOptionChosen;
        option1.onTriggerEnter -= OnOptionChosen;
    }

    void OnOptionChosen(int optionId)
    {
        switch (optionId)
        {
            case 0:
                onOption0Chosen?.Invoke();
                break;
            case 1:
                onOption1Chosen?.Invoke();
                break;
        }
    }

    public void Setup(UnityAction onOption0Chosen, UnityAction onOption1Chosen, Sprite option0Sprite, Sprite option1Sprite, string option0Txt, string option1Txt)
    {
        //set callbacks on option chosen
        this.onOption0Chosen = onOption0Chosen;
        this.onOption1Chosen = onOption1Chosen;


        //set sprite option, if sprite provided, otherwise disable
        if (option0Sprite != null)
        {
            this.option0Img.sprite = option0Sprite;
        }
        else
        {
            this.option0Img.enabled = false;
        }
        if (option1Sprite != null)
        {
            this.option1Img.sprite = option1Sprite;
        }
        else
        {
            this.option1Img.enabled = false;
        }


        //set option text, if text provided, otherwise disable
        if (!string.IsNullOrEmpty(option0Txt))
        {
            this.option0Txt.text = option0Txt;
        }
        else
        {
            this.option0Txt.enabled = false;
        }
        if (!string.IsNullOrEmpty(option1Txt))
        {
            this.option1Txt.text = option1Txt;
        }
        else
        {
            this.option1Txt.enabled = false;
        }
    }
}
