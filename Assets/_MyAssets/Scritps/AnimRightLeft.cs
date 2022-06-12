using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimRightLeft : MonoBehaviour
{
    [SerializeField] float targetXPosition;
    [SerializeField] float moveDuration = 1;
    [SerializeField] Ease moveEase = Ease.OutQuart;

    private void Start()
    {
        MoveRightLeftRecursively();
    }

    void MoveRightLeftRecursively()
    {
        transform.DOLocalMoveX(targetXPosition, moveDuration)
            .SetEase(moveEase)
            .OnComplete(() =>
            {
                transform.DOLocalMoveX(-targetXPosition, moveDuration)
                .SetEase(moveEase)
                .OnComplete(MoveRightLeftRecursively);
            });
    }
}
