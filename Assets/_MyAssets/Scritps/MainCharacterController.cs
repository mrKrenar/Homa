using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum DeathReason { sadness, hunger}

public class MainCharacterController : MonoSingleton<MainCharacterController>
{
    [SerializeField] float collectMoneyDistance = 1;
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float groundWidth = 4.5f;

    Vector3 updatedPosition;
    float lastXPos, currentXPos, deltaX;

    public bool GameStarted { get; set; }

    bool died;

    void Start()
    {
        updatedPosition = transform.position;

        StartCoroutine(CheckCollectables());
        
        IEnumerator CheckCollectables()
        {
            var checkDelay = new WaitForSeconds(.1f);
            Collider[] colliders;

            while (true)
            {
                colliders = Physics.OverlapSphere(transform.position, collectMoneyDistance);

                foreach (var item in colliders)
                {
                    if (item.CompareTag("Money"))
                    {
                        Debug.Log("money found", item.gameObject);
                    }
                }
                yield return checkDelay;
            }
        }
    }

    public void Die(DeathReason reason)
    {
        died = true;

        CameraController.Instance.PlayerDied();

        UiManager.Instance.SetDeathReason(reason);

        transform.DOMove(new Vector3(0, 0, transform.position.z + 5), 1f).OnComplete(
            () => {
                CharacterAnimationController.Instance.SetAnimation(CharacterAnimationType.die);
            });
    }

    private void Update()
    {
        if (died)
        {
            return;
        }

        //when stop touching, make sure: deltaX = 0 and character stops moving on X
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            lastXPos = currentXPos = 0;
        }
        
        //prevent character from "jumping" on first click
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            lastXPos = currentXPos = TouchPositionFromScreenCenter01();
        }

        //calculate deltaX
        if (Input.GetKey(KeyCode.Mouse0))
        {
            lastXPos = currentXPos;
            currentXPos = TouchPositionFromScreenCenter01();
            deltaX = (currentXPos - lastXPos) / Screen.width;

            deltaX *= groundWidth;

            //cache movement X calculations
            updatedPosition.x += deltaX;

            //clamp x position to allowed values
            if (updatedPosition.x > (groundWidth - 2) / 2)
            {
                updatedPosition.x = (groundWidth - 2) / 2;
            }
            else if (updatedPosition.x < (-groundWidth + 2) / 2)
            {
                updatedPosition.x = (-groundWidth + 2) / 2;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!GameStarted || died)
        {
            return;
        }

        //apply movement calculations
        updatedPosition.z += Time.fixedDeltaTime * moveSpeed;
        transform.position = updatedPosition;
    }

    float TouchPositionFromScreenCenter01()
    {
        return Input.mousePosition.x - (Screen.width / 2);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, collectMoneyDistance);
    }
}
