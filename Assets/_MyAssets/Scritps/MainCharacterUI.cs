using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MainCharacterUI : MonoSingleton<MainCharacterUI>
{
    [SerializeField] Slider healthSlider, wealthSlider;
    
    [SerializeField] Image healthSliderBackground, healthSliderFill, wealthSliderBackground, wealthSliderFill;
    [SerializeField] Color lowColorBackground = Color.red, highColorBackground = Color.green;
    [SerializeField] Color lowColorFill = Color.red, highColorFill = Color.green;

    private void Start()
    {
        UpdateWealthSlider(.5f);
        UpdateHealthSlider(.5f);
    }

    public void UpdateHealthSlider(float valueToAdd)
    {
        UpdateSlider(healthSlider, valueToAdd);

        healthSliderBackground.DOColor(Color.Lerp(lowColorBackground, highColorBackground, healthSlider.value + valueToAdd), 1);
        healthSliderFill.DOColor(Color.Lerp(lowColorFill, highColorFill, healthSlider.value + valueToAdd), 1);
    }

    public void UpdateWealthSlider(float valueToAdd)
    {
        UpdateSlider(wealthSlider, valueToAdd);

        wealthSliderBackground.DOColor(Color.Lerp(lowColorBackground, highColorBackground, wealthSlider.value + valueToAdd), 1);
        wealthSliderFill.DOColor(Color.Lerp(lowColorFill, highColorFill, wealthSlider.value + valueToAdd), 1);
    }

    void UpdateSlider(Slider slider, float valueToAdd)
    {
        slider.DOComplete();

        if (slider.value + valueToAdd > 1)
        {
            valueToAdd = 1 - slider.value;
        }
        else if (slider.value + valueToAdd < 0)
        {
            valueToAdd = -slider.value;
        }

        slider.DOValue(slider.value + valueToAdd, 1);
    }
}
