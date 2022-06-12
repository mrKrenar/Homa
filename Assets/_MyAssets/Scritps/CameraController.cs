using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float cameraOffset = 2.5f;
    Vector3 startPosition, updatedPosition;

    public bool ignoreFollowing;

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void FixedUpdate()
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
}
