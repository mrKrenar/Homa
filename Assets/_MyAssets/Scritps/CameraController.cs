using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoSingleton<CameraController>
{
    [SerializeField] float cameraOffset = 2.5f;
    Vector3 startPosition, updatedPosition;

    public bool ignoreFollowing;

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void LateUpdate()
    {
        if (ignoreFollowing)
        {
            return;
        }

        updatedPosition.x = startPosition.x;
        updatedPosition.y = startPosition.y;
        updatedPosition.z = MainCharacterController.Instance.transform.position.z -cameraOffset;

        transform.position =  updatedPosition;
    }

    public void PlayerDied()
    {
        ignoreFollowing = true;

        Vector3 diePosition = new Vector3(0, 0, MainCharacterController.Instance.transform.position.z + 5);

        transform.DODynamicLookAt(diePosition, 5);

        transform.DOMove(diePosition + Vector3.up * 5, 5f);
    }
}
