using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AnimFadeInOnEnable : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;

    private void Awake()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponentInChildren<CanvasGroup>();
        }
    }

    private void OnEnable()
    {
        canvasGroup.DOFade(1, 1);
    }
}
